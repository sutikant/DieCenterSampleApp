using System;
using System.Drawing;
using System.Windows.Forms;
using DieCenterLib;

namespace DieCenterSampleApp
{
    public partial class Form1 : Form
    {
        private Bitmap _currentBmp;

        // สำหรับเก็บผลล่าสุดจาก DLL
        private DieCenterResult _lastResult;

        // ROI แบบ Manual (ในพิกัดรูปจริง)
        private Rectangle? _manualRoiImage = null;

        // ROI preview บน PictureBox
        private Rectangle _manualRoiPreview = Rectangle.Empty;

        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            pbMain.SizeMode = PictureBoxSizeMode.Zoom;

            this.MaximizeBox = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Open image";
                dlg.Filter = "Image files|*.bmp;*.png;*.jpg;*.jpeg;*.tif;*.tiff|All files|*.*";

                // ตั้งค่า Default Directory
                try
                {
                    string exeDir = Application.StartupPath;
                    string picDir = System.IO.Path.Combine(
                        exeDir,
                        @"sample_pic");

                    if (System.IO.Directory.Exists(picDir))
                        dlg.InitialDirectory = picDir;
                    else
                        dlg.InitialDirectory = exeDir;  // fallback
                }
                catch
                {
                    dlg.InitialDirectory = Application.StartupPath;
                }

                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                // ล้างภาพเก่า
                if (_currentBmp != null)
                {
                    pbMain.Image = null;
                    _currentBmp.Dispose();
                    _currentBmp = null;
                }

