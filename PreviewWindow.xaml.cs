//im not gonna even try to optimize this shit code

using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.Windows;
using System.Windows.Threading;

namespace HideMyFace
{
    public partial class PreviewWindow : System.Windows.Window
    {
        private VideoCapture _capture;
        private DispatcherTimer _timer;
        private CascadeClassifier _faceCascade;
        private int _censorshipType;
        private int _effectSize;
        private int _cameraIndex;

        public PreviewWindow(int censorshipType, int effectSize, int cameraIndex)
        {
            InitializeComponent();
            _censorshipType = censorshipType;
            _effectSize = effectSize;
            _cameraIndex = cameraIndex;

            _capture = new VideoCapture(_cameraIndex);
            _faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml"); //if you delete that file your pc will break

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(33) };
            _timer.Tick += ProcessFrame;
            _timer.Start();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            using var frame = new Mat();
            _capture.Read(frame);
            if (frame.Empty()) return;

            var gray = frame.CvtColor(ColorConversionCodes.BGR2GRAY);
            var faces = _faceCascade.DetectMultiScale(gray, 1.1, 4);

            foreach (var face in faces)
            {
                ApplyCensorship(frame, face);
            }

            VideoImage.Source = frame.ToBitmapSource();
        }

        private void ApplyCensorship(Mat frame, OpenCvSharp.Rect face)
        {
            switch (_censorshipType)
            {
                case 0:
                    frame.Rectangle(face, Scalar.Black, -1);
                    break;
                case 1:
                    frame.Rectangle(face, Scalar.White, -1);
                    break;
                case 2:
                    var roi = new Mat(frame, face);
                    var small = roi.Resize(new OpenCvSharp.Size(_effectSize, _effectSize), 0, 0, InterpolationFlags.Area);
                    var big = small.Resize(roi.Size(), 0, 0, InterpolationFlags.Nearest);
                    big.CopyTo(roi);
                    break;
                case 3:
                    Cv2.GaussianBlur(new Mat(frame, face), new Mat(frame, face), new OpenCvSharp.Size(_effectSize, _effectSize), 0);
                    break;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _timer?.Stop();
            _capture?.Release();
            _capture?.Dispose();
            base.OnClosed(e);
        }
    }
}
