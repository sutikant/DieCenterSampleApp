
namespace DieCenterSampleApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDetect = new System.Windows.Forms.Button();
            this.rbAutoRoi = new System.Windows.Forms.RadioButton();
            this.rbManualRoi = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(163, 79);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(550, 550);
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::DieCenterSampleApp.Properties.Resources.image_pic;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(124, 42);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Image = global::DieCenterSampleApp.Properties.Resources.center_pic;
            this.btnDetect.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetect.Location = new System.Drawing.Point(12, 138);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(124, 42);
            this.btnDetect.TabIndex = 2;
            this.btnDetect.Text = "DETECT";
            this.btnDetect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // rbAutoRoi
            // 
            this.rbAutoRoi.AutoSize = true;
            this.rbAutoRoi.Location = new System.Drawing.Point(21, 19);
            this.rbAutoRoi.Name = "rbAutoRoi";
            this.rbAutoRoi.Size = new System.Drawing.Size(69, 17);
            this.rbAutoRoi.TabIndex = 3;
            this.rbAutoRoi.TabStop = true;
            this.rbAutoRoi.Text = "Auto ROI";
            this.rbAutoRoi.UseVisualStyleBackColor = true;
            // 
            // rbManualRoi
            // 
            this.rbManualRoi.AutoSize = true;
            this.rbManualRoi.Location = new System.Drawing.Point(21, 42);
            this.rbManualRoi.Name = "rbManualRoi";
            this.rbManualRoi.Size = new System.Drawing.Size(82, 17);
            this.rbManualRoi.TabIndex = 4;
            this.rbManualRoi.TabStop = true;
            this.rbManualRoi.Text = "Manual ROI";
            this.rbManualRoi.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAutoRoi);
            this.groupBox1.Controls.Add(this.rbManualRoi);
            this.groupBox1.Location = new System.Drawing.Point(12, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 72);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ROI Mode Selection";
            // 
            // btnReset
            // 
            this.btnReset.Image = global::DieCenterSampleApp.Properties.Resources.reset_pic;
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReset.Location = new System.Drawing.Point(12, 186);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(124, 42);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "RESET";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Firebrick;
            this.label16.Location = new System.Drawing.Point(165, 12);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(548, 52);
            this.label16.TabIndex = 36;
            this.label16.Text = resources.GetString("label16.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 655);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pbMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Die Center Detector Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.RadioButton rbAutoRoi;
        private System.Windows.Forms.RadioButton rbManualRoi;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label16;
    }
}

