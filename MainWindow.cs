using StravaViewer.Models;

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

        }

        private void LoadClient(object sender, EventArgs e)
        {
            Model.SetActivities();

            Model.DisplayTime = TimePeriod.FromYear(2022);
        }

        /* TODO Misi
         * - add 2 buttons
         * - bind them to the NextDisplayTime() and LastDisplayTime() methods
         * - make them fancy
         */


        private void ModelChanged(object sender, EventArgs e)
        {
            plot(Model.GetPlotData());
        }

        private void plot(PlotData plot_data)
        {
            if (plot_data.isDetailPlot)
            {
                plot_detail(plot_data);
            }
            else
            {
                plot_summary(plot_data);
            }

            BarPlot.Refresh();
        }

        private void plot_summary(PlotData plot_data)
        {
            foreach (double[] values in plot_data.Values)
            {
                BarPlot.Plot.AddBar(values, plot_data.Positions);
            }
            //BarPlot.Plot.AddBar(plot_data.Values, plot_data.Positions);
            BarPlot.Plot.XTicks(plot_data.Positions, plot_data.Labels);            
        }

        private void plot_detail(PlotData plot_data)
        {

        }

        private void NextTimeButton_Click(object sender, EventArgs e)
        {
            Model.NextDisplayTime();
        }

        private void LastTimeButton_Click(object sender, EventArgs e)
        {
            Model.LastDisplayTime();
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