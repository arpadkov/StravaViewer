﻿using Newtonsoft.Json;
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
        ActivityRoute main_route;
        ActivityStreams streams;
        ActivityLaps laps;
        //List<ActivityRoute> additional_routes = new List<ActivityRoute>();

        ScottPlot.Plottable.VLine highlightLine;
        ScottPlot.Plottable.Polygon highlightPolygon;
        ScottPlot.Plottable.Tooltip infoToolTip;
        ScottPlot.Plottable.SignalPlotXY elevationSignaPlot;
        //ScottPlot.PlottableVline vline;

        GMapOverlay routes = new GMapOverlay("routes");
        //GMapOverlay main_route_overlay = new GMapOverlay("main_route");

        GMapOverlay markers = new GMapOverlay("markers");
        GMapMarker highlight_marker;

        double? current_lat;
        double? current_lng;

        bool hasHertrate = true;

        public DetailedActivityView(Activity activity, ActivityStreams streams, ActivityLaps laps)
        {
            this.activity = activity;
            this.main_route = streams.Route;
            this.streams = streams;
            this.laps = laps;

            InitializeComponent();

            this.timer1.Interval = 1000 / 30;
            numericUpDown1.Value = 30;

            highlightLine = new ScottPlot.Plottable.VLine();
            highlightLine.LineColor = Color.Black;
            highlightLine.IsVisible = false;

            highlightPolygon = new ScottPlot.Plottable.Polygon(new double[4], new double[4]);
            highlightPolygon.FillColor = Color.FromArgb(50, Color.DarkSlateGray);
            highlightPolygon.LineWidth = 0;

            infoToolTip = new ScottPlot.Plottable.Tooltip();
            infoToolTip.Label = "isnt empty";
            infoToolTip.X = 0;
            infoToolTip.Y = 0;
            infoToolTip.IsVisible = false;

            lapsGridView.DataSource = laps.LapsTable;

            //this.Cursor = new Cursor(Cursor.Current.Handle);

            //MapRefreshThread = new Thread(new ThreadStart(RefreshMap));
        }

        private void DetailedActivityView_Load(object sender, EventArgs e)
        {
            mainSplitContainer.BorderStyle = BorderStyle.FixedSingle;
            MapPloSplitContainer.BorderStyle = BorderStyle.FixedSingle;
            PlotsSplitContainer.BorderStyle = BorderStyle.FixedSingle;

            //Map.MapProvider = GoogleMapProvider.Instance;
            //Map.MapProvider = GoogleSatelliteMapProvider.Instance;
            //Map.MapProvider = GoogleTerrainMapProvider.Instance;
            Map.MapProvider = OpenStreetMapProvider.Instance;
            Map.DragButton = MouseButtons.Left;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            Map.ShowCenter = false;

            double temp_marker_lat = main_route.latlngs[0][0];
            double temp_marker_lng = main_route.latlngs[0][1];

            //markers = new GMapOverlay("markers");
            //var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string filePath = Path.Combine(projectPath, "Resources");
            highlight_marker = new GMarkerGoogle(new PointLatLng(temp_marker_lat, temp_marker_lng), new Bitmap(Properties.Resources.highlight_marker));
            highlight_marker.Size = new Size(24, 24);
            highlight_marker.Offset = new Point(-12, -12);
            //marker.Size = System.Drawing.Size(10, 10);
            markers.Markers.Add(highlight_marker);
            Map.Overlays.Add(markers);
            

            //streams.CheckStreams();

            DrawRoute(main_route, "main_route", Color.Red);
            CreateElevationPlot();


            CreateMultiPlot();

            

            //Map.Position = new PointLatLng(activity.start_lat, activity.start_lng);
            //Map.Position = new PointLatLng(48.655, 10.299);    //Altenberg
        }

        //private void DrawRoute(ActivityStreams additional_stream)
        //{
        //    GMapOverlay additionalRoute = new GMapOverlay("AdditionalActivity");
        //    List<PointLatLng> points = new List<PointLatLng>();

        //    foreach (var point in additional_stream.latlngs)
        //    {
        //        points.Add(new PointLatLng(point[0], point[1]));
        //    }

        //}

        private void DrawRoute(ActivityRoute route, string route_name, Color color)
        {
            //GMapOverlay routes = new GMapOverlay("routes");
            //GMapOverlay route_overlay = new GMapOverlay(route_name);
            List<PointLatLng> points = new List<PointLatLng>();

            foreach (var point in route.latlngs)
            {
                points.Add(new PointLatLng(point[0], point[1]));
            }            

            GMapRoute groute = new GMapRoute(points, route_name);
            groute.Stroke = new Pen(color, 3);
            //groute.Stroke.Color = color;
            groute.Stroke.Width = 3;
            routes.Routes.Add(groute);
            Map.Overlays.Add(routes);



            // start and end marker
            var start_sign_bitmap = new Bitmap(Properties.Resources.start_marker);
            var end_sign_bitmap = new Bitmap(Properties.Resources.end_marker);
            //var tempmarker = new GMarkerGoogle(new PointLatLng(route.latlngs[0][0], route.latlngs[0][1]), new Bitmap());
            //var start_marker = new GMarkerGoogle(new PointLatLng(route.latlngs[0][0], route.latlngs[0][1]), GMarkerGoogleType.green_big_go);
            var start_marker = new GMarkerGoogle(new PointLatLng(route.latlngs[0][0], route.latlngs[0][1]), start_sign_bitmap);
            var end_marker = new GMarkerGoogle(new PointLatLng(route.latlngs[route.latlngs.Count - 1][0], route.latlngs[route.latlngs.Count - 1][1]), end_sign_bitmap);
            //start_marker.ToolTipText = "Start here";
            //end_marker.ToolTipText = "End here";
            start_marker.Size = new Size(32, 32);
            end_marker.Size = new Size(32, 32);
            start_marker.Offset = new Point(-16, -16);
            end_marker.Offset = new Point(-16, -16);
            markers.Markers.Add(start_marker);
            markers.Markers.Add(end_marker);

            Map.Position = points[0];
            //routes.IsVisibile = false;
            //routes.IsVisibile = true;
        }

        private void CreateElevationPlot()
        {
            //var plt = new ScottPlot.Plottable.ScatterPlot(streams.distances_lowres, streams.elevations_lowres);
            elevationSignaPlot = elevationPlot.Plot.AddSignalXY(streams.GetLowresStream("distance"), streams.GetLowresStream("altitude"));
            elevationSignaPlot.Color = Color.SpringGreen;
            elevationSignaPlot.LineWidth = 2;
            elevationSignaPlot.MarkerSize = 0;
            elevationSignaPlot.FillBelow(Color.SpringGreen, Color.Transparent);

            //this.elevationPlot.Plot.XAxis.Label("Distance [km]");
            //this.elevationPlot.Plot.YAxis.Label("Elevation [m]");

            elevationPlot.Plot.Add(elevationSignaPlot);

            elevationPlot.Plot.Add(highlightLine);
            elevationPlot.Plot.Add(highlightPolygon);
            elevationPlot.Plot.Add(infoToolTip);

            elevationPlot.Plot.SetAxisLimitsX(0, streams.GetStream("distance").Last());
            elevationPlot.Plot.AxisAutoY();

            elevationPlot.Refresh();
        }

        public void CreateMultiPlot()
        {
            try
            {
                var heartratePlot = new ScottPlot.Plottable.ScatterPlot(streams.GetLowresStream("distance"), streams.GetLowresStream("heartrate"));
                heartratePlot.Color = Color.Red;
                heartratePlot.LineWidth = 2;
                heartratePlot.MarkerSize = 0;
                multiPlot.Plot.Add(heartratePlot);
            }
            catch
            {
                MessageBox.Show("The Activity does not contain Heart Rate data\nHeart rate related data will not be displayed");
                hasHertrate = false;
            }
            
            multiPlot.Plot.Add(highlightLine);
            multiPlot.Plot.Add(highlightPolygon);

            multiPlot.Plot.SetAxisLimitsX(0, streams.GetStream("distance").Last());
            multiPlot.Plot.AxisAutoY();

            multiPlot.Plot.MatchLayout(elevationPlot.Plot, true, true);

            multiPlot.Refresh();
        }

        public void AddRouteGPX(string raw_gpx)
        {
            //ActivityRoute route = new ActivityRoute()
        }

        private void HighlightPoint(double distance)
        {
            highlightLine.X = distance;

            // TODO: This needs refactpring

            //find index in Streams
            int index = streams.IndexOfClosestDistance(distance);
            //(double mouseCoordX, _) = elevationPlot.GetMouseCoordinates();
            //(double pointX, double pointY, int index) = elevationSignaPlot.GetPointNearestX(mouseCoordX);


            //label string
            double elevation = streams.GetLowresStream("altitude")[index];
            TimeSpan time = TimeSpan.FromSeconds(streams.GetLowresStream("time")[index]);
            double heartrate = 0;
            if (hasHertrate)
            {
                heartrate = streams.GetLowresStream("heartrate")[index];
            }

            string info =
                String.Format("Distance: {0} km\n", Math.Round(distance, 2)) +
                String.Format("Elevation: {0} m\n", Math.Round(elevation, 2)) +
                "Time: " + time.ToString(@"hh\:mm\:ss") + "\n" +
                String.Format("Heartrate: {0} bpm\n", Math.Round(heartrate));

            infoToolTip.X = distance;
            //var x = elevationPlot.Plot.YAxis.GetSize();
            double yMax = elevationPlot.Plot.YAxis.Dims.Max;
            double yMin = elevationPlot.Plot.YAxis.Dims.Min;
            infoToolTip.Y = ((yMax + yMin) / 2) + ((yMax - yMin) * 0.2);
            infoToolTip.Label = info;

            try
            {
                //highlightLine.RenderLine();
                elevationPlot.Render();
                multiPlot.Render();
            }
            catch (Exception ex)
            {

            }

            // MAP           
            if (index > 0)
            {
                PointLatLng point = new PointLatLng(main_route.latlngs_lowres[index][0], main_route.latlngs_lowres[index][1]);
                highlight_marker.Position = point;
                //markers.Markers.Clear();
                //markers.Markers.Add(new GMarkerGoogle(point, GMarkerGoogleType.yellow));

            }
        }

        private void HighlightSection(int lapIndex)
        {
            if (lapIndex < 0)
            {
                return;
            }

            ActivityLap lap = laps.GetActivityLap(lapIndex);
            double start_distance = streams.GetStream("distance")[lap.start_index];
            double end_distance = streams.GetStream("distance")[lap.end_index];

            // draw poligon to highlight the section
            highlightPolygon.IsVisible = true;
            //double yMax = elevationPlot.Plot.YAxis.Dims.Max;
            //double yMin = elevationPlot.Plot.YAxis.Dims.Min;
            double yMax = 10000;
            double yMin = -100;
            double[] xs = {start_distance, end_distance, end_distance, start_distance};
            double[] ys = { yMin, yMin, yMax, yMax};

            highlightPolygon.Xs = xs;
            highlightPolygon.Ys = ys;

            elevationPlot.Render();
            multiPlot.Render();
        }

        private void CleanPointHighlight()
        {
            highlightLine.IsVisible = false;
            highlight_marker.IsVisible = false;
            infoToolTip.IsVisible = false;

            elevationPlot.Render();
            multiPlot.Render();
            //markers.Markers.Clear();
        }

        private void CleanSectionHighlight()
        {
            highlightPolygon.IsVisible = false;
            elevationPlot.Render();
            multiPlot.Render();
        }

        private void StartPointHighlight()
        {
            //System.Windows.Forms.Cursor.Hide();
            highlight_marker.IsVisible = true;
            highlightLine.IsVisible = true;
            infoToolTip.IsVisible = true;
            timer1.Start();
        }

        private void EndPointHighlight()
        {
            //System.Windows.Forms.Cursor.Show();
            timer1.Stop();
            CleanPointHighlight();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // distance needs to be determinrmined to highlight a point
            // it is determined either from the map or from the elevationPlot
            double distance;

            // cursor is not on the map => it must be on elevationPlot
            if (current_lat == null || current_lng == null)
            {
                (distance, _) = elevationPlot.GetMouseCoordinates();
            }

            else
            { 
                distance = streams.DistanceFromLatLng((double)current_lat, (double)current_lng, (float)0.2);
            }

            if (distance > 0)
            {
                //TODO: this is bad
                highlight_marker.IsVisible = true;
                highlightLine.IsVisible = true;
                infoToolTip.IsVisible = true;
                HighlightPoint(distance);
            }
            else
            {
                CleanPointHighlight();
            }

        }

        private void elevationPlot_MouseEnter(object sender, EventArgs e)
        {
            StartPointHighlight();
            CleanPointHighlight();
        }

        private void elevationPlot_MouseLeave(object sender, EventArgs e)
        {
            EndPointHighlight();
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

        private void lapsGridView_MouseLeave(object sender, EventArgs e)
        {
            CleanSectionHighlight();
            foreach (DataGridViewRow row in lapsGridView.Rows)
            {
                row.Selected = false;
            }
        }

        private void Map_MouseEnter(object sender, EventArgs e)
        {
            // also calls Map_MouseMove -> current_lat & lng will be set
            StartPointHighlight();
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            current_lat = Map.FromLocalToLatLng(e.X, e.Y).Lat;
            current_lng = Map.FromLocalToLatLng(e.X, e.Y).Lng;
        }

        private void Map_MouseLeave(object sender, EventArgs e)
        {
            current_lat = null;
            current_lng = null;
            EndPointHighlight();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddRouteButton_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            if (filePath != string.Empty) 
            {
                ActivityRoute new_route = new ActivityRoute(fileContent);
                DrawRoute(new_route, "route1", Color.Blue);
            }

        }
    }

}
