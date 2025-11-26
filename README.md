# DieCenterSampleApp

A simple WinForms sample application demonstrating how to integrate and use  
**DieCenterLib** — a high-precision die center detection library powered by OpenCvSharp4.

This sample shows how to:
- Load an image  
- Detect the die center (Auto ROI or Manual ROI)  
- Draw ROI box, corner points, diagonals, and computed center  
- Display detection results in both ROI coordinates and full-image coordinates  

## 🚀 Features

- Auto-ROI and Manual-ROI modes  
- Visual overlay (ROI, corners, diagonals, center point)  
- Uses `DieCenterLib` directly from NuGet  
- Clean WinForms implementation  
- Accepts BMP / PNG / JPG / TIFF  

## 📦 Required NuGet Packages

Install manually or let Visual Studio install dependencies automatically:

```
Install-Package DieCenterLib -Version 1.0.0
```

This also installs:
- OpenCvSharp4  
- OpenCvSharp4.Extensions  
- OpenCvSharp4.runtime.win  

## 🛠️ How to Run

1. Clone repository:
   ```
   git clone https://github.com/<your-username>/DieCenterSampleApp.git
   ```
2. Open `DieCenterSampleApp.sln`  
3. Restore NuGet packages (usually automatic)  
4. Press **F5** to run  

## 📌 Basic Usage

1. **Open image**  
2. Select **Auto ROI** or **Manual ROI**  
3. Click **Detect**  
4. Overlay + detection summary will appear  

## 📂 Project Structure

```
DieCenterSampleApp/
│   DieCenterSampleApp.sln
│   README.md
│
└───DieCenterSampleApp/
      Form1.cs
      Program.cs
      Form1.Designer.cs
      Form1.resx
      DieCenterSampleApp.csproj
```

## 🧪 DLL Call Example

```csharp
var param = new DieCenterParameters
{
    UseAutoRoi = true,
    RoiWidth = 480,
    RoiHeight = 480
};

_lastResult = DieCenterDetector.Run(_currentBmp, param);
```

## 📝 License

MIT License © 2025 Napat Sutikant

