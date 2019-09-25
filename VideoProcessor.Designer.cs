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
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.Plotter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plotter.Location = new System.Drawing.Point(0, 0);
            this.Plotter.Margin = new System.Windows.Forms.Padding(0);
            this.Plotter.Name = "Plotter";
            this.Plotter.Size = new System.Drawing.Size(632, 446);
            this.Plotter.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.Plotter);
            this.Name = "Plotter";
            this.Text = "OxyPlot in Windows Forms";
            this.ResumeLayout(false);

        }

        #endregion

        private PlotView Plotter;
    }
}