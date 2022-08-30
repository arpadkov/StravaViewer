namespace StravaViewer.Forms
{
    partial class DetailedActivityView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.elevationPlot = new ScottPlot.FormsPlot();
            this.Map = new GMap.NET.WindowsForms.GMapControl();
            this.multiPlot = new ScottPlot.FormsPlot();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openStravaButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lapsGridView = new System.Windows.Forms.DataGridView();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MapPloSplitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PlotsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.AddRouteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lapsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapPloSplitContainer)).BeginInit();
            this.MapPloSplitContainer.Panel1.SuspendLayout();
            this.MapPloSplitContainer.Panel2.SuspendLayout();
            this.MapPloSplitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlotsSplitContainer)).BeginInit();
            this.PlotsSplitContainer.Panel1.SuspendLayout();
            this.PlotsSplitContainer.Panel2.SuspendLayout();
            this.PlotsSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // elevationPlot
            // 
            this.elevationPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elevationPlot.Location = new System.Drawing.Point(0, 0);
            this.elevationPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.elevationPlot.MinimumSize = new System.Drawing.Size(0, 150);
            this.elevationPlot.Name = "elevationPlot";
            this.elevationPlot.Size = new System.Drawing.Size(1190, 160);
            this.elevationPlot.TabIndex = 0;
            this.elevationPlot.MouseEnter += new System.EventHandler(this.elevationPlot_MouseEnter);
            this.elevationPlot.MouseLeave += new System.EventHandler(this.elevationPlot_MouseLeave);
            // 
            // Map
            // 
            this.Map.BackColor = System.Drawing.Color.Cyan;
            this.Map.Bearing = 0F;
            this.Map.CanDragMap = true;
            this.Map.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Map.EmptyTileColor = System.Drawing.Color.Navy;
            this.Map.GrayScaleMode = false;
            this.Map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.Map.LevelsKeepInMemory = 5;
            this.Map.Location = new System.Drawing.Point(0, 0);
            this.Map.MarkersEnabled = true;
            this.Map.MaxZoom = 18;
            this.Map.MinimumSize = new System.Drawing.Size(400, 200);
            this.Map.MinZoom = 2;
            this.Map.MouseWheelZoomEnabled = true;
            this.Map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.Map.Name = "Map";
            this.Map.NegativeMode = false;
            this.Map.PolygonsEnabled = true;
            this.Map.RetryLoadTile = 0;
            this.Map.RoutesEnabled = true;
            this.Map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.Map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.Map.ShowTileGridLines = false;
            this.Map.Size = new System.Drawing.Size(1190, 400);
            this.Map.TabIndex = 1;
            this.Map.Zoom = 13D;
            this.Map.MouseEnter += new System.EventHandler(this.Map_MouseEnter);
            this.Map.MouseLeave += new System.EventHandler(this.Map_MouseLeave);
            this.Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map_MouseMove);
            // 
            // multiPlot
            // 
            this.multiPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiPlot.Location = new System.Drawing.Point(0, 0);
            this.multiPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.multiPlot.MinimumSize = new System.Drawing.Size(0, 150);
            this.multiPlot.Name = "multiPlot";
            this.multiPlot.Size = new System.Drawing.Size(1190, 156);
            this.multiPlot.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openStravaButton
            // 
            this.openStravaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openStravaButton.Location = new System.Drawing.Point(87, 32);
            this.openStravaButton.Name = "openStravaButton";
            this.openStravaButton.Size = new System.Drawing.Size(108, 23);
            this.openStravaButton.TabIndex = 3;
            this.openStravaButton.Text = "Open in Strava";
            this.openStravaButton.UseVisualStyleBackColor = true;
            this.openStravaButton.Click += new System.EventHandler(this.openStravaButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(150, 3);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            165,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 23);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Plot/Map refresh rate [Hz]";
            // 
            // lapsGridView
            // 
            this.lapsGridView.AllowUserToAddRows = false;
            this.lapsGridView.AllowUserToDeleteRows = false;
            this.lapsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lapsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lapsGridView.Location = new System.Drawing.Point(0, 0);
            this.lapsGridView.MinimumSize = new System.Drawing.Size(150, 0);
            this.lapsGridView.Name = "lapsGridView";
            this.lapsGridView.ReadOnly = true;
            this.lapsGridView.RowHeadersVisible = false;
            this.lapsGridView.RowTemplate.Height = 25;
            this.lapsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lapsGridView.Size = new System.Drawing.Size(328, 724);
            this.lapsGridView.TabIndex = 7;
            this.lapsGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.lapsGridView_CellMouseEnter);
            this.lapsGridView.MouseLeave += new System.EventHandler(this.lapsGridView_MouseLeave);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.MapPloSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.lapsGridView);
            this.mainSplitContainer.Size = new System.Drawing.Size(1526, 726);
            this.mainSplitContainer.SplitterDistance = 1192;
            this.mainSplitContainer.TabIndex = 8;
            this.mainSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // MapPloSplitContainer
            // 
            this.MapPloSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapPloSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MapPloSplitContainer.MinimumSize = new System.Drawing.Size(0, 400);
            this.MapPloSplitContainer.Name = "MapPloSplitContainer";
            this.MapPloSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MapPloSplitContainer.Panel1
            // 
            this.MapPloSplitContainer.Panel1.Controls.Add(this.AddRouteButton);
            this.MapPloSplitContainer.Panel1.Controls.Add(this.panel1);
            this.MapPloSplitContainer.Panel1.Controls.Add(this.Map);
            // 
            // MapPloSplitContainer.Panel2
            // 
            this.MapPloSplitContainer.Panel2.Controls.Add(this.PlotsSplitContainer);
            this.MapPloSplitContainer.Size = new System.Drawing.Size(1190, 724);
            this.MapPloSplitContainer.SplitterDistance = 400;
            this.MapPloSplitContainer.TabIndex = 0;
            this.MapPloSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.openStravaButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Location = new System.Drawing.Point(980, 317);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 68);
            this.panel1.TabIndex = 8;
            // 
            // PlotsSplitContainer
            // 
            this.PlotsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlotsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.PlotsSplitContainer.Name = "PlotsSplitContainer";
            this.PlotsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PlotsSplitContainer.Panel1
            // 
            this.PlotsSplitContainer.Panel1.Controls.Add(this.elevationPlot);
            this.PlotsSplitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer3_Panel1_Paint);
            // 
            // PlotsSplitContainer.Panel2
            // 
            this.PlotsSplitContainer.Panel2.Controls.Add(this.multiPlot);
            this.PlotsSplitContainer.Size = new System.Drawing.Size(1190, 320);
            this.PlotsSplitContainer.SplitterDistance = 160;
            this.PlotsSplitContainer.TabIndex = 0;
            // 
            // AddRouteButton
            // 
            this.AddRouteButton.BackgroundImage = global::StravaViewer.Properties.Resources.plus_sign;
            this.AddRouteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddRouteButton.Location = new System.Drawing.Point(16, 17);
            this.AddRouteButton.Name = "AddRouteButton";
            this.AddRouteButton.Size = new System.Drawing.Size(30, 30);
            this.AddRouteButton.TabIndex = 9;
            this.AddRouteButton.UseVisualStyleBackColor = true;
            this.AddRouteButton.Click += new System.EventHandler(this.AddRouteButton_Click);
            // 
            // DetailedActivityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1526, 726);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "DetailedActivityView";
            this.Text = "DetailedActivityView";
            this.Load += new System.EventHandler(this.DetailedActivityView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lapsGridView)).EndInit();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.MapPloSplitContainer.Panel1.ResumeLayout(false);
            this.MapPloSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MapPloSplitContainer)).EndInit();
            this.MapPloSplitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PlotsSplitContainer.Panel1.ResumeLayout(false);
            this.PlotsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PlotsSplitContainer)).EndInit();
            this.PlotsSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.FormsPlot elevationPlot;
        private GMap.NET.WindowsForms.GMapControl Map;
        private ScottPlot.FormsPlot multiPlot;
        private System.Windows.Forms.Timer timer1;
        private Button openStravaButton;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private DataGridView lapsGridView;
        private SplitContainer mainSplitContainer;
        private SplitContainer MapPloSplitContainer;
        private Panel panel1;
        private SplitContainer PlotsSplitContainer;
        private Button AddRouteButton;
    }
}