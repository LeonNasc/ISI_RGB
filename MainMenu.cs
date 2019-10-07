using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISI_RGB
{
    public partial class MainMenu : Form
    {
        private System.Windows.Forms.OpenFileDialog dialog;
        public string filepath { get; private set; }

        public MainMenu()
        {
            InitializeComponent();
            dialog = new OpenFileDialog();
            dialog.Filter = "Arquivos de vídeo|*.mp4;*.mov";
            dialog.Title = "Selecione um arquivo de vídeo";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.filepath = dialog.FileName;
                if(dialog.FileName != "")
                    this.Close();
            }
        }
    }
}
