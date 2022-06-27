using StravaViewer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using ScottPlot;

namespace StravaViewer.Forms
{
    public partial class DetailedActivityView : Form
    {
        Activity activity;
        JArray LatLngStream;
        JArray DistanceStream;
        double[] distances;
        JArray AltitudeStream;
        double[] elevations;
        JArray HeartRateStream;
        double[] heartrates;
        JArray TimeStream;


        ScottPlot.Plottable.VLine highlightLine;
        ScottPlot.Plottable.Tooltip infoToolTip;
        //ScottPlot.PlottableVline

        GMapOverlay markers;
        GMapMarker marker;

        int plot_resolution = 500;

        public DetailedActivityView(Activity activity, JArray LatLngStream, JArray DistanceStream, JArray AltitudeStream, JArray HeartRateStream, JArray TimeStream)
        {
            this.activity = activity;
            this.LatLngStream = LatLngStream;
            this.DistanceStream = DistanceStream;
            this.AltitudeStream = AltitudeStream;
            this.HeartRateStream = HeartRateStream;
            this.TimeStream = TimeStream;

            List<double> distances_list = new List<double>();
            List<double> elevations_list = new List<double>();
            List<double> heartrates_list = new List<double>();

            int value_frequency = 1;
            if (LatLngStream.Count > plot_resolution)
            {
                value_frequency = LatLngStream.Count / plot_resolution;
            }
            
            for (int i = 0; i < LatLngStream.Count; i += value_frequency)
            {
                distances_list.Add((double)DistanceStream[i] / 1000);
                elevations_list.Add((double)AltitudeStream[i]);
                heartrates_list.Add((double)HeartRateStream[i]);
            }

            distances = distances_list.ToArray();
            elevations = elevations_list.ToArray();
            heartrates = heartrates_list.ToArray();

            InitializeComponent();

            this.timer1.Interval = 1000 / Properties.Settings.Default.PlotRefreshRate;
            numericUpDown1.Value = Properties.Settings.Default.PlotRefreshRate;

            highlightLine = new ScottPlot.Plottable.VLine();
            highlightLine.LineColor = Color.Black;

            infoToolTip = new ScottPlot.Plottable.Tooltip();
            infoToolTip.Label = "isnt empty";
            infoToolTip.X = 0;
            infoToolTip.Y = 0;



        }

        private void DetailedActivityView_Load(object sender, EventArgs e)
        {
            //Map.MapProvider = GoogleMapProvider.Instance;
            //Map.MapProvider = GoogleSatelliteMapProvider.Instance;
            //Map.MapProvider = GoogleTerrainMapProvider.Instance;
            Map.MapProvider = OpenStreetMapProvider.Instance;
            Map.DragButton = MouseButtons.Left;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            Map.ShowCenter = false;

            double temp_marker_lat = LatLngStream[0][0].ToObject<double>();
            double temp_marker_lng = LatLngStream[0][1].ToObject<double>();

            markers = new GMapOverlay("markers");
            marker = new GMarkerGoogle(new PointLatLng(temp_marker_lat, temp_marker_lng), GMarkerGoogleType.yellow);
            //marker.Size = System.Drawing.Size(10, 10);
            markers.Markers.Add(marker);
            Map.Overlays.Add(markers);

            CreateRoute();
            CreateElevationPlot();
            CreateMultiPlot();

            //Map.Position = new PointLatLng(activity.start_lat, activity.start_lng);
            //Map.Position = new PointLatLng(48.655, 10.299);    //Altenberg
        }

        private void CreateRoute()
        {
            // ROUTES EXAMPLE
            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();

            foreach (var jtoken_point in LatLngStream)
            {
                double lat = jtoken_point[0].ToObject<double>();
                double lng = jtoken_point[1].ToObject<double>();
                points.Add(new PointLatLng(lat, lng));
            }            

            GMapRoute route = new GMapRoute(points, "A walk in the park");
            route.Stroke = new Pen(Color.Red, 3);
            routes.Routes.Add(route);
            Map.Overlays.Add(routes);            

            Map.Position = points[0];
        }

        private void CreateElevationPlot()
        {
            //var plt = new ScottPlot.Plottable.ScatterPlot(distances, elevations);
            var plt = elevationPlot.Plot.AddSignalXY(distances, elevations);
            plt.Color = Color.SpringGreen;
            plt.LineWidth = 2;
            plt.MarkerSize = 0;
            plt.FillBelow(Color.SpringGreen, Color.Transparent);

            //var fillPlot = elevationPlot.Plot.AddFill(distances, elevations);
            //fillPlot.FillColor = Color.SpringGreen;
            //fillPlot.LineWidth = 0;


            //this.elevationPlot.Plot.XAxis.Label("Distance [km]");
            //this.elevationPlot.Plot.YAxis.Label("Elevation [m]");

            elevationPlot.Plot.Add(plt);

            elevationPlot.Plot.Add(highlightLine);
            elevationPlot.Plot.Add(infoToolTip);

            elevationPlot.Refresh();
        }

        public void CreateMultiPlot()
        {
            var heartratePlot = new ScottPlot.Plottable.ScatterPlot(distances, heartrates);
            heartratePlot.Color = Color.Red;
            heartratePlot.LineWidth = 2;
            heartratePlot.MarkerSize = 0;

            multiPlot.Plot.Add(heartratePlot);
            multiPlot.Plot.Add(highlightLine);

            multiPlot.Plot.MatchLayout(elevationPlot.Plot, true, true);

            multiPlot.Refresh();
        }

