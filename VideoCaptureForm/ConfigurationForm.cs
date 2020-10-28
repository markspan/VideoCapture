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
        private readonly VideoCapture _capture; // usb camera source
        private readonly string[] _cameraList;
        private List<CameraSourceSelector> _cameraControls;
        private string _dataPath;
        public ConfigurationForm()
        {
            InitializeComponent();
            _capture = new VideoCapture();
            _cameraList = ListOfAttachedCameras();
            _cameraControls = new List<CameraSourceSelector>();
            CameraInfo.CameraName.Text = "No Camera Found";
            DataDirSet.Description =
            "Select the directory that you want to use as the default data directory.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            DataDirSet.ShowNewFolderButton = false;

            // Default to the My Documents folder.
            // Could read this in from a JSON file to be "Persistent"?

            DataDirSet.RootFolder = Environment.SpecialFolder.Desktop;// Personal;
            _dataPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Dir.Text = _dataPath;

            if (_cameraList.Length == 0)
            {
                DialogResult OK = MessageBox.Show(
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

            int camIndex = 1;
            foreach (string cam in _cameraList)
            {
                CameraSourceSelector thisCam;
                if (camIndex > 1)
                {
                    thisCam = new CameraSourceSelector();

                    DateTime id = DateTime.Now;
                    thisCam.CameraName.Text = _cameraList[camIndex - 1];
                    thisCam.FileName.Text = _cameraList[camIndex - 1] + id.ToString("yyddMMhhmm");
                    thisCam.StreamName.Text = _cameraList[camIndex - 1] + id.ToString("yyddMMhhmm");
                    thisCam.Location = new System.Drawing.Point(10, 20 * camIndex);
                    thisCam.Check.Checked = true;
                    _cameraControls.Add(thisCam);
                    Camerabox.Controls.Add(thisCam);
                }
                else
                {
                    DateTime id = DateTime.Now;
                    CameraInfo.CameraName.Text = _cameraList[camIndex - 1];
                    CameraInfo.FileName.Text = _cameraList[camIndex - 1] + id.ToString("yyddMMhhmm");
                    CameraInfo.StreamName.Text = _cameraList[camIndex - 1] + id.ToString("yyddMMhhmm");
                    _cameraControls.Add(CameraInfo);
                }
                camIndex++;
            }

            Camerabox.ResumeLayout(false);
            ResumeLayout(false);
        }
        public static string[] ListOfAttachedCameras()
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
            int thiscamIndex = 0;
            foreach (CameraSourceSelector Cam in _cameraControls)
            {
                if (Cam.Check.Checked)
                {
                    VideoCaptureForm CamForm = new VideoCaptureForm(thiscamIndex,
                                                    Cam.CameraName.Text,
                                                    Cam.FileName.Text,
                                                    Cam.StreamName.Text,
                                                    _dataPath);
                    CamForm.Show();
                }
            }
        }

        private void CnclButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}

