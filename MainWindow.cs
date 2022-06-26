using StravaViewer.Models;
using StravaViewer.Models.AbstractPlot;
using StravaViewer.Forms;
using ScottPlot;

namespace StravaViewer
{
    public partial class MainWindow : Form
    {
        ActivityModel Model;
        List<ScottPlot.Plottable.ScatterPlot> HighlightLines = new List<ScottPlot.Plottable.ScatterPlot>();

        public MainWindow()
        {
            InitializeComponent();

            BarPlot.Configuration.LeftClickDragPan = false;
            BarPlot.Configuration.MiddleClickAutoAxis = false;
            BarPlot.Configuration.MiddleClickDragZoom = false;
            BarPlot.Configuration.RightClickDragZoom = false;
            BarPlot.Configuration.ScrollWheelZoom = false;
            BarPlot.Configuration.DoubleClickBenchmark = false;
            BarPlot.Configuration.LockHorizontalAxis = true;
            BarPlot.Configuration.LockVerticalAxis = true;


            //this.Size = new System.Drawing.Size(1200, 900);


            this.Model = new ActivityModel();
            this.Model.ModelChanged += ModelChanged;

            this.infoTypeCombo.DataSource = Enum.GetValues(typeof(InfoType));
            this.activityTypeCombo.DataSource = Enum.GetValues(typeof(ActivityType));

            //Model.Client.GetActivityStream("7351331006");

            plot();
        }

        private void LoadClient(object sender, EventArgs e)
        {
            //Model.SetActivities();

            //Model.DisplayTime = TimePeriod.FromYear(2022);

            //Model.ModelChanged += ModelChanged;
        }

        /* TODO Misi
         * - add 2 buttons
         * - bind them to the NextDisplayTime() and LastDisplayTime() methods
         * - make them fancy
         */


        private void ModelChanged(object sender, EventArgs e)
        {
            if (this.Model.AbstractPlot != null && !this.Model.AbstractPlot.PlotData.IsEmpty)
            {
                plot();
            }
           
        }

        private void plot()
        {
            BarPlot.Plot.Clear();
            CleanInfoPanel();

            // Reversing the list, to plot the highest bar first
            this.Model.AbstractPlot.PlotData.valueSeries.Reverse();

            foreach (double[] values in this.Model.AbstractPlot.PlotData.valueSeries)
            {
                var plot = BarPlot.Plot.AddBar(values, this.Model.AbstractPlot.PlotData.Positions);
                plot.BarWidth = 0.9;
            }

            BarPlot.Plot.XTicks(this.Model.AbstractPlot.PlotData.Positions, this.Model.AbstractPlot.PlotData.Labels);

            BarPlot.Plot.Title(this.Model.AbstractPlot.PlotData.Title);

            // DrawBoundingRectangles();

            BarPlot.Refresh();
        }

        private void NextTimeButton_Click(object sender, EventArgs e)
        {
            this.Model.NextDisplayTime();
        }

        private void LastTimeButton_Click(object sender, EventArgs e)
        {
            this.Model.LastDisplayTime();
        }

        private void CleanInfoPanel()
        {
            InfoPanelNameValue.Text = "Nothing selected";
            InfoPanelDateValue.Text = "-";
            InfoPanelDistanceValue.Text = "-";
            InfoPanelDurationValue.Text = "-";
            InfoPanelElevationGainValue.Text = "-";
        }

        private void DisplayDetails(ActivityCollection collection)
        {
            InfoPanelNameValue.Text = collection.Name;
            InfoPanelDateValue.Text = collection.Date;
            InfoPanelDistanceValue.Text = collection.Distance;
            InfoPanelDurationValue.Text = collection.MovingDuration;
            InfoPanelElevationGainValue.Text = collection.ElevationGain;
        }

        private void DisplayDetails(Activity activity)
        {
            InfoPanelNameValue.Text = activity.Name;
            InfoPanelDateValue.Text = activity.Date;
            InfoPanelDistanceValue.Text = activity.Distance;
            InfoPanelDurationValue.Text = activity.MovingDuration;
            InfoPanelElevationGainValue.Text = activity.ElevationGain;
        }


        private void BarPlot_MouseDown(object sender, MouseEventArgs e)
        {
            (double x, double y) = BarPlot.GetMouseCoordinates();
            x = Math.Round(x, 2);
            y = Math.Round(y, 2);
            clickCoordLabel.Text = "Click Coordinates\n" + x.ToString() + " : " + y.ToString();

            foreach (ActivityCollection collection in this.Model.AbstractPlot.activityCollections)
            {
                // the corresponding ActivityCollection is clicked
                if (collection.BoundingRectangle.Contains(x, y))
                {
                    InfoPanelNameValue.Text = "Activity / Collection details:\n" + collection.ToString();
                    DisplayDetails(collection);
                    DrawBoundingRectangle(collection.BoundingRectangle);
                    return;
                }

                // no ActivityCollection is clicked, an Activity may be clicked
                foreach (Activity activity in collection.activities)
                {
                    if (activity.BoundingRectangle.Contains(x, y))
                    {
                        InfoPanelNameValue.Text = "Activity / Collection details:\n" + activity.ToString();
                        DisplayDetails(activity);
                        DrawBoundingRectangle(activity.BoundingRectangle);
                        return;
                    }
                }
            }

            CleanBoundingRectangles();
            CleanInfoPanel();

        }

        private void BarPlot_MouseMove(object sender, MouseEventArgs e)
        {
            (double x, double y) = BarPlot.GetMouseCoordinates();
            x = Math.Round(x, 2);
            y = Math.Round(y, 2);
            moveCoordinatesLabel.Text = "Mouse Coordinates\n" + x.ToString() + " : " + y.ToString();
        }

