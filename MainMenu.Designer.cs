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
            this.selectBTN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ExecutarBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // selectBTN
            // 
            this.selectBTN.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.selectBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectBTN.Location = new System.Drawing.Point(12, 80);
            this.selectBTN.Name = "selectBTN";
            this.selectBTN.Size = new System.Drawing.Size(366, 43);
            this.selectBTN.TabIndex = 1;
            this.selectBTN.Text = "Abrir arquivo de vídeo";
            this.selectBTN.UseVisualStyleBackColor = false;
            this.selectBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 180);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(366, 277);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // ExecutarBTN
            // 
            this.ExecutarBTN.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ExecutarBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExecutarBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecutarBTN.Location = new System.Drawing.Point(13, 130);
            this.ExecutarBTN.Name = "ExecutarBTN";
            this.ExecutarBTN.Size = new System.Drawing.Size(365, 44);
            this.ExecutarBTN.TabIndex = 3;
            this.ExecutarBTN.Text = "Executar";
            this.ExecutarBTN.UseVisualStyleBackColor = false;
            this.ExecutarBTN.Click += new System.EventHandler(this.ExecutarBTN_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 469);
            this.Controls.Add(this.ExecutarBTN);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.selectBTN);
            this.Controls.Add(this.Titulo);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Button selectBTN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ExecutarBTN;
    }
}