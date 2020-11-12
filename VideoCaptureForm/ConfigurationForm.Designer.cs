namespace VideoCaptureForm
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.Camerabox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonsBox = new System.Windows.Forms.GroupBox();
            this.StartRecording = new System.Windows.Forms.Button();
            this.CnclButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.DataDirSet = new System.Windows.Forms.FolderBrowserDialog();
            this.SetDataDir = new System.Windows.Forms.Button();
            this.Dir = new System.Windows.Forms.Label();
            this.HelpTextDisplay = new System.Windows.Forms.ToolTip(this.components);
            this.Camerabox.SuspendLayout();
            this.ButtonsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Camerabox
            // 
            this.Camerabox.Controls.Add(this.label3);
            this.Camerabox.Controls.Add(this.label2);
            this.Camerabox.Controls.Add(this.label1);
            this.Camerabox.Location = new System.Drawing.Point(23, 28);
            this.Camerabox.Name = "Camerabox";
            this.Camerabox.Size = new System.Drawing.Size(1021, 200);
            this.Camerabox.TabIndex = 1;
            this.Camerabox.TabStop = false;
            this.Camerabox.Text = "Cameras";
            this.HelpTextDisplay.SetToolTip(this.Camerabox, "Cameras and recording parameters");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(707, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stream Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(425, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "File Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camera Name";
            // 
            // ButtonsBox
            // 
            this.ButtonsBox.Controls.Add(this.StartRecording);
            this.ButtonsBox.Controls.Add(this.CnclButton);
            this.ButtonsBox.Controls.Add(this.OKButton);
            this.ButtonsBox.Location = new System.Drawing.Point(23, 234);
            this.ButtonsBox.Name = "ButtonsBox";
            this.ButtonsBox.Size = new System.Drawing.Size(273, 78);
            this.ButtonsBox.TabIndex = 3;
            this.ButtonsBox.TabStop = false;
            // 
            // StartRecording
            // 
            this.StartRecording.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartRecording.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StartRecording.BackgroundImage")));
            this.StartRecording.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StartRecording.Enabled = false;
            this.StartRecording.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StartRecording.Location = new System.Drawing.Point(97, 26);
            this.StartRecording.Name = "StartRecording";
            this.StartRecording.Size = new System.Drawing.Size(85, 34);
            this.StartRecording.TabIndex = 2;
            this.StartRecording.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.HelpTextDisplay.SetToolTip(this.StartRecording, "Pressing record will start recording the videos to file, and will push the framet" +
        "imes to the streams.");
            this.StartRecording.UseVisualStyleBackColor = true;
            this.StartRecording.Click += new System.EventHandler(this.StartRecording_Click);
            // 
            // CnclButton
            // 
            this.CnclButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CnclButton.Enabled = false;
            this.CnclButton.Location = new System.Drawing.Point(188, 26);
            this.CnclButton.Name = "CnclButton";
            this.CnclButton.Size = new System.Drawing.Size(73, 34);
            this.CnclButton.TabIndex = 1;
            this.CnclButton.Text = "Cancel";
            this.HelpTextDisplay.SetToolTip(this.CnclButton, "When you posted the streams, but want to change the settings, first cancel the fe" +
        "ed and streams");
            this.CnclButton.UseVisualStyleBackColor = true;
            this.CnclButton.Click += new System.EventHandler(this.CnclButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OKButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.OKButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.OKButton.Location = new System.Drawing.Point(6, 26);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(85, 34);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.HelpTextDisplay.SetToolTip(this.OKButton, "Pressing ok will start displaying the videos, and will post the streams to enable" +
        " recording using lsl labrecorder.");
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SetDataDir
            // 
            this.SetDataDir.Location = new System.Drawing.Point(314, 260);
            this.SetDataDir.Name = "SetDataDir";
            this.SetDataDir.Size = new System.Drawing.Size(29, 34);
            this.SetDataDir.TabIndex = 4;
            this.SetDataDir.Text = "D";
            this.HelpTextDisplay.SetToolTip(this.SetDataDir, "Where to save the videos");
            this.SetDataDir.UseVisualStyleBackColor = true;
            this.SetDataDir.Click += new System.EventHandler(this.SetDataDir_Click);
            // 
            // Dir
            // 
            this.Dir.AutoSize = true;
            this.Dir.Location = new System.Drawing.Point(349, 266);
            this.Dir.Name = "Dir";
            this.Dir.Size = new System.Drawing.Size(72, 20);
            this.Dir.TabIndex = 5;
            this.Dir.Text = "Directory";
            // 
            // HelpTextDisplay
            // 
            this.HelpTextDisplay.IsBalloon = true;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CnclButton;
            this.ClientSize = new System.Drawing.Size(1065, 325);
            this.Controls.Add(this.Dir);
            this.Controls.Add(this.SetDataDir);
            this.Controls.Add(this.ButtonsBox);
            this.Controls.Add(this.Camerabox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigurationForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SyncVideo";
            this.Camerabox.ResumeLayout(false);
            this.Camerabox.PerformLayout();
            this.ButtonsBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox Camerabox;
        private System.Windows.Forms.GroupBox ButtonsBox;
        private System.Windows.Forms.Button CnclButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog DataDirSet;
        private System.Windows.Forms.Button SetDataDir;
        public System.Windows.Forms.Label Dir;
        private System.Windows.Forms.Button StartRecording;
        private System.Windows.Forms.ToolTip HelpTextDisplay;
    }
}