using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using ScottPlot;
using System.Data;
using StravaViewer.Client.Activity;

namespace StravaViewer.Forms
{
    public partial class DetailedActivityView : Form
    {
        Activity activity;
        ActivityStreams streams;
        ActivityLaps laps;

        ScottPlot.Plottable.VLine highlightLine;
        ScottPlot.Plottable.Tooltip infoToolTip;
        ScottPlot.Plottable.SignalPlotXY elevationSignaPlot;
        //ScottPlot.PlottableVline vline;

        GMapOverlay markers;
        GMapMarker marker;

        

        public DetailedActivityView(Activity activity, ActivityStreams streams, ActivityLaps laps)
        {
            this.activity = activity;
            this.streams = streams;
            this.laps = laps;

            InitializeComponent();

            this.timer1.Interval = 1000 / 30;
            numericUpDown1.Value = 30;

            highlightLine = new ScottPlot.Plottable.VLine();
            highlightLine.LineColor = Color.Black;

            infoToolTip = new ScottPlot.Plottable.Tooltip();
            infoToolTip.Label = "isnt empty";
            infoToolTip.X = 0;
            infoToolTip.Y = 0;

            lapsGridView.DataSource = laps.LapsTable;

            //this.Cursor = new Cursor(Cursor.Current.Handle);

            //MapRefreshThread = new Thread(new ThreadStart(RefreshMap));
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

            double temp_marker_lat = streams.latlngs[0][0];
            double temp_marker_lng = streams.latlngs[0][1];

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
            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();

            foreach (var point in streams.latlngs)
            {
                points.Add(new PointLatLng(point[0], point[1]));
            }            

            GMapRoute route = new GMapRoute(points, "A walk in the park");
            route.Stroke = new Pen(Color.Red, 3);
            routes.Routes.Add(route);
            Map.Overlays.Add(routes);            

            Map.Position = points[0];
        }

        private void CreateElevationPlot()
        {
            //var plt = new ScottPlot.Plottable.ScatterPlot(streams.distances_lowres, streams.elevations_lowres);
            elevationSignaPlot = elevationPlot.Plot.AddSignalXY(streams.distances_lowres, streams.elevations_lowres);
            elevationSignaPlot.Color = Color.SpringGreen;
            elevationSignaPlot.LineWidth = 2;
            elevationSignaPlot.MarkerSize = 0;
            elevationSignaPlot.FillBelow(Color.SpringGreen, Color.Transparent);

            //this.elevationPlot.Plot.XAxis.Label("Distance [km]");
            //this.elevationPlot.Plot.YAxis.Label("Elevation [m]");

            elevationPlot.Plot.Add(elevationSignaPlot);

            elevationPlot.Plot.Add(highlightLine);
            elevationPlot.Plot.Add(infoToolTip);

            elevationPlot.Refresh();
        }

        public void CreateMultiPlot()
        {
            var heartratePlot = new ScottPlot.Plottable.ScatterPlot(streams.distances_lowres, streams.heartrates_lowres);
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
            highlightLine.X = distance;

            // TODO: This needs refactpring

            //find index in Streams
            //int index = streams.IndexOfClosestDistance(distance);
            (double mouseCoordX, _) = elevationPlot.GetMouseCoordinates();
            (double pointX, double pointY, int index) = elevationSignaPlot.GetPointNearestX(mouseCoordX);


            //label string
            double elevation = streams.elevations_lowres[index];
            TimeSpan time = TimeSpan.FromSeconds(streams.times_lowres[index]);
            double heartrate = streams.times_lowres[index];

            string info =
                String.Format("Distance: {0} km\n", Math.Round(distance, 2)) +
                String.Format("Elevation: {0} m\n", Math.Round(elevation, 2)) +
                "Time: " + time.ToString(@"hh\:mm\:ss") + "\n" +
                String.Format("Heartrate: {0} bpm\n", Math.Round(heartrate));

            infoToolTip.X = distance;
            var x = elevationPlot.Plot.YAxis.GetSize();
            double yMax = elevationPlot.Plot.YAxis.Dims.Max;
            double yMin = elevationPlot.Plot.YAxis.Dims.Min;
            infoToolTip.Y = ((yMax + yMin) / 2) + ((yMax - yMin) * 0.2);
            infoToolTip.Label = info;

            try
            {
                //highlightLine.RenderLine();
                elevationPlot.Render();
                //multiPlot.Render();
            }
            catch (Exception ex)
            {

            }

            // MAP           
            if (index > 0)
            {
                PointLatLng point = new PointLatLng(streams.latlngs_lowres[index][0], streams.latlngs_lowres[index][1]);
                marker.Position = point;
                //markers.Markers.Clear();
                //markers.Markers.Add(new GMarkerGoogle(point, GMarkerGoogleType.yellow));

            }
        }

        private void HighlightSection(int lapIndex)
        {
            //float start_distance = 
        }

        private void CleanHighlight()
        {
            highlightLine.IsVisible = false;
            marker.IsVisible = false;
            infoToolTip.IsVisible = false;

            elevationPlot.Render();
            multiPlot.Render();
            //markers.Markers.Clear();
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
            //System.Windows.Forms.Cursor.Hide();
            marker.IsVisible = true;
            highlightLine.IsVisible = true;
            infoToolTip.IsVisible = true;
            timer1.Start();
        }

        private void elevationPlot_MouseLeave(object sender, EventArgs e)
        {
            //System.Windows.Forms.Cursor.Show();
            timer1.Stop();
            CleanHighlight();
        }

        private double ClosestValue(double[] array, double toFind)
        {
            double closest = array[0];
            double minDifference = Math.Abs(array[0] - toFind);

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

        private void lapsGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            HighlightSection(index);

            foreach (DataGridViewRow row in lapsGridView.Rows)
            {
                if (row.Index == index)
                {
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }

            }
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
