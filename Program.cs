using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Threading.Tasks;
using System.Threading;

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
            await Dispatch_args(args);
        }

        static async Task Dispatch_args(string[] args)
        {
            if (args.Length != 3)
            {
                //qtde de argumentos invalidos
                Console.WriteLine("Modo de usar: ISI_RGB.exe --v nome_do_video nome_do_grafico");
                Environment.Exit(0);
            }
            else if (args[0] == "--v")
            {
                var processorTask = Task.Run(() =>
                {
                    Application.EnableVisualStyles();
                    var Processor = new VideoProcessor(args[1], args[2]);
                    Console.WriteLine("Iniciando processamento...");
                    var loop = Task.Run(() =>
                    {
                        Console.WriteLine("Processando...");
                        Parallel.Invoke(
                            () => Application.Run(Processor),
                            () =>
                            {
                                Processor.ProcessVideo();
                                Processor.SavePlot(args[2]);
                            }
                        );
                    });
                    loop.Wait();
                });

                processorTask.Wait();
            }
            else
            {
                //Formato de argumento invalido
                Console.WriteLine("Modo de usar: ISI_RGB.exe --v nome_do_video nome_do_grafico");
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
                        await VideoProcessingLoop(capture, frame);
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
                Console.WriteLine(e.Message);
                Console.WriteLine("Não foi possível abrir o arquivo");
            }
        }
        private async Task VideoProcessingLoop(VideoCapture a, Mat b)
        {
            int frame_count = 0;
            while (true)
            {
                var frame = a.QueryFrame();
                if (frame != null)
                {
                    Channels pixel = this.PixelAverage(frame.ToImage<Bgr, byte>());
                    this.channels.Add(pixel);
                    if(frame_count%5==0)
                         Plot(frame_count/30.0,pixel);

                    frame_count++;
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

            //definir o inicial valor dos canais
            double red = 0;
            double green = 0;
            double blue = 0;

            for (int i = 0; i < height; i++)
            {
                for(int j= 0;j< width;j++)
                {
                    blue += (double)Convert.ToInt32(image.Data[i, j, 0]) / size;
                    green += (double)Convert.ToInt32(image.Data[i, j, 1]) / size;
                    red += (double)Convert.ToInt32(image.Data[i, j, 2]) / size;

                };
            }
            return new Channels(red, green, blue);
        }

        private void Plot(double seconds,Channels point)
        {
            this.RedSeries.Points.Add(new DataPoint(seconds,point.red));
            this.GreenSeries.Points.Add(new DataPoint(seconds, point.green));
            this.BlueSeries.Points.Add(new DataPoint(seconds, point.blue));


            this.Plotter.Model = standard_pm;
            Plotter.Model.InvalidatePlot(true);
        }

        public void SavePlot(string filename)
        {
            var PngExporter = new PngExporter { Width = 1024, Height = 768, Background = OxyColors.White };
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