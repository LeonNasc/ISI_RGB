using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Threading.Tasks;

namespace ISI_RGB
{
    internal struct Channels
    {
        public double red;
        public double green;
        public double blue;

        public Channels(double r, double g, double b)
        {
            red = r;
            green = g;
            blue = b;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(true);

            Dispatch_args(args);
        }

        static async Task Dispatch_args(string[] args)
        {
            if (args.Length != 3)
            {
                //qtde de argumentos invalidos
                Console.WriteLine("Modo de usar: EmguApp.exe --v nome_do_video nome_do_grafico");
                Environment.Exit(0);
            }
            else if (args[0] == "--v")
            {
                var Processor = new VideoProcessor(args[1], args[2]);
                Console.WriteLine("Iniciando processamento...");
                var loop = Task.Run(() =>
                {
                    Processor.ProcessVideo();
                    Processor.savePlot(args[2]);
                });
                loop.Wait();

                var show = Task.Run(() =>
                {
                    Mat picture = new Mat(@"img.jpg"); // Pick some path on your disk!
                    CvInvoke.Imshow("Hello World!", picture); // Open window with image
                    CvInvoke.WaitKey(); // Render image and keep window opened until any key is pressed
                });
            }
            else
            {
                //Formato de argumento invalido
                Console.WriteLine("Modo de usar: EmguApp.exe --v nome_do_video nome_do_grafico");
                Environment.Exit(1);
            }
        }
    }


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
            this.standard_pm = new PlotModel
            {
                Title = "Canais RGB",
                Subtitle = "Teste",
                PlotType = PlotType.Cartesian,
                Background = OxyColors.White
            };

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


        public void ProcessVideo()
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
            catch (Exception)
            {
                Console.WriteLine("Não foi possível abrir o arquivo");
            }
        }
        private void VideoProcessingLoop(VideoCapture a, Mat b)
        {
            while (true)
            {
                var frame = a.QueryFrame();
                if (frame != null)
                {
                    Channels pixel = this.PixelAverage(frame.ToImage<Bgr, byte>());
                    this.channels.Add(pixel);

                    //CvInvoke.Imshow("VideoProcessor",frame);
                    //this.Plot();
                    //CvInvoke.WaitKey(); // Render image and keep window opened until any key is pressed
                }
                else
                {
                    Console.WriteLine("Processamento finalizado");
                    return;
                }
            }
        }

        private Channels PixelAverage(Image<Bgr, byte> image)
        {
            int width = image.Cols;
            int height = image.Rows;
            int size = width * height;

            //definir o valor dos canais
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

                }
            }
            return new Channels(red, green, blue);
        }

        private void Plot()
        {
            //const int framerate = 30;
            int pointCount = this.channels.Count;
            double[] red = new double[pointCount];
            double[] green = new double[pointCount];
            double[] blue = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                red[i] = this.channels[i].red;
                green[i] = this.channels[i].green;
                blue[i] = this.channels[i].blue;
                this.RedSeries.Points.Add(new DataPoint(i, red[i]));
                this.GreenSeries.Points.Add(new DataPoint(i, green[i]));
                this.BlueSeries.Points.Add(new DataPoint(i, blue[i]));
            }

            this.Plotter.Model = standard_pm;
            //Image<Bgr, byte> img = new Image<Bgr, byte>(this.plotter.GetBitmap(renderFirst:true, lowQuality:false));
            //CvInvoke.Imshow("Plot", img.Mat);
            //CvInvoke.WaitKey();

        }

        public void savePlot(string filename)
        {
            this.Plot();
            var PngExporter = new PngExporter { Width = 800, Height = 600, Background = OxyColors.White };
            string path = $"plots/{filename}.jpg";
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
                    str.WriteLine("Segundos, R, G, B", fs);
                    for (int i = 0; i < channels.Count; i++)
                    {
                        Channels ch = channels[i];
                        str.WriteLine("{0}, {1}, {2},{3}", i / 30.0, ch.red, ch.green, ch.blue);
                    }
                }
            }
        }
    }
}