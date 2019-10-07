namespace ISI_RGB
{
    partial class MainMenu
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
            this.Titulo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Titulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Titulo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Titulo.Font = new System.Drawing.Font("Arial", 24F);
            this.Titulo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Titulo.Location = new System.Drawing.Point(36, 28);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(323, 38);
            this.Titulo.TabIndex = 0;
            this.Titulo.Text = "Plotador RGB ISI_QV";
            this.Titulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(366, 54);
            this.button1.TabIndex = 1;
            this.button1.Text = "Abrir arquivo de vídeo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Titulo);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Button button1;
    }
}