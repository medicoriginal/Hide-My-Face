using System.Windows;
using OpenCvSharp;

namespace HideMyFace
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCameras();
        }

        private void LoadCameras()
        {
            for (int i = 0; i < 5; i++)
            {
                using var capture = new VideoCapture(i);
                if (capture.IsOpened())
                {
                    CameraSelector.Items.Add($"Camera {i}");
                }
            }
            if (CameraSelector.Items.Count == 0)
            {
                CameraSelector.Items.Add("No cameras to choose");
            }
            CameraSelector.SelectedIndex = 0;
        }

        private void OpenPreview_Click(object sender, RoutedEventArgs e)
        {
            int camIndex = CameraSelector.SelectedIndex;
            var preview = new PreviewWindow(
                CensorshipType.SelectedIndex,
                (int)EffectSize.Value,
                camIndex);
            preview.Show();
        }
    }
}