                _currentBmp = (Bitmap)Image.FromFile(dlg.FileName);
                pbMain.Image = (Bitmap)_currentBmp.Clone();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbAutoRoi.Checked = true;
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {

            // ตรวจว่ามีภาพหรือยัง
            if (_currentBmp == null)
            {
                MessageBox.Show(this, "Please open an image first.",
                    "DieCenterSample", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // เตรียมพารามิเตอร์พื้นฐาน
            var param = new DieCenterParameters
            {
                // Default → Auto ROI
                UseAutoRoi = rbAutoRoi.Checked,

                RoiWidth = 480,
                RoiHeight = 480,
                Coverage = 0.8f,
                BandWidth = 50,
                CannyLow = 30,
                CannyHigh = 90,
                MinGrad = 5.0f,
                KSigma = 2.0,
                ExportIntensityCsv = false
            };


            // หากเลือก Manual ROI → ตรวจสอบและส่ง ROI ให้ DLL
            if (rbManualRoi.Checked)
            {
                if (_manualRoiImage == null)
                {
                    MessageBox.Show(this,
                        "Please click the top-left corner of the ROI (Manual ROI mode).",
                        "DieCenterSample",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // ใช้ ROI จาก image ที่คลิกไว้
                param.UseAutoRoi = false;
                param.ManualRoi = _manualRoiImage.Value;
            }

            // เรียก DLL
            using (var bmpClone = (Bitmap)_currentBmp.Clone())
            {
                try
                {
                    _lastResult = DieCenterDetector.Run(bmpClone, param);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Detection failed: " + ex.Message,
                        "DieCenterSample", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // วาด Overlay ของผลลัพธ์
            DrawOverlay();

            // แสดงผลลัพธ์
            if (_lastResult != null)
            {
                string msg =
                    $"Center (ROI coords)   ≈ ({_lastResult.Center.X:F2}, {_lastResult.Center.Y:F2})\n" +
                    $"Center (Image coords) ≈ ({_lastResult.CenterInImage.X:F2}, {_lastResult.CenterInImage.Y:F2})\n\n" +
                    _lastResult.SummaryText;

                MessageBox.Show(this, msg, "DieCenterSample",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Rectangle TransformPbToImage(Rectangle pbRect, PictureBox pb, Bitmap img)
        {
            float imgRatio = (float)img.Width / img.Height;
            float pbRatio = (float)pb.Width / pb.Height;

            float scale;
            int offsetX = 0, offsetY = 0;

            if (imgRatio > pbRatio)
            {
                scale = (float)img.Width / pb.Width;
                offsetY = (pb.Height - (int)(img.Height / scale)) / 2;
            }
            else
            {
                scale = (float)img.Height / pb.Height;
                offsetX = (pb.Width - (int)(img.Width / scale)) / 2;
            }

            int x = (int)((pbRect.X - offsetX) * scale);
            int y = (int)((pbRect.Y - offsetY) * scale);
            int w = (int)(pbRect.Width * scale);
            int h = (int)(pbRect.Height * scale);

            return new Rectangle(x, y, w, h);
        }

        private void ComputeZoomMapping(PictureBox pb, Bitmap img,
                                out float scale, out int offX, out int offY)
        {
            int pbW = pb.ClientSize.Width;
            int pbH = pb.ClientSize.Height;
            int imgW = img.Width;
            int imgH = img.Height;

            if (imgW <= 0 || imgH <= 0 || pbW <= 0 || pbH <= 0)
            {
                scale = 1f;
                offX = offY = 0;
                return;
            }

            float rx = (float)pbW / imgW;
            float ry = (float)pbH / imgH;
            scale = Math.Min(rx, ry);

            int drawW = (int)System.Math.Round(imgW * scale);
            int drawH = (int)System.Math.Round(imgH * scale);

            offX = (pbW - drawW) / 2;
            offY = (pbH - drawH) / 2;
        }

        private Rectangle TransformImageToPb(Rectangle imgRect, PictureBox pb, Bitmap img)
        {
            ComputeZoomMapping(pb, img, out float scale, out int offX, out int offY);

            int x = (int)System.Math.Round(imgRect.X * scale) + offX;
            int y = (int)System.Math.Round(imgRect.Y * scale) + offY;
            int w = (int)System.Math.Round(imgRect.Width * scale);
            int h = (int)System.Math.Round(imgRect.Height * scale);

            return new Rectangle(x, y, w, h);
        }


        private void DrawOverlay()
        {
            if (_currentBmp == null || _lastResult == null)
                return;

            // วาดบนสำเนาใหม่ จะได้ไม่ทำลายรูปต้นฉบับ
            var overlayBmp = (Bitmap)_currentBmp.Clone();

            var roi = _lastResult.RoiOnImage;  // image coordinates
            var corners = _lastResult.Corners; // ROI coordinates
            var center = _lastResult.Center;   // ROI coordinates

            using (var g = Graphics.FromImage(overlayBmp))
            using (var penRoi = new Pen(Color.Lime, 2))
            using (var penDiag = new Pen(Color.Red, 1))
            using (var brushCorner = new SolidBrush(Color.Yellow))
            using (var brushCenter = new SolidBrush(Color.Red))
            {
                // วาดกรอบ ROI
                g.DrawRectangle(penRoi, roi);

                // แปลงจุด Corners จาก ROI → Image (เพิ่ม Offset ROI.X/Y)
                if (corners != null && corners.Length == 4)
                {
                    PointF[] imgCorners = new PointF[4];
                    for (int i = 0; i < 4; i++)
                    {
                        imgCorners[i] = new PointF(
                            roi.X + corners[i].X,
                            roi.Y + corners[i].Y);
                    }

                    // วาดมุม
                    foreach (var c in imgCorners)
                    {
                        g.FillEllipse(brushCorner, c.X - 4, c.Y - 4, 8, 8);
                    }

                    // วาดเส้นทแยงมุม
                    g.DrawLine(penDiag, imgCorners[0], imgCorners[2]); // TL-BR
                    g.DrawLine(penDiag, imgCorners[1], imgCorners[3]); // TR-BL

                    // center (ROI → image)
                    var centerImg = new PointF(
                        roi.X + center.X,
                        roi.Y + center.Y);
                    g.FillEllipse(brushCenter, centerImg.X - 3, centerImg.Y - 3, 6, 6);
                }
            }

            // โชว์ใน PictureBox
            var old = pbMain.Image;
            pbMain.Image = overlayBmp;
            if (old != null && !ReferenceEquals(old, _currentBmp))
                old.Dispose();
        }

        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (!rbManualRoi.Checked || pbMain.Image == null || _currentBmp == null)
                return;

            // ขนาด ROI
            const int roiW = 480;
            const int roiH = 480;

            // ใช้ rect เล็ก ๆ แค่เพื่อหา x,y ของจุดคลิกบน image
            var tinyRectPb = new Rectangle(e.Location.X, e.Location.Y, 1, 1);
            Rectangle tinyInImg = TransformPbToImage(tinyRectPb, pbMain, _currentBmp);

            int imgX = tinyInImg.X;
            int imgY = tinyInImg.Y;

            // clamp ไม่ให้ ROI หลุดขอบภาพ
            if (imgX + roiW > _currentBmp.Width) imgX = _currentBmp.Width - roiW;
            if (imgY + roiH > _currentBmp.Height) imgY = _currentBmp.Height - roiH;
            if (imgX < 0) imgX = 0;
            if (imgY < 0) imgY = 0;

            // เก็บ ROI ที่จะส่งเข้า DLL (image coords)
            _manualRoiImage = new Rectangle(imgX, imgY, roiW, roiH);

            // สร้างกรอบ preview บน PictureBox
            _manualRoiPreview = TransformImageToPb(_manualRoiImage.Value, pbMain, _currentBmp);

            pbMain.Invalidate();
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            if (rbManualRoi.Checked && !_manualRoiPreview.IsEmpty)
            {
                using (var pen = new Pen(Color.Lime, 2))
                {
                    e.Graphics.DrawRectangle(pen, _manualRoiPreview);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_currentBmp == null)
                return;

            _lastResult = null;

            // ล้าง ROI manual ทั้ง image + preview
            _manualRoiImage = null;
            _manualRoiPreview = Rectangle.Empty;

            if (pbMain.Image != null)
            {
                pbMain.Image.Dispose();
                pbMain.Image = null;
            }

            pbMain.Image = (Bitmap)_currentBmp.Clone();
            pbMain.Invalidate();
        }
    }
}
