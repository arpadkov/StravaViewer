namespace StravaViewer
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.BarPlot = new ScottPlot.FormsPlot();
            this.mouseCoordLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            this.windowPositionLabel = new System.Windows.Forms.Label();
            this.mousePosPxLabel = new System.Windows.Forms.Label();
            this.calculatedCoordsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ActsSet";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // BarPlot
            // 
            this.BarPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BarPlot.Location = new System.Drawing.Point(13, 44);
            this.BarPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BarPlot.Name = "BarPlot";
            this.BarPlot.Size = new System.Drawing.Size(567, 368);
            this.BarPlot.TabIndex = 1;
            this.BarPlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BarPlot_MouseMove);
            this.BarPlot.Move += new System.EventHandler(this.BarPlot_Move);
            this.BarPlot.Resize += new System.EventHandler(this.BarPlot_Resize);
            // 
            // mouseCoordLabel
            // 
            this.mouseCoordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mouseCoordLabel.AutoSize = true;
            this.mouseCoordLabel.Location = new System.Drawing.Point(657, 46);
            this.mouseCoordLabel.Name = "mouseCoordLabel";
            this.mouseCoordLabel.Size = new System.Drawing.Size(74, 15);
            this.mouseCoordLabel.TabIndex = 2;
            this.mouseCoordLabel.Text = "MouseCords";
            // 
            // sizeLabel
            // 
            this.sizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(657, 92);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(67, 15);
            this.sizeLabel.TabIndex = 3;
            this.sizeLabel.Text = "ResizeLabel";
            // 
            // positionLabel
            // 
            this.positionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(657, 196);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(78, 15);
            this.positionLabel.TabIndex = 4;
            this.positionLabel.Text = "positionLabel";
            // 
            // windowPositionLabel
            // 
            this.windowPositionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.windowPositionLabel.AutoSize = true;
            this.windowPositionLabel.Location = new System.Drawing.Point(657, 247);
            this.windowPositionLabel.Name = "windowPositionLabel";
            this.windowPositionLabel.Size = new System.Drawing.Size(94, 15);
            this.windowPositionLabel.TabIndex = 5;
            this.windowPositionLabel.Text = "WindowPosition";
            // 
            // mousePosPxLabel
            // 
            this.mousePosPxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mousePosPxLabel.AutoSize = true;
            this.mousePosPxLabel.Location = new System.Drawing.Point(657, 327);
            this.mousePosPxLabel.Name = "mousePosPxLabel";
            this.mousePosPxLabel.Size = new System.Drawing.Size(75, 15);
            this.mousePosPxLabel.TabIndex = 6;
            this.mousePosPxLabel.Text = "MousePosPx";
            // 
            // calculatedCoordsLabel
            // 
            this.calculatedCoordsLabel.AutoSize = true;
            this.calculatedCoordsLabel.Location = new System.Drawing.Point(657, 375);
            this.calculatedCoordsLabel.Name = "calculatedCoordsLabel";
            this.calculatedCoordsLabel.Size = new System.Drawing.Size(101, 15);
            this.calculatedCoordsLabel.TabIndex = 7;
            this.calculatedCoordsLabel.Text = "CalculatedCoords";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.calculatedCoordsLabel);
            this.Controls.Add(this.mousePosPxLabel);
            this.Controls.Add(this.windowPositionLabel);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.mouseCoordLabel);
            this.Controls.Add(this.BarPlot);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1500, 1500);
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "MainWindow";
            this.Text = "StravaViewer";
            this.Load += new System.EventHandler(this.LoadClient);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.Move += new System.EventHandler(this.MainWindow_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;

        public void SetLabel(string text)
        {
            label1.Text = text;
        }

        private ScottPlot.FormsPlot BarPlot;
        private Label mouseCoordLabel;
        private Label sizeLabel;
        private Label positionLabel;
        private Label windowPositionLabel;
        private Label mousePosPxLabel;
        private Label calculatedCoordsLabel;
    }
}