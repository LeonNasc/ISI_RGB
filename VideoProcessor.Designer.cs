// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.Designer.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using OxyPlot.WindowsForms;

namespace ISI_RGB
{
    partial class VideoProcessor
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
            this.Plotter = new OxyPlot.WindowsForms.PlotView();
            this.videoFrames = new System.Windows.Forms.PictureBox();
            this.mediaPixels = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.videoFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPixels)).BeginInit();
            this.SuspendLayout();
            // 
            // Plotter
            // 
            this.Plotter.Dock = System.Windows.Forms.DockStyle.Right;
            this.Plotter.Location = new System.Drawing.Point(146, 0);
            this.Plotter.Margin = new System.Windows.Forms.Padding(0);
            this.Plotter.Name = "Plotter";
            this.Plotter.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.Plotter.Size = new System.Drawing.Size(799, 446);
            this.Plotter.TabIndex = 0;
            this.Plotter.Text = ":w";
            this.Plotter.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.Plotter.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.Plotter.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // videoFrames
            // 
            this.videoFrames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videoFrames.Location = new System.Drawing.Point(0, 71);
            this.videoFrames.Name = "videoFrames";
            this.videoFrames.Size = new System.Drawing.Size(143, 126);
            this.videoFrames.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.videoFrames.TabIndex = 1;
            this.videoFrames.TabStop = false;
            this.videoFrames.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // mediaPixels
            // 
            this.mediaPixels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mediaPixels.Location = new System.Drawing.Point(0, 227);
            this.mediaPixels.Name = "mediaPixels";
            this.mediaPixels.Size = new System.Drawing.Size(143, 126);
            this.mediaPixels.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mediaPixels.TabIndex = 2;
            this.mediaPixels.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Frame atual";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Média dos pixels";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // VideoProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 446);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mediaPixels);
            this.Controls.Add(this.videoFrames);
            this.Controls.Add(this.Plotter);
            this.Name = "VideoProcessor";
            this.Text = "OxyPlot in Windows Forms";
            this.Load += new System.EventHandler(this.VideoProcessor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPixels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PlotView Plotter;
        private System.Windows.Forms.PictureBox videoFrames;
        private System.Windows.Forms.PictureBox mediaPixels;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}