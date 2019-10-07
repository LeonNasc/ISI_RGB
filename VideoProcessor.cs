using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Threading.Tasks;
using System.Drawing;

namespace ISI_RGB
{
    partial class VideoProcessor : Form
    {

        private List<Channels> channels = new List<Channels>();
        private string FilePath { get; set; }
        private string GraphPath { get; set; }
        private PlotModel standard_pm;
        private LineSeries RedSeries, GreenSeries, BlueSeries;

        public VideoProcessor(string path, string graph)
        {
            this.InitializeComponent();
            this.FilePath = path;
            this.GraphPath = graph;
            PrepararGrafico(graph);
        }

        public VideoProcessor()
        {
            this.InitializeComponent();
            // Exibir tela de seleção
            PrepararGrafico("Lorem");
        }

        private void PrepararGrafico(string graph)
        {
            this.standard_pm = new PlotModel
            {
                Title = "Canais RGB",
                Subtitle = graph,
                PlotType = PlotType.XY,
                PlotAreaBackground = OxyColors.White,
                Background = OxyColors.White
            };

            this.standard_pm.Axes.Add(
                    new LinearAxis()
                    {
                        Position = OxyPlot.Axes.AxisPosition.Left,
                        Maximum = 255,
                        Minimum = 0,
                    }
                );

            //O eixo X é plotado no fim do processamento
            this.standard_pm.Axes.Add(
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
            this.standard_pm.Series.Add(this.RedSeries);
            this.standard_pm.Series.Add(this.GreenSeries);
            this.standard_pm.Series.Add(this.BlueSeries);
        }

        public async Task ProcessVideo()
        {
            try
            {
                //Capturar erro caso o video não exista
                using (var capture = new VideoCapture(this.FilePath)) // Loading video from file
                {
                    if (capture.IsOpened)
                    {
                        var frame = capture.QueryFrame();
                        VideoProcessingLoop(capture, frame);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Não foi possível encontrar o arquivo {this.FilePath}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu uma exceção");
            }
        }
        private void VideoProcessingLoop(VideoCapture a, Mat b)
        {
            int frame_count = 0;
            while (true)
            {
                var frame = a.QueryFrame();
                if (frame != null)
                {
                    var frame_img = frame.ToImage<Bgr, byte>();
                    Channels pixel = this.PixelAverage(frame_img);
                    this.channels.Add(pixel);

                    if (frame_count % 2 == 0)
                    {
                       Plot(frame_count / 30.0, pixel);
                       mediaPixels.Image = newBitmapFromRGB(pixel.red, pixel.green, pixel.blue);
                       videoFrames.Image = frame_img.ToBitmap();
                    }

                    frame_count++;
                }
                else
                {
                    Console.WriteLine("Processamento finalizado");
                    return;
                }
            }
        }

        private Bitmap newBitmapFromRGB(double red, double green, double blue)
        {
            Bitmap b = new Bitmap(64, 64);
            using (Graphics g = Graphics.FromImage(b))
                g.Clear(Color.FromArgb((int)red, (int)green, (int)blue));

            return b;
        }

        private Channels PixelAverage(Image<Bgr, byte> image)
        {
            int width = image.Cols;
            int height = image.Rows;
            int size = width * height;

            //definir o inicial valor dos canais
            double red = 0;
            double green = 0;
            double blue = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    blue += (double)Convert.ToInt32(image.Data[i, j, 0]) / size;
                    green += (double)Convert.ToInt32(image.Data[i, j, 1]) / size;
                    red += (double)Convert.ToInt32(image.Data[i, j, 2]) / size;

                };
            }
            return new Channels(red, green, blue);
        }

        private void Plot(double seconds, Channels point)
        {
            this.RedSeries.Points.Add(new DataPoint(seconds, point.red));
            this.GreenSeries.Points.Add(new DataPoint(seconds, point.green));
            this.BlueSeries.Points.Add(new DataPoint(seconds, point.blue));

            this.Plotter.Model = standard_pm;
            Plotter.Model.InvalidatePlot(true);
        }

        private void VideoProcessor_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void SavePlot(string filename)
        {
            var PngExporter = new PngExporter { Width = 1024, Height = 768, Background = OxyColors.White };
            string path = $"plots/{filename}.jpg";

            if (!Directory.Exists("./plots"))
            {
                Directory.CreateDirectory("./plots");
            }

            PngExporter.Export(this.Plotter.ActualModel, path, 1024, 768);
            this.SaveCSV($"plots/{filename}.csv");
            Console.WriteLine($"Gráfico salvo com sucesso em {path}");
        }

        /// <summary>
        /// Salva um CSV com os pontos de dados do vídeo
        /// </summary>
        /// <param name="nome">Nome do arquivo</param>
        internal void SaveCSV(string nome)
        {
            using (FileStream fs = new FileStream(nome, FileMode.Create))
            {
                using (var str = new StreamWriter(fs))
                {
                    str.WriteLine("Segundos;R;G;B", fs);
                    for (int i = 0; i < channels.Count; i++)
                    {
                        Channels ch = channels[i];
                        str.WriteLine("{0:2};{1:3};{2:3};{3:3}", i / 30.0, ch.red, ch.green, ch.blue);
                    }
                }
            }
        }
    }
}
