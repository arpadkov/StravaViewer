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
        JArray AltitudeStream;

        public DetailedActivityView(Activity activity, JArray LatLngStream, JArray DistanceStream, JArray AltitudeStream)
        {
            this.activity = activity;
            this.LatLngStream = LatLngStream;
            this.DistanceStream = DistanceStream;
            this.AltitudeStream = AltitudeStream;

            InitializeComponent();
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

            var elevationPlot = new ScottPlot.Plottable.ScatterPlot(distance, elevation);
            elevationPlot.Color = Color.SpringGreen;
            elevationPlot.LineWidth = 2;
            elevationPlot.MarkerSize = 0;

            //var FillPlot = new ScottPlot.Plottable.
            var fillPlot = multiPlot.Plot.AddFill(distance, elevation);
            //fillPlot.Color = Color.ForestGreen;
            //fillPlot.FillColor = Color.ForestGreen;
            fillPlot.FillColor = Color.SpringGreen;
            fillPlot.LineWidth = 0;
            //fillPlot.
            

            multiPlot.Plot.XAxis.Label("Distance [km]");
            multiPlot.Plot.YAxis.Label("Elevation [m]");

            multiPlot.Plot.Add(elevationPlot);
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
    }
}
