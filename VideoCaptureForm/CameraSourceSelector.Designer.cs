namespace VideoCaptureForm
{
    partial class CameraSourceSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CameraName = new System.Windows.Forms.TextBox();
            this.Check = new System.Windows.Forms.CheckBox();
            this.FileName = new System.Windows.Forms.TextBox();
            this.StreamName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CameraName
            // 
            this.CameraName.Location = new System.Drawing.Point(119, 13);
            this.CameraName.Name = "CameraName";
            this.CameraName.Size = new System.Drawing.Size(260, 26);
            this.CameraName.TabIndex = 0;
            // 
            // Check
            // 
            this.Check.AutoSize = true;
            this.Check.Location = new System.Drawing.Point(9, 15);
            this.Check.Name = "Check";
            this.Check.Size = new System.Drawing.Size(104, 24);
            this.Check.TabIndex = 1;
            this.Check.Text = "Camera 1";
            this.Check.UseVisualStyleBackColor = true;
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(412, 13);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(260, 26);
            this.FileName.TabIndex = 2;
            // 
            // StreamName
            // 
            this.StreamName.Location = new System.Drawing.Point(698, 13);
            this.StreamName.Name = "StreamName";
            this.StreamName.Size = new System.Drawing.Size(260, 26);
            this.StreamName.TabIndex = 3;
            this.StreamName.Tag = "lsl";
            // 
            // CameraSourceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StreamName);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.Check);
            this.Controls.Add(this.CameraName);
            this.Name = "CameraSourceSelector";
            this.Size = new System.Drawing.Size(973, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox CameraName;
        public System.Windows.Forms.TextBox FileName;
        public System.Windows.Forms.TextBox StreamName;
        public System.Windows.Forms.CheckBox Check;
    }
}