        private void DrawBoundingRectangles()
        {
            if (this.Model.PlotType == PlotType.MonthDetail)
            {
                foreach (ActivityCollection actCollection in this.Model.AbstractPlot.activityCollections)
                {
                    foreach (Activity act in actCollection.activities)
                    {
                        DrawBoundingRectangle(act.BoundingRectangle);
                    }
                }

            }
            else if (this.Model.PlotType == PlotType.YearlySummary || Model.PlotType == PlotType.MonthlySummary){
                foreach (ActivityCollection actCollection in this.Model.AbstractPlot.activityCollections)
                {
                    DrawBoundingRectangle(actCollection.BoundingRectangle);
                }
            }
        }

        private void DrawBoundingRectangle(BoundingRectangle rect)
        {
            CleanBoundingRectangles();

            PlotLine(rect.left, rect.bottom, rect.right, rect.bottom); //bottom
            PlotLine(rect.left, rect.top, rect.right, rect.top); //top
            PlotLine(rect.left, rect.bottom, rect.left, rect.top); //left
            PlotLine(rect.right, rect.bottom, rect.right, rect.top); //right
        }

        private void CleanBoundingRectangles()
        {
            foreach (ScottPlot.Plottable.ScatterPlot line in HighlightLines)
            {
                line.IsVisible = false;
            }
            HighlightLines.Clear();
        }

        private void PlotLine(double x1, double y1, double x2, double y2)
        {
            ScottPlot.Plottable.ScatterPlot line = BarPlot.Plot.AddLine(x1, y1, x2, y2, Color.Black, lineWidth: 5);
            line.LineStyle = LineStyle.Dot;
            line.IsVisible = true;
            HighlightLines.Add(line);
        }

        private void BarPlot_DoubleClick(object sender, EventArgs e)
        {
            CleanInfoPanel();

            (double x, double y) = BarPlot.GetMouseCoordinates();

            foreach (ActivityCollection collection in Model.ActivityCollections)
            {
                if (collection.BoundingRectangle.Contains(x, y))
                {
                    this.Model.DrillDown(collection);
                    return;
                }

                // no ActivityCollection is clicked, an Activity may be clicked
                foreach (Activity activity in collection.activities)
                {
                    if (activity.BoundingRectangle.Contains(x, y))
                    {
                        //JArray LatLngStream = Model.Client.GetActivityStream(activity.id, "latlng");
                        OpenDetailedActivityView(activity);

                        //activity.OpenInBrowser();
                        return;
                    }
                }
            }
        }

        private void OpenDetailedActivityView(Activity activity)
        {
            DetailedActivityView detailedActivityView = new DetailedActivityView(
                activity,
                Model.Client.GetActivityStream(activity.id, "latlng"),
                Model.Client.GetActivityStream(activity.id, "distance"),
                Model.Client.GetActivityStream(activity.id, "altitude"),
                Model.Client.GetActivityStream(activity.id, "heartrate")
                );
            detailedActivityView.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Model.PlotType = PlotType.YearlySummary;
            this.Model.DisplayTime = Model.InitializeDisplaytime();
        }

        private void infoTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Model.InfoType = (InfoType) infoTypeCombo.SelectedItem;
        }

        private void activityTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Model.ActivityType = (ActivityType) activityTypeCombo.SelectedItem;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            Model.Client.UploadActivities();
        }

        private void fullInitButton_Click(object sender, EventArgs e)
        {
            Model.Client.GetAllActivities(fullInit: true);
        }

        /*#region Trash

        private void BarPlot_MouseMove(object sender, MouseEventArgs e)
        {
            (double x, double y) = BarPlot.GetMouseCoordinates();
            mouseCoordLabel.Text = x.ToString() + "\n" + y.ToString();

            mousePosPxLabel.Text = "Mouse: \n" +
                Convert.ToString(Cursor.Position.X + " - " + Cursor.Position.Y);


            SetCoordsLabel();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BarPlot_Move(object sender, EventArgs e)
        {
            positionLabel.Text = "MOVED";
        }

        private void BarPlot_Resize(object sender, EventArgs e)
        {

            int height = BarPlot.Height;
            int width = BarPlot.Width;
            int bottom = BarPlot.Bottom;
            int top = BarPlot.Top;
            int left = BarPlot.Left;
            int right = BarPlot.Right;

            sizeLabel.Text = "Height: " + height.ToString() + "\n" +
                "Width: " + width.ToString() + "\n" +
                "Bottom: " + bottom.ToString() + "\n" +
                "Top: " + top.ToString() + "\n" +
                "Left: " + left.ToString() + "\n" +
                "Right: " + right.ToString() + "\n";

            SetCoordsLabel();
        }

        private void MainWindow_Move(object sender, EventArgs e)
        {
            int top = this.Top;
            int bottom = this.Bottom;
            int left = this.Left;
            int right = this.Right;

            windowPositionLabel.Text =
                "Top: " + top.ToString() + "\n" +
                "Bottom: " + bottom.ToString() + "\n" +
                "Left: " + left.ToString() + "\n" +
                "Right: " + right.ToString() + "\n";

            SetCoordsLabel();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosPxLabel.Text = "Mouse pixels: \n" + 
                Convert.ToString(Cursor.Position.X + " - " + Cursor.Position.Y);
        }

        private void SetCoordsLabel()
        {
            float offsetX = BarPlot.plt.XAxis.Dims.DataOffsetPx;

            calculatedCoordsLabel.Text = (BarPlot.plt.XAxis.Dims.Min * BarPlot.plt.XAxis.Dims.PxPerUnit).ToString();
        }
        #endregion*/

    }
}