using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.IO;
using System.Linq;

namespace ISI_RGB
{
    partial class Frontend : Form
    {
        private VideoProcessor vp { get; set; }
        public string GraphPath { get; private set; }

        /// <summary>
        /// Construtor para leitura a partir de arquivo de vídeo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="graph"></param>
        public Frontend(string path, string graph)
        {
            this.InitializeComponent();
            this.GraphPath = graph;
            vp = new VideoProcessor(path,this);
            this.PrepararGrafico(graph);
        }


        /// <summary>
        /// Construtor para leitura a partir do feed
        /// </summary>
        public Frontend(string graph)
        {
            this.InitializeComponent();
            this.GraphPath = graph;
            this.vp = new VideoProcessor(this);
            this.PrepararGrafico(graph);
        }

        private void PrepararGrafico(string graph)
        {
            this.plot_model= new PlotModel
            {
                Title = "Canais RGB",
                Subtitle = graph,
                PlotType = PlotType.XY,
                PlotAreaBackground = OxyColors.White,
                Background = OxyColors.White
            };

            this.plot_model.Axes.Add(
                    new LinearAxis()
                    {
                        Position = OxyPlot.Axes.AxisPosition.Left,
                        Maximum = 255,
                        Minimum = 0,
                    }
                );

            //O eixo X é plotado no fim do processamento
            this.plot_model.Axes.Add(
                new LinearAxis()
                {
                    Position = OxyPlot.Axes.AxisPosition.Bottom,
                    AbsoluteMinimum = 0,
                }
            );

            this.RedSeries = new LineSeries
            {
                Color = OxyColors.Red,
                Title = "Red",
            };
            this.GreenSeries = new LineSeries
            {
                Color = OxyColors.Green,
                Title = "Green",
            };
            this.BlueSeries = new LineSeries
            {
                Color = OxyColors.Blue,
                Title = "Blue"
            };
            this.plot_model.Series.Add(this.RedSeries);
            this.plot_model.Series.Add(this.GreenSeries);
            this.plot_model.Series.Add(this.BlueSeries);
        }

        public void SavePlot(string filename)
        {
            var PngExporter = new PngExporter { Width = 1024, Height = 768, Background = OxyColors.White };
            string path = $"plots/{filename.Split(Path.DirectorySeparatorChar).Last()}.jpg";

            if (!Directory.Exists("./plots"))
            {
                Directory.CreateDirectory("./plots");
            }

            PngExporter.Export(Plotter.ActualModel, $"plots/graph_{filename}.png", 1366, 768);
            this.vp.SaveCSV($"plots/{filename}.csv");
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

        }

        private void stopBTN_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Salvando resultados");
            this.SavePlot(this.GraphPath);
            this.Close();
        }
    }
}
