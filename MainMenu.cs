using System;
using System.Windows.Forms;

namespace ISI_RGB
{
    public partial class MainMenu : Form
    {
        public string filepath { get; private set; }

        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           var dialog = new OpenFileDialog();
           dialog.Filter = "Arquivos de vídeo|*.mp4;*.mov;*.avi;*.wmv";
           dialog.Title = "Selecione um arquivo de vídeo";
           DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.filepath = dialog.FileName;
                if(dialog.FileName != "")
                {
                    Frontend fr = new Frontend(this.filepath, dialog.FileName);
                    fr.ShowDialog();
                }
            }
            else
            {
                return;
            }
        }

        private void ExecutarBTN_Click(object sender, EventArgs e)
        {
            Frontend fr = new Frontend($"Gráfico de {DateTime.Now.ToLongDateString()}");
            fr.ShowDialog();
        }
    }
}
