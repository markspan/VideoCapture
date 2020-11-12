using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp;
using SharpDX.MediaFoundation;

namespace VideoCaptureForm
{
    public partial class ConfigurationForm : Form
    {

        private readonly string[] _cameraList;
        private readonly List<CameraSourceSelector> _cameraControls;
        private readonly List<VideoCaptureForm> _captures;
        private string _dataPath;
        public ConfigurationForm()
        {
            InitializeComponent();
            _cameraList = ListOfAttachedCameras();
            _cameraControls = new List<CameraSourceSelector>();
            _captures = new List<VideoCaptureForm>();

            DataDirSet.Description =
            "Select the directory that you want to use as the default data directory.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            DataDirSet.ShowNewFolderButton = false;

            // Default to the My Documents folder.
            // Could read this in from a JSON file to be "Persistent"?

            DataDirSet.RootFolder = Environment.SpecialFolder.MyComputer;
            _dataPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Dir.Text = _dataPath;

            if (_cameraList.Length == 0)
            {
                MessageBox.Show(
                    "No Compatible cameras were found on this machine",
                    "ERROR",
                    MessageBoxButtons.OK);
            }
            PopulateCameraGUI();
        }
        private void PopulateCameraGUI()
        {
            Camerabox.SuspendLayout();
            SuspendLayout();

            string id = DateTime.Now.ToString("yyddMMhhmm");
            int camIndex = 1;

            foreach (string cam in _cameraList)
            {
                CameraSourceSelector thisCam;
                thisCam = new CameraSourceSelector();
                thisCam.Check.Text += camIndex.ToString();
                thisCam.CameraName.Text = _cameraList[camIndex - 1];
                thisCam.FileName.Text = "cam" + camIndex;//_cameraList[camIndex - 1] + id;
                thisCam.StreamName.Text = "cam" + camIndex;//_cameraList[camIndex - 1] + id;
                thisCam.Location = new System.Drawing.Point(10, 30 * camIndex);
                thisCam.Check.Checked = true;
                _cameraControls.Add(thisCam);
                Camerabox.Controls.Add(thisCam);

                camIndex++;
            }

            Camerabox.ResumeLayout(false);
            ResumeLayout(false);
        }
        private static string[] ListOfAttachedCameras()
        {
            var cameras = new List<string>();
            var attributes = new MediaAttributes(1);
            attributes.Set(CaptureDeviceAttributeKeys.SourceType.Guid, CaptureDeviceAttributeKeys.SourceTypeVideoCapture.Guid);
            var devices = MediaFactory.EnumDeviceSources(attributes);
            for (var i = 0; i < devices.Count(); i++)
            {
                var friendlyName = devices[i].Get(CaptureDeviceAttributeKeys.FriendlyName);
                cameras.Add(friendlyName);
            }
            return cameras.ToArray();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                int thiscamIndex = 0;
                System.Drawing.Color orig = BackColor;
                foreach (CameraSourceSelector Cam in _cameraControls)
                {
                    if (Cam.Check.Checked)
                    {
                        BackColor = System.Drawing.Color.AntiqueWhite;
                        Invalidate();
                        _captures.Add(new VideoCaptureForm(thiscamIndex,
                                                        Cam.CameraName.Text,
                                                        Cam.FileName.Text,
                                                        Cam.StreamName.Text,
                                                        _dataPath));
                        _captures.Last().Show();
                        //CamForm.Show();
                        BackColor = orig;

                    }
                    thiscamIndex++;
                }
                OKButton.Enabled = false;
                StartRecording.Enabled = true;
                CnclButton.Enabled = true;
                Camerabox.Enabled = false;
                SetDataDir.Enabled = false;
            }
            catch (Exception)
            {
                OKButton.Enabled = true;
                StartRecording.Enabled = false;
                CnclButton.Enabled = false;
                Camerabox.Enabled = true;
                SetDataDir.Enabled = true;
            }
        }

        private void CnclButton_Click(object sender, EventArgs e)
        {
            OKButton.Enabled = true;
            StartRecording.Enabled = false;
            CnclButton.Enabled = false;
            Camerabox.Enabled = true;
            SetDataDir.Enabled = true;
            _captures.Clear();
        }

        private void SetDataDir_Click(object sender, EventArgs e)
        {
            DialogResult result = DataDirSet.ShowDialog();
            if (result == DialogResult.OK)
            {
                _dataPath = DataDirSet.SelectedPath;
                Dir.Text = _dataPath;
            }
        }

        private void StartRecording_Click(object sender, EventArgs e)
        {
            OKButton.Enabled = false;
            StartRecording.Enabled = false;
            CnclButton.Enabled = true;
            Camerabox.Enabled = false;
            SetDataDir.Enabled = false;
            foreach (VideoCaptureForm v in _captures)
            {
                bool succes = v.StartRecording();
            }
        }
    }
}

