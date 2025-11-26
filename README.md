# DieCenterSampleApp

A simple WinForms sample application demonstrating how to integrate and use  
**DieCenterLib** — a high‑precision die center detection library powered by OpenCvSharp4.

This sample shows how to:

- Load an image  
- Detect the die center (Auto ROI or Manual ROI)  
- Draw ROI box, corner points, diagonals, and computed center  
- Display detection results in both ROI coordinates and full‑image coordinates  

---

## 🚀 Features
- Auto‑ROI and Manual‑ROI modes  
- Visual overlay (ROI, corners, diagonals, center point)  
- Uses `DieCenterLib` directly from NuGet  
- Clean WinForms implementation  
- Supports BMP / PNG / JPG / TIFF  

---

## 📦 Required NuGet Packages

Install manually or let Visual Studio install dependencies automatically:

```powershell
Install-Package DieCenterLib -Version 1.0.0
```

This automatically installs:
- OpenCvSharp4  
- OpenCvSharp4.Extensions  
- OpenCvSharp4.runtime.win  

---

## 🛠️ How to Run

1. Clone repository  
2. Open `DieCenterSampleApp.sln`  
3. Restore NuGet packages  
4. Press **F5** to run  

---

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
      sample_pic/
```

---

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

---

# 🛑 Troubleshooting  
Common problems when opening this project from a **ZIP archive** or when **Designer fails to load**.

---

## ❗ 1) “The designer could not be shown for this file...”
Occurs when opening *Form1.Designer.cs*.

### ✔️ Fix (Case A: Windows blocked the ZIP file)

If the ZIP was downloaded from the internet, Windows marks it as *blocked*.

**Solution:**
1. Right‑click the ZIP → **Properties**
2. Check **Unblock**
3. Click **OK**
4. Extract again  
5. Open the `.sln` and try the Designer again

---

## ❗ 2) Error: “Couldn't process file Form1.resx due to being in the Internet or Restricted zone”

**Solution (same as Case A):**

1. Right‑click the *project folder* → **Properties**  
2. Click **Unblock** (if appears)  
3. Restart Visual Studio  

---

## ❗ 3) Designer fails because NuGet packages are not restored yet

Happens when opening the project immediately after extracting ZIP.

**Solution:**

1. Build → **Rebuild Solution**  
2. Visual Studio automatically restores NuGet  
3. Close Designer → reopen  

Or restore manually:  
```
Tools → NuGet Package Manager → Restore Packages
```

---

## ❗ 4) Designer works only after restarting Visual Studio  
This is caused by stale Designer cache.

**Permanent Fix:**

1. Close Visual Studio  
2. Delete folders:
```
.vs/
bin/
obj/
```
3. Reopen Visual Studio  
4. Build again  

---

## 📝 License

MIT License © 2025 Napat Sutikant
