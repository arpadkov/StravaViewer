using StravaViewer.Models;
using StravaViewer.Models.AbstractPlot;

namespace StravaViewer
{
    public partial class MainWindow : Form
    {
        ActivityModel Model;

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
            Model.ModelChanged += ModelChanged;

            this.infoTypeCombo.DataSource = Enum.GetValues(typeof(InfoType));

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
            if (Model.AbstractPlot != null && !Model.AbstractPlot.PlotData.IsEmpty)
            {
                plot();
            }
           
        }

        private void plot()
        {
            BarPlot.Plot.Clear();

            foreach (double[] values in Model.AbstractPlot.PlotData.valueSeries)
            {
                BarPlot.Plot.AddBar(values, Model.AbstractPlot.PlotData.Positions);
            }

            BarPlot.Plot.XTicks(Model.AbstractPlot.PlotData.Positions, Model.AbstractPlot.PlotData.Labels);

            BarPlot.Plot.Title(Model.AbstractPlot.PlotData.Title);

            DrawBoundingRectangles();

            BarPlot.Refresh();
        }

        //private void plot_summary()
        //{
        //    foreach (double[] values in plot_data.valueSeries)
        //    {
        //        BarPlot.Plot.AddBar(values, plot_data.Positions);
        //    }

        //    BarPlot.Plot.XTicks(plot_data.Positions, plot_data.Labels);            
        //}

        //private void plot_detail()
        //{

        //}

        private void NextTimeButton_Click(object sender, EventArgs e)
        {
            Model.NextDisplayTime();
        }

        private void LastTimeButton_Click(object sender, EventArgs e)
        {
            Model.LastDisplayTime();
        }

        private void DisplayDetails()
        {
            (double x, double y) = BarPlot.GetMouseCoordinates();

            if (Model.PlotType == PlotType.MonthDetail)
            {
                DisplayActivityDetails(x, y);
            }
            else
            {
                DisplayCollectionDetails(x, y);
            }
        }

        private void DisplayCollectionDetails(double x, double y)
        {
            foreach (Models.AbstractPlot.ActivityCollection collection in Model.AbstractPlot.activityCollections)
            {
                if (collection.BoundingRectangle.Contains(x, y))
                {
                    detailLabel.Text = "Activity / Collection details:\n" + collection.ToString();
                }
            }
        }

        private void DisplayActivityDetails(double x, double y)
        {

            foreach (ActivityCollection actCollection in Model.AbstractPlot.activityCollections)
            {
                foreach (Activity activity in actCollection.activities)
                    {
                        if (activity.BoundingRectangle.Contains(x, y))
                        {
                            detailLabel.Text = "Activity / Collection details:\n" + activity.ToString();
                        }
                    }
            }                
        }


        private void BarPlot_MouseDown(object sender, MouseEventArgs e)
        {
            (double x, double y) = BarPlot.GetMouseCoordinates();
            x = Math.Round(x, 2);
            y = Math.Round(y, 2);
            clickCoordLabel.Text = "Click Coordinates\n" + x.ToString() + " : " + y.ToString();

            DisplayDetails();
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
            if (Model.PlotType == PlotType.MonthDetail)
            {
                foreach (ActivityCollection actCollection in Model.AbstractPlot.activityCollections)
                {
                    foreach (Activity act in actCollection.activities)
                    {
                        DrawBoundingRectangle(act.BoundingRectangle);
                    }
                }

            }
            else if (Model.PlotType == PlotType.YearlySummary || Model.PlotType == PlotType.MonthlySummary){
                foreach (ActivityCollection actCollection in Model.AbstractPlot.activityCollections)
                {
                    DrawBoundingRectangle(actCollection.BoundingRectangle);
                }
            }
        }

        private void DrawBoundingRectangle(BoundingRectangle rect)
        {
            PlotLine(rect.left, rect.bottom, rect.right, rect.bottom); //bottom
            PlotLine(rect.left, rect.top, rect.right, rect.top); //top
            PlotLine(rect.left, rect.bottom, rect.left, rect.top); //left
            PlotLine(rect.right, rect.bottom, rect.right, rect.top); //right
        }

        private void PlotLine(double x1, double y1, double x2, double y2)
        {
            var vLine = BarPlot.Plot.AddLine(x1, y1, x2, y2, Color.Red, lineWidth: 3);
        }

        private void BarPlot_DoubleClick(object sender, EventArgs e)
        {
            (double x, double y) = BarPlot.GetMouseCoordinates();

            foreach (ActivityCollection actCollection in Model.ActivityCollections)
            {
                if (actCollection.BoundingRectangle.Contains(x, y))
                {
                    Model.DrillDown(actCollection);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.PlotType = PlotType.YearlySummary;
            Model.DisplayTime = Model.InitializeDisplaytime();
        }

        private void infoTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Model.InfoType = (InfoType) infoTypeCombo.SelectedItem;
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