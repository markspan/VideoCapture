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

        private double _FPS = 0;                        // calculated fps of input stream (camera_
        private long _frameNumber = 0;                  // Framenumber IN FILE
        private Boolean _recording = false;             // is recording on?

        private readonly string _fileName = "";                  // Filename of output stream (mjpeg)
        private readonly string _streamName = "";
        private readonly string _dataDir = "";
        private readonly int _index = -1;


        public VideoCaptureForm(int index,
                                string cameraname,
                                string filename,
                                string streamname,
                                string datadir)
        {
            InitializeComponent();
            this.Text = cameraname;
            _fileName = filename;
            _streamName = streamname;
            _dataDir = datadir;
            _index = index;

            _capture = new VideoCapture();
            _video = new VideoWriter();

            _inf = new liblsl.StreamInfo(_streamName, "Markers", 1, 0, liblsl.channel_format_t.cf_string, DateTime.Now.ToString());
            _outl = new liblsl.StreamOutlet(_inf);
        }

        private void VideoCaptureForm_Load(object sender, EventArgs e)
        {
            OpenCamera(_index);
            //OpenVideoFileForWrite();
            RetrieveFrame.RunWorkerAsync();       // start importing frames and screen output
        }

        private bool OpenCamera(int cam)
        {
            if (_capture.IsOpened())
            {
                //Close any opened capture device
                _capture.Release();
            }

            _capture.Open(cam, VideoCaptureAPIs.DSHOW);
            if (!_capture.IsOpened()) // this camera cannot be opened: not present or in use.
            {
                Console.WriteLine("Camera " + cam + " Cannot open.....\n");
                return false;
            }
            // Camera could be opened: 
            _capture.Fps = 30;           // set its framerate to 30 (as-if that would work :))
            _ = _capture.RetrieveMat();  // first capture takes longer....
            Thread.Sleep(100);

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
            _capture?.Dispose();
            _video?.Release();
            _video?.Dispose();
        }


        private void RetrieveFrame_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var frameBitmap = (Bitmap)e.UserState;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = frameBitmap;
        }


        public bool StartRecording()
        {
            try
            {
                if (_video.IsOpened())
                    _video.Release();

                string FullPath = _dataDir + "\\" + _fileName;
                if (!FullPath.EndsWith(".avi")) FullPath += ".avi";

                _video.Open(FullPath, FourCC.MJPG, Math.Max(5, _FPS), new OpenCvSharp.Size(_capture.FrameWidth, _capture.FrameHeight), true);

                if (!_video.IsOpened())
                {
                    MessageBox.Show("File Creation Error\n Cannot create " + FullPath, "Error", MessageBoxButtons.OK);
                    Close();
                    return false;
                }
                _recording = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void RetrieveFrame_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;
            while (!bgWorker.CancellationPending)
            {
                try
                {
                    _capture.Grab();
                    using (var frameMat = _capture.RetrieveMat())
                    {
                        if (_recording)
                        {
                            // Place Framenumber and FSP (as measured) left topcorner of the frame
                            Cv2.PutText(frameMat, _fileName + " " + _frameNumber++.ToString() + " FPS: " + _FPS.ToString(), new OpenCvSharp.Point(10, 13), HersheyFonts.HersheyPlain, 1, Scalar.Black);

                            // Create a Label for the marker used, containing framenumber. 
                            // Only the timestamp is important here, as framenumbers mean nothing.
                            string[] sample = new string[1];
                            sample[0] = _frameNumber.ToString();
                            // Push the sample through LSL
                            _outl.push_sample(sample);
                            // Write the frame to the file
                            if (_video.IsOpened())
                                _video.Write(frameMat);
                        }
                        // create bitmap from frame, and then show it on screen
                        var frameBitmap = BitmapConverter.ToBitmap(frameMat);
                        bgWorker.ReportProgress(0, frameBitmap);
                    }
                }
                catch (Exception)
                {
                    // Even if we mess up: continue
                    Console.WriteLine("Missed???\n");
                }
                // Create some room for the guithread.
                // 10 ms limits the framerate to a theoretical maximum of 100hz.
                Thread.Sleep(10);
            }
        }
    }
}
