using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISI_RGB
{
    partial class Frontend : Form
    {

        private VideoProcessor vp { get; set; }

        /// <summary>
        /// Construtor para leitura a partir de arquivo de vídeo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="graph"></param>
        public Frontend(string path, string graph)
        {
            this.InitializeComponent();
            vp = new VideoProcessor(path, graph);
        }


        /// <summary>
        /// Construtor para leitura a partir do feed
        /// </summary>
        public Frontend(string graph)
        {
            this.InitializeComponent();
            this.vp = new VideoProcessor(graph);
        }

        private void VideoProcessor_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Definindo área de corte"); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             var VideoProcessTask = Task.Run(() =>
                    {
                        vp.ProcessVideo(Plotter);
                    });

            VideoProcessTask.Wait();
        }
    }
}
