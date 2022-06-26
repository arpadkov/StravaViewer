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
        int[] distances;
        JArray AltitudeStream;
        JArray HeartRateStream;

        ScottPlot.Plottable.VLine highlightLine;

        GMapMarker marker;

        public DetailedActivityView(Activity activity, JArray LatLngStream, JArray DistanceStream, JArray AltitudeStream, JArray HeartRateStream)
        {
            this.activity = activity;
            this.LatLngStream = LatLngStream;
            this.DistanceStream = DistanceStream;
            this.AltitudeStream = AltitudeStream;
            this.HeartRateStream = HeartRateStream;

            distances = new int[LatLngStream.Count];
            for (int i = 0; i < LatLngStream.Count; i++)
            {
                distances[i] = (int)DistanceStream[i];
            }

            InitializeComponent();

            highlightLine = new ScottPlot.Plottable.VLine();
            highlightLine.LineColor = Color.Black;

            GMapOverlay markers = new GMapOverlay("markers");
            marker = new GMap.NET.WindowsForms.Markers.GMarkerCross(new PointLatLng(0, 0));
            markers.Markers.Add(marker);
            Map.Overlays.Add(markers);

            
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

            //points.Add(new PointLatLng(48.656289, 10.302153));
            //points.Add(new PointLatLng(48.661909, 10.307454));

            foreach (var jtoken_point in LatLngStream)
            {
                double lat = jtoken_point[0].ToObject<double>();
                double lng = jtoken_point[1].ToObject<double>();
                points.Add(new PointLatLng(lat, lng));
                //Map.Position = new PointLatLng(lat, lng);
            }            

            GMapRoute route = new GMapRoute(points, "A walk in the park");
            route.Stroke = new Pen(Color.Red, 3);
            routes.Routes.Add(route);
            Map.Overlays.Add(routes);

            

            Map.Position = points[0];
            //Map.Position = new PointLatLng(0, 0);
        }

        private void CreateElevationPlot()
        {
            List<double> distance_list = new List<double>();
            List<double> elevation_list = new List<double>();

            for (int i = 0; i < DistanceStream.Count; i++)
            {
                distance_list.Add(DistanceStream[i].ToObject<double>()/1000);
                elevation_list.Add(AltitudeStream[i].ToObject<double>());
            }

            double[] distance = distance_list.ToArray();
            double[] elevation = elevation_list.ToArray();

            //foreach (var jtoken_point in DistanceStream)
            //{
            //    distance_list.Add(jtoken_point.ToObject<double>());
            //}

            var plt = new ScottPlot.Plottable.ScatterPlot(distance, elevation);
            plt.Color = Color.SpringGreen;
            plt.LineWidth = 2;
            plt.MarkerSize = 0;

            //var FillPlot = new ScottPlot.Plottable.
            var fillPlot = this.elevationPlot.Plot.AddFill(distance, elevation);
            //fillPlot.Color = Color.ForestGreen;
            //fillPlot.FillColor = Color.ForestGreen;
            fillPlot.FillColor = Color.SpringGreen;
            fillPlot.LineWidth = 0;
            //fillPlot.
            

            //this.elevationPlot.Plot.XAxis.Label("Distance [km]");
            //this.elevationPlot.Plot.YAxis.Label("Elevation [m]");

            elevationPlot.Plot.Add(plt);

            elevationPlot.Plot.Add(highlightLine);

            elevationPlot.Refresh();
        }

        public void CreateMultiPlot()
        {
            List<double> distance_list = new List<double>();
            List<double> heartrate_list = new List<double>();

            for (int i = 0; i < HeartRateStream.Count; i++)
            {
                distance_list.Add(DistanceStream[i].ToObject<double>() / 1000);
                heartrate_list.Add(HeartRateStream[i].ToObject<double>());
            }

            double[] distance = distance_list.ToArray();
            double[] heartrate = heartrate_list.ToArray();

            var heartratePlot = new ScottPlot.Plottable.ScatterPlot(distance, heartrate);
            heartratePlot.Color = Color.Red;
            heartratePlot.LineWidth = 2;
            heartratePlot.MarkerSize = 0;

            multiPlot.Plot.Add(heartratePlot);
            multiPlot.Plot.Add(highlightLine);
            multiPlot.Refresh();
        }

        private void HighlightPoint(double distance)
        {

            highlightLine.X = distance;

            //double[] array = DistanceStream.ToArray<double>();

            int index = Array.IndexOf(distances, Convert.ToInt32(distance*1000));
            //var nearest = array.MinBy(x => Math.Abs((long)x - distance));

            if (index != -1)
            {
                var jtoken_point = LatLngStream[index];
                double lat = jtoken_point[0].ToObject<double>();
                double lng = jtoken_point[1].ToObject<double>();

                PointLatLng point = new PointLatLng(lat, lng);
                marker = new GMarkerCross(point);

                Map.Overlays[0].IsVisibile = false;
                Map.Overlays[0].IsVisibile = true;
                Map.Refresh();

            }

            

            //elevationPlot.Plot.Add(line);
            elevationPlot.Refresh();
            multiPlot.Refresh();
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
            (double x, double y) = elevationPlot.GetMouseCoordinates();

            HighlightPoint(x);
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
