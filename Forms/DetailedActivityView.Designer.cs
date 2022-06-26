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
            this.elevationPlot = new ScottPlot.FormsPlot();
            this.Map = new GMap.NET.WindowsForms.GMapControl();
            this.multiPlot = new ScottPlot.FormsPlot();
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
            this.multiPlot.Location = new System.Drawing.Point(28, 697);
            this.multiPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.multiPlot.Name = "multiPlot";
            this.multiPlot.Size = new System.Drawing.Size(1139, 199);
            this.multiPlot.TabIndex = 2;
            // 
            // DetailedActivityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 887);
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
    }
}