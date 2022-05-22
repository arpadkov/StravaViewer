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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BarPlot = new ScottPlot.FormsPlot();
            this.NextTimeButton = new System.Windows.Forms.Button();
            this.LastTimeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BarPlot
            // 
            this.BarPlot.Location = new System.Drawing.Point(138, 36);
            this.BarPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BarPlot.Name = "BarPlot";
            this.BarPlot.Size = new System.Drawing.Size(467, 346);
            this.BarPlot.TabIndex = 0;
            // 
            // NextTimeButton
            // 
            this.NextTimeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextTimeButton.Image = global::StravaViewer.Properties.Resources.button_right;
            this.NextTimeButton.Location = new System.Drawing.Point(549, 375);
            this.NextTimeButton.Name = "NextTimeButton";
            this.NextTimeButton.Size = new System.Drawing.Size(56, 53);
            this.NextTimeButton.TabIndex = 1;
            this.NextTimeButton.UseVisualStyleBackColor = true;
            this.NextTimeButton.Click += new System.EventHandler(this.NextTimeButton_Click);
            // 
            // LastTimeButton
            // 
            this.LastTimeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LastTimeButton.Image = global::StravaViewer.Properties.Resources.button_left;
            this.LastTimeButton.Location = new System.Drawing.Point(166, 375);
            this.LastTimeButton.Name = "LastTimeButton";
            this.LastTimeButton.Size = new System.Drawing.Size(56, 53);
            this.LastTimeButton.TabIndex = 1;
            this.LastTimeButton.UseVisualStyleBackColor = true;
            this.LastTimeButton.Click += new System.EventHandler(this.LastTimeButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.LastTimeButton);
            this.Controls.Add(this.NextTimeButton);
            this.Controls.Add(this.BarPlot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1500, 1500);
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "MainWindow";
            this.Text = "StravaViewer";
            this.Load += new System.EventHandler(this.LoadClient);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ScottPlot.FormsPlot BarPlot;
        private Button NextTimeButton;
        private Button LastTimeButton;
    }
}