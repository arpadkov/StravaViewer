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
            this.BarPlot = new ScottPlot.FormsPlot();
            this.NextTimeButton = new System.Windows.Forms.Button();
            this.LastTimeButton = new System.Windows.Forms.Button();
            this.clickCoordLabel = new System.Windows.Forms.Label();
            this.moveCoordinatesLabel = new System.Windows.Forms.Label();
            this.InfoPanelNameValue = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.infoTypeCombo = new System.Windows.Forms.ComboBox();
            this.activityTypeCombo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.InfoPanelDurationValue = new System.Windows.Forms.Label();
            this.InfoPanelElevationGainValue = new System.Windows.Forms.Label();
            this.InfoPanelDistanceValue = new System.Windows.Forms.Label();
            this.InfoPanelDateValue = new System.Windows.Forms.Label();
            this.InfoPanelDurationLabel = new System.Windows.Forms.Label();
            this.InfoPanelElevationGainLabel = new System.Windows.Forms.Label();
            this.InfoPanelDistanceLabel = new System.Windows.Forms.Label();
            this.InfoPanelDateLabel = new System.Windows.Forms.Label();
            this.uploadButton = new System.Windows.Forms.Button();
            this.fullInitButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarPlot
            // 
            this.BarPlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BarPlot.Location = new System.Drawing.Point(-3, 23);
            this.BarPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BarPlot.Name = "BarPlot";
            this.BarPlot.Size = new System.Drawing.Size(687, 406);
            this.BarPlot.TabIndex = 0;
            this.BarPlot.DoubleClick += new System.EventHandler(this.BarPlot_DoubleClick);
            this.BarPlot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarPlot_MouseDown);
            this.BarPlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BarPlot_MouseMove);
            // 
            // NextTimeButton
            // 
            this.NextTimeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextTimeButton.Image = global::StravaViewer.Properties.Resources.button_right;
            this.NextTimeButton.Location = new System.Drawing.Point(125, 435);
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
            this.LastTimeButton.Location = new System.Drawing.Point(35, 435);
            this.LastTimeButton.Name = "LastTimeButton";
            this.LastTimeButton.Size = new System.Drawing.Size(56, 53);
            this.LastTimeButton.TabIndex = 1;
            this.LastTimeButton.UseVisualStyleBackColor = true;
            this.LastTimeButton.Click += new System.EventHandler(this.LastTimeButton_Click);
            // 
            // clickCoordLabel
            // 
            this.clickCoordLabel.AutoSize = true;
            this.clickCoordLabel.Location = new System.Drawing.Point(210, 435);
            this.clickCoordLabel.Name = "clickCoordLabel";
            this.clickCoordLabel.Size = new System.Drawing.Size(100, 15);
            this.clickCoordLabel.TabIndex = 2;
            this.clickCoordLabel.Text = "Click Coordinates";
            // 
            // moveCoordinatesLabel
            // 
            this.moveCoordinatesLabel.AutoSize = true;
            this.moveCoordinatesLabel.Location = new System.Drawing.Point(346, 435);
            this.moveCoordinatesLabel.Name = "moveCoordinatesLabel";
            this.moveCoordinatesLabel.Size = new System.Drawing.Size(110, 15);
            this.moveCoordinatesLabel.TabIndex = 3;
            this.moveCoordinatesLabel.Text = "Mouse Coordinates";
            // 
            // InfoPanelNameValue
            // 
            this.InfoPanelNameValue.AutoSize = true;
            this.InfoPanelNameValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelNameValue.Location = new System.Drawing.Point(3, 2);
            this.InfoPanelNameValue.Name = "InfoPanelNameValue";
            this.InfoPanelNameValue.Size = new System.Drawing.Size(141, 21);
            this.InfoPanelNameValue.TabIndex = 4;
            this.InfoPanelNameValue.Text = "Nothing selected";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(678, 319);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 29);
            this.button1.TabIndex = 5;
            this.button1.Text = "Show All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // infoTypeCombo
            // 
            this.infoTypeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infoTypeCombo.FormattingEnabled = true;
            this.infoTypeCombo.Location = new System.Drawing.Point(677, 232);
            this.infoTypeCombo.Name = "infoTypeCombo";
            this.infoTypeCombo.Size = new System.Drawing.Size(121, 23);
            this.infoTypeCombo.TabIndex = 6;
            this.infoTypeCombo.SelectedIndexChanged += new System.EventHandler(this.infoTypeCombo_SelectedIndexChanged);
            // 
            // activityTypeCombo
            // 
            this.activityTypeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activityTypeCombo.FormattingEnabled = true;
            this.activityTypeCombo.Location = new System.Drawing.Point(677, 274);
            this.activityTypeCombo.Name = "activityTypeCombo";
            this.activityTypeCombo.Size = new System.Drawing.Size(121, 23);
            this.activityTypeCombo.TabIndex = 7;
            this.activityTypeCombo.SelectedIndexChanged += new System.EventHandler(this.activityTypeCombo_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.InfoPanelDurationValue);
            this.panel1.Controls.Add(this.InfoPanelElevationGainValue);
            this.panel1.Controls.Add(this.InfoPanelDistanceValue);
            this.panel1.Controls.Add(this.InfoPanelDateValue);
            this.panel1.Controls.Add(this.InfoPanelDurationLabel);
            this.panel1.Controls.Add(this.InfoPanelElevationGainLabel);
            this.panel1.Controls.Add(this.InfoPanelDistanceLabel);
            this.panel1.Controls.Add(this.InfoPanelDateLabel);
            this.panel1.Controls.Add(this.InfoPanelNameValue);
            this.panel1.Location = new System.Drawing.Point(674, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 170);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // InfoPanelDurationValue
            // 
            this.InfoPanelDurationValue.AutoSize = true;
            this.InfoPanelDurationValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelDurationValue.Location = new System.Drawing.Point(145, 124);
            this.InfoPanelDurationValue.Name = "InfoPanelDurationValue";
            this.InfoPanelDurationValue.Size = new System.Drawing.Size(57, 21);
            this.InfoPanelDurationValue.TabIndex = 12;
            this.InfoPanelDurationValue.Text = "label7";
            // 
            // InfoPanelElevationGainValue
            // 
            this.InfoPanelElevationGainValue.AutoSize = true;
            this.InfoPanelElevationGainValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelElevationGainValue.Location = new System.Drawing.Point(145, 90);
            this.InfoPanelElevationGainValue.Name = "InfoPanelElevationGainValue";
            this.InfoPanelElevationGainValue.Size = new System.Drawing.Size(57, 21);
            this.InfoPanelElevationGainValue.TabIndex = 11;
            this.InfoPanelElevationGainValue.Text = "label6";
            // 
            // InfoPanelDistanceValue
            // 
            this.InfoPanelDistanceValue.AutoSize = true;
            this.InfoPanelDistanceValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelDistanceValue.Location = new System.Drawing.Point(145, 59);
            this.InfoPanelDistanceValue.Name = "InfoPanelDistanceValue";
            this.InfoPanelDistanceValue.Size = new System.Drawing.Size(57, 21);
            this.InfoPanelDistanceValue.TabIndex = 10;
            this.InfoPanelDistanceValue.Text = "label5";
            // 
            // InfoPanelDateValue
            // 
            this.InfoPanelDateValue.AutoSize = true;
            this.InfoPanelDateValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelDateValue.Location = new System.Drawing.Point(145, 27);
            this.InfoPanelDateValue.Name = "InfoPanelDateValue";
            this.InfoPanelDateValue.Size = new System.Drawing.Size(57, 21);
            this.InfoPanelDateValue.TabIndex = 9;
            this.InfoPanelDateValue.Text = "label4";
            // 
            // InfoPanelDurationLabel
            // 
            this.InfoPanelDurationLabel.AutoSize = true;
            this.InfoPanelDurationLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelDurationLabel.Location = new System.Drawing.Point(3, 124);
            this.InfoPanelDurationLabel.Name = "InfoPanelDurationLabel";
            this.InfoPanelDurationLabel.Size = new System.Drawing.Size(145, 21);
            this.InfoPanelDurationLabel.TabIndex = 8;
            this.InfoPanelDurationLabel.Text = "Moving Duration:";
            // 
            // InfoPanelElevationGainLabel
            // 
            this.InfoPanelElevationGainLabel.AutoSize = true;
            this.InfoPanelElevationGainLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelElevationGainLabel.Location = new System.Drawing.Point(3, 90);
            this.InfoPanelElevationGainLabel.Name = "InfoPanelElevationGainLabel";
            this.InfoPanelElevationGainLabel.Size = new System.Drawing.Size(121, 21);
            this.InfoPanelElevationGainLabel.TabIndex = 7;
            this.InfoPanelElevationGainLabel.Text = "Elevation Gain";
            // 
            // InfoPanelDistanceLabel
            // 
            this.InfoPanelDistanceLabel.AutoSize = true;
            this.InfoPanelDistanceLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelDistanceLabel.Location = new System.Drawing.Point(3, 59);
            this.InfoPanelDistanceLabel.Name = "InfoPanelDistanceLabel";
            this.InfoPanelDistanceLabel.Size = new System.Drawing.Size(76, 21);
            this.InfoPanelDistanceLabel.TabIndex = 6;
            this.InfoPanelDistanceLabel.Text = "Distance";
            // 
            // InfoPanelDateLabel
            // 
            this.InfoPanelDateLabel.AutoSize = true;
            this.InfoPanelDateLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoPanelDateLabel.Location = new System.Drawing.Point(3, 27);
            this.InfoPanelDateLabel.Name = "InfoPanelDateLabel";
            this.InfoPanelDateLabel.Size = new System.Drawing.Size(50, 21);
            this.InfoPanelDateLabel.TabIndex = 5;
            this.InfoPanelDateLabel.Text = "Date:";
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(477, 435);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(122, 23);
            this.uploadButton.TabIndex = 9;
            this.uploadButton.Text = "Upload Activities";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // fullInitButton
            // 
            this.fullInitButton.Location = new System.Drawing.Point(605, 435);
            this.fullInitButton.Name = "fullInitButton";
            this.fullInitButton.Size = new System.Drawing.Size(122, 23);
            this.fullInitButton.TabIndex = 10;
            this.fullInitButton.Text = "Download All";
            this.fullInitButton.UseVisualStyleBackColor = true;
            this.fullInitButton.Click += new System.EventHandler(this.fullInitButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 500);
            this.Controls.Add(this.fullInitButton);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.activityTypeCombo);
            this.Controls.Add(this.infoTypeCombo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.moveCoordinatesLabel);
            this.Controls.Add(this.clickCoordLabel);
            this.Controls.Add(this.LastTimeButton);
            this.Controls.Add(this.NextTimeButton);
            this.Controls.Add(this.BarPlot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StravaViewer";
            this.Load += new System.EventHandler(this.LoadClient);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ScottPlot.FormsPlot BarPlot;
        private Button NextTimeButton;
        private Button LastTimeButton;
        private Label clickCoordLabel;
        private Label moveCoordinatesLabel;
        private Label InfoPanelNameValue;
        private Button button1;
        private ComboBox infoTypeCombo;
        private ComboBox activityTypeCombo;
        private Panel panel1;
        private Label InfoPanelDistanceLabel;
        private Label InfoPanelDateLabel;
        private Label InfoPanelDurationValue;
        private Label InfoPanelElevationGainValue;
        private Label InfoPanelDistanceValue;
        private Label InfoPanelDateValue;
        private Label InfoPanelDurationLabel;
        private Label InfoPanelElevationGainLabel;
        private Button uploadButton;
        private Button fullInitButton;
    }
}