# HideMyFace

**HideMyFace** is a real-time webcam face censorship application. It allows you to obscure faces using black/white blocks, pixelation, or Gaussian blur. The output can be directly used in streaming software like OBS.

---

## Features

- Real-time face detection using OpenCV (`haarcascade_frontalface_default.xml`)
- Multiple censorship modes:
  - Black block
  - White block
  - Pixelation
  - Gaussian blur
- Adjustable effect size
- Selectable webcam device

---

## Used

- **OpenCvSharp** — for real-time computer vision and face detection
- **haarcascade_frontalface_default.xml** — Haar Cascade classifier for face detection

---

## Licensing

This project is distributed under the **MIT License**. See the LICENSE file for details.

Third-party components and licenses:

- [OpenCvSharp](https://github.com/shimat/opencvsharp) — MIT License
- [OpenCV Haar Cascades](https://github.com/opencv/opencv/tree/master/data/haarcascades) — BSD License

---

## Usage

1. Make sure `haarcascade_frontalface_default.xml` is in same folder as exe.
2. Select your camera, censorship type, and effect size.
3. The output will appear in the preview window and can be used in OBS via a window capture.

---

## Notes

- Tested on Windows 10 with .NET 9.0.
- Ensure your camera drivers are installed and accessible.
- Adjust effect size to your preference for pixelation or blur intensity.
- VERY BUGGY!
