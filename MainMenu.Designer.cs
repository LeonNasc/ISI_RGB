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
            this.AbrirBTN = new System.Windows.Forms.Button();
            this.ExecutarBTN = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.BackColor = System.Drawing.SystemColors.Menu;
            this.Titulo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Titulo.Font = new System.Drawing.Font("Arial", 24F);
            this.Titulo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Titulo.Location = new System.Drawing.Point(12, 16);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(148, 36);
            this.Titulo.TabIndex = 0;
            this.Titulo.Text = "IQV-RGB";
            this.Titulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AbrirBTN
            // 
            this.AbrirBTN.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.AbrirBTN.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AbrirBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbrirBTN.Location = new System.Drawing.Point(12, 80);
            this.AbrirBTN.Name = "AbrirBTN";
            this.AbrirBTN.Size = new System.Drawing.Size(366, 43);
            this.AbrirBTN.TabIndex = 1;
            this.AbrirBTN.Text = "Abrir arquivo de vídeo";
            this.AbrirBTN.UseVisualStyleBackColor = false;
            this.AbrirBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExecutarBTN
            // 
            this.ExecutarBTN.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ExecutarBTN.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExecutarBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecutarBTN.Location = new System.Drawing.Point(13, 130);
            this.ExecutarBTN.Name = "ExecutarBTN";
            this.ExecutarBTN.Size = new System.Drawing.Size(365, 44);
            this.ExecutarBTN.TabIndex = 3;
            this.ExecutarBTN.Text = "Executar da webcam";
            this.ExecutarBTN.UseVisualStyleBackColor = false;
            this.ExecutarBTN.Click += new System.EventHandler(this.ExecutarBTN_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Titulo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 191);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 191);
            this.Controls.Add(this.ExecutarBTN);
            this.Controls.Add(this.AbrirBTN);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Button ExecutarBTN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button AbrirBTN;
    }
}