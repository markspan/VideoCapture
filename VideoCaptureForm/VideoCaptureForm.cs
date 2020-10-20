/* Program to retrieve a video stream from a OpenCV compatible webcam and save it to a file
 * in MJPEG format. Simultaniously creating an labstreaminglayer Marker stream to create 
 * timestamps for each retrieved frame.
 * 
 * Adapted from the videocaptureform demo of openCVsharp4.
 * https://github.com/shimat/opencvsharp_samples/tree/95bed79aa9ce7f9b548812bc529fa09833c4c158/VideoCaptureForm
 * 
 * v0.9.0 19/10/2020 Mark Span University of Groningen m.m.span@rug.nl
 * 
*/
using System;
using System.ComponentModel;    // Backgroundworker
using System.Diagnostics;       // Stopwatch
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using LSL;                      // labstreaminglayer
using OpenCvSharp;              // for videocapture and videowriter
using OpenCvSharp.Extensions;   // for bitmapconverter


namespace VideoCaptureForm
{
    public partial class VideoCaptureForm : Form
    {
        private readonly VideoCapture _capture; // usb camera source
        private VideoWriter _video;             // Videofile
        private liblsl.StreamInfo _inf;
        private liblsl.StreamOutlet _outl;

        double _FPS = 0;                        // calculated fps of input stream (camera_
        long _frameNumber = 0;                  // Framenumber IN FILE
        Boolean _recording = false;             // is recording on?
        string _fileName = "";                  // Filename of output stream (mjpeg)


        public VideoCaptureForm()
        {
            InitializeComponent();

            _capture = new VideoCapture();
            _ = EnumerateNumberOfCameras(); // count number of attached cameras and create
                                            // menu items for each.

            _video = new VideoWriter();
            _inf = new liblsl.StreamInfo("VideoTimings " + DateTime.Now, "Markers", 1, 0, liblsl.channel_format_t.cf_string, "giu4569");
            _outl = new liblsl.StreamOutlet(_inf);
        }

        private void VideoCaptureForm_Load(object sender, EventArgs e)
        {
            if (!openCamera(0)) _=openCamera(1);  // default camera in use? open auxilary camera
            RetrieveFrame.RunWorkerAsync();       // start importing frames and screen output
        }

        private bool openCamera(int cam)
        {
            if (_capture.IsOpened())
            {
                //Close any opened capture device
                _capture.Release();
            }

            _capture.Open(cam, VideoCaptureAPIs.ANY);
            if (!_capture.IsOpened()) // this camera cannot be opened: not present or in use.
            {
                return false;
            }
            // Camera could be opened: 
            _capture.Fps = 30;           // set its framerate to 30 (as-if that would work :))
            _ = _capture.RetrieveMat();  // first capture takes longer....
            Thread.Sleep(300);

            // fetch 30 frames and calculate the #frames per 1000ms
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 30; i++)
            {
                _ = _capture.RetrieveMat();
            }
            stopWatch.Stop(); 
           
            _FPS = Math.Round(30000.0 / stopWatch.ElapsedMilliseconds);

            _capture.Fps = _FPS; // and set the **input** framerate to this (as-if that would work :))

            this.ClientSize = new System.Drawing.Size(_capture.FrameWidth, _capture.FrameHeight);
            return true;
        }

        private void VideoCaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            RetrieveFrame.CancelAsync();
            Thread.Sleep(300);
            _capture.Dispose();
            _video.Release();
            _video.Dispose();
        }


        private void RetrieveFrame_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var frameBitmap = (Bitmap)e.UserState;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = frameBitmap;
        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem)sender).Text;
            if (name == "Recording")
            {
                name = "Record";
                _recording = false;
            }
            else
            {
                name = "Recording";
                _recording = true;
            }
            ((ToolStripMenuItem)sender).Text = name;
        }
        private int EnumerateNumberOfCameras()
        {
            int numberOfDevices = 0;
            bool noError = true;

            while (noError)
            {
                try
                {
                    // Check if camera is available.
                    noError = _capture.Open(numberOfDevices, VideoCaptureAPIs.ANY);
                    if (noError) addMenu(numberOfDevices);
                    // Will crash if not available, hence try/catch.
                    // ...
                }
                catch (Exception)
                {
                    noError = false;
                }

                // If above call worked, we have found another camera.
                _capture.Release();
                numberOfDevices++;
            }
            return numberOfDevices - 1;
        }

        private void addMenu(int index)
        {
            System.Windows.Forms.ToolStripMenuItem item = new ToolStripMenuItem();
            item.Checked = false;
            item.CheckOnClick = true;

            item.Name = "Camera " + index;
            item.Size = new System.Drawing.Size(224, 26);
            item.Text = item.Name;
            item.Click += new System.EventHandler(this.Camera_Click);
            this.cameraToolStripMenuItem.DropDownItems.Add(item);
        }

        private void fileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog GetSaveFile = new SaveFileDialog();

            GetSaveFile.Filter = "avi files (*.avi)|*.avi|All files (*.*)|*.*";
            GetSaveFile.FilterIndex = 1;
            GetSaveFile.RestoreDirectory = true;

            if (GetSaveFile.ShowDialog() == DialogResult.OK)
            {
                _fileName = GetSaveFile.FileName;
            }
                ((ToolStripMenuItem)sender).Text = _fileName;

            if (_video.IsOpened()) _video.Release();
            _video.Open(_fileName, FourCC.MJPG, Math.Max(5, _FPS), new OpenCvSharp.Size(_capture.FrameWidth, _capture.FrameHeight), true);
            if (!_video.IsOpened())
            {
                Close();
                return;
            }
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in this.cameraToolStripMenuItem.DropDownItems)
                item.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
            string Name = ((ToolStripMenuItem)sender).Name;
            int camera = Int32.Parse(Name.Remove(0, 7));
            openCamera(camera);
        }

        private void RetrieveFrame_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;
            while (!bgWorker.CancellationPending)
            {
                try
                {
                    using (var frameMat = _capture.RetrieveMat())
                    {
                        if (_recording)
                        {
                            Cv2.PutText(frameMat, _frameNumber++.ToString() + " FPS: " + _FPS.ToString(), new OpenCvSharp.Point(10, 13), HersheyFonts.HersheyPlain, 1, Scalar.Black);
                            string[] sample = new string[1];
                            sample[0] = new string('F', 1) + _frameNumber.ToString();
                            _outl.push_sample(sample);
                            if (_video.IsOpened())
                                _video.Write(frameMat);
                        }
                        var frameBitmap = BitmapConverter.ToBitmap(frameMat);
                        bgWorker.ReportProgress(0, frameBitmap);
                    }
                }
                catch (Exception)
                {
                }
                Thread.Sleep(10);
            }
        }
    }
}