        private void HighlightPoint(double distance)
        {
            highlightLine.IsVisible = true;
            highlightLine.X = distance;

            // find index in Streams
            double[] full_distances_array = DistanceStream.ToObject<double[]>();

            double closest_distance = ClosestValue(full_distances_array, distance * 1000);
            int index = Array.IndexOf(full_distances_array, closest_distance);

            // label string
            double elevation = AltitudeStream[index].ToObject<double>();
            TimeSpan time = TimeSpan.FromSeconds(TimeStream[index].ToObject<double>());
            double heartrate = HeartRateStream[index].ToObject<double>();
            string info =
                String.Format("Distance: {0} km\n", Math.Round(distance, 2)) +
                String.Format("Elevation: {0} m\n", Math.Round(elevation, 2)) +
                "Time: " + time.ToString(@"hh\:mm\:ss") + "\n" +
                String.Format("Heartrate: {0} bpm\n", Math.Round(heartrate));

            infoToolTip.X = distance;
            var x = elevationPlot.Plot.YAxis.GetSize();
            double yMax = elevationPlot.Plot.YAxis.Dims.Max;
            double yMin = elevationPlot.Plot.YAxis.Dims.Min;
            infoToolTip.Y = ((yMax + yMin) / 2) + ((yMax-yMin) * 0.2);
            infoToolTip.Label = info;

            try
            {
                elevationPlot.Render();
                multiPlot.Render();
            }
            catch (Exception ex)
            {

            }

            // MAP           

            if (index > 0)
            {
                var jtoken_point = LatLngStream[index];
                double lat = jtoken_point[0].ToObject<double>();
                double lng = jtoken_point[1].ToObject<double>();

                PointLatLng point = new PointLatLng(lat, lng);
                markers.Markers.Clear();
                markers.Markers.Add(new GMarkerGoogle(point, GMarkerGoogleType.yellow));

            }
        }

        private void CleanHighlight()
        {
            highlightLine.IsVisible = false;
            markers.Markers.Clear();
        }

        private void Map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                double lat = Map.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = Map.FromLocalToLatLng(e.X, e.Y).Lng;

                //coordLabel.Text = lat.ToString() + " - " + lng.ToString();
            }
        }

        private void elevationPlot_MouseMove(object sender, MouseEventArgs e)
        {
            //(double x, double y) = elevationPlot.GetMouseCoordinates();

            //HighlightPoint(x);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            (double x, double y) = elevationPlot.GetMouseCoordinates();
            //elevationPlot.Mou

            HighlightPoint(x);
        }

        private void elevationPlot_MouseEnter(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void elevationPlot_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            CleanHighlight();
        }

        private double ClosestValue(double[] array, double toFind)
        {
            //int toFindRound = Convert.ToInt32(toFind);
            double closest = array[0];
            double minDifference = Math.Abs(array[0] - toFind);
            //double closest = array[0];

            foreach (double val in array)
            {
                double dif = Math.Abs(toFind - val);

                if (dif < minDifference)
                {
                    minDifference = dif;
                    closest = val;
                }
            }

            return closest;
        }

        private void openStravaButton_Click(object sender, EventArgs e)
        {
            activity.OpenInBrowser();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(1000 / numericUpDown1.Value);
        }

        //public static int findClosest(int[] arr,
        //                          int target)
        //{
        //    int n = arr.Length;

        //    // Corner cases
        //    if (target <= arr[0])
        //        return arr[0];
        //    if (target >= arr[n - 1])
        //        return arr[n - 1];

        //    // Doing binary search
        //    int i = 0, j = n, mid = 0;
        //    while (i < j)
        //    {
        //        mid = (i + j) / 2;

        //        if (arr[mid] == target)
        //            return arr[mid];

        //        /* If target is less
        //        than array element,
        //        then search in left */
        //        if (target < arr[mid])
        //        {

        //            // If target is greater
        //            // than previous to mid,
        //            // return closest of two
        //            if (mid > 0 && target > arr[mid - 1])
        //                return getClosest(arr[mid - 1],
        //                             arr[mid], target);

        //            /* Repeat for left half */
        //            j = mid;
        //        }

        //        // If target is
        //        // greater than mid
        //        else
        //        {
        //            if (mid < n - 1 && target < arr[mid + 1])
        //                return getClosest(arr[mid],
        //                     arr[mid + 1], target);
        //            i = mid + 1; // update i
        //        }
        //    }

        //    // Only single element
        //    // left after search
        //    return arr[mid];
        //}

        //public static int ClosestTo(this IEnumerable<int> collection, int target)
        //{
        //    // NB Method will return int.MaxValue for a sequence containing no elements.
        //    // Apply any defensive coding here as necessary.
        //    var closest = int.MaxValue;
        //    var minDifference = int.MaxValue;
        //    foreach (var element in collection)
        //    {
        //        var difference = Math.Abs((long)element - target);
        //        if (minDifference > difference)
        //        {
        //            minDifference = (int)difference;
        //            closest = element;
        //        }
        //    }

        //    return closest;
        //}
    }


}
