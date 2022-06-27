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
            this.SuspendLayout();
            // 
            // elevationPlot
            // 
            this.elevationPlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elevationPlot.Location = new System.Drawing.Point(28, 512);
            this.elevationPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.elevationPlot.Name = "elevationPlot";
            this.elevationPlot.Size = new System.Drawing.Size(1139, 196);
            this.elevationPlot.TabIndex = 0;
            this.elevationPlot.MouseEnter += new System.EventHandler(this.elevationPlot_MouseEnter);
            this.elevationPlot.MouseLeave += new System.EventHandler(this.elevationPlot_MouseLeave);
            this.elevationPlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.elevationPlot_MouseMove);
            // 
            // Map
            // 
            this.Map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Map.Bearing = 0F;
            this.Map.CanDragMap = true;
            this.Map.EmptyTileColor = System.Drawing.Color.Navy;
            this.Map.GrayScaleMode = false;
            this.Map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.Map.LevelsKeepInMemory = 5;
            this.Map.Location = new System.Drawing.Point(95, 12);
            this.Map.MarkersEnabled = true;
            this.Map.MaxZoom = 18;
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
            this.Map.Size = new System.Drawing.Size(839, 494);
            this.Map.TabIndex = 1;
            this.Map.Zoom = 13D;
            this.Map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Map_MouseClick);
            // 
            // multiPlot
            // 
            this.multiPlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiPlot.Location = new System.Drawing.Point(28, 693);
            this.multiPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.multiPlot.Name = "multiPlot";
            this.multiPlot.Size = new System.Drawing.Size(1139, 199);
            this.multiPlot.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openStravaButton
            // 
            this.openStravaButton.Location = new System.Drawing.Point(990, 483);
            this.openStravaButton.Name = "openStravaButton";
            this.openStravaButton.Size = new System.Drawing.Size(108, 23);
            this.openStravaButton.TabIndex = 3;
            this.openStravaButton.Text = "Open in Strava";
            this.openStravaButton.UseVisualStyleBackColor = true;
            this.openStravaButton.Click += new System.EventHandler(this.openStravaButton_Click);
            // 
            // DetailedActivityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 887);
            this.Controls.Add(this.openStravaButton);
            this.Controls.Add(this.multiPlot);
            this.Controls.Add(this.Map);
            this.Controls.Add(this.elevationPlot);
            this.Name = "DetailedActivityView";
            this.Text = "DetailedActivityView";
            this.Load += new System.EventHandler(this.DetailedActivityView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.FormsPlot elevationPlot;
        private GMap.NET.WindowsForms.GMapControl Map;
        private ScottPlot.FormsPlot multiPlot;
        private System.Windows.Forms.Timer timer1;
        private Button openStravaButton;
    }
}