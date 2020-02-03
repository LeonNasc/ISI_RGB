using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Drawing;

namespace ISI_RGB
{
    class VideoProcessor
    {
        private VideoCapture video_feed;
        private List<Channels> buffered_channels = new List<Channels>();
        private string GraphPath { get; set; }
        private PlotModel plot_model;
        private LineSeries RedSeries, GreenSeries, BlueSeries;
        private Bitmap mediaPixels;
        private Bitmap curr_frame;
        private int frame_rate;

        public VideoProcessor(string path, string graph)
        {
            this.GraphPath = graph;

            try
            {
                this.video_feed = new VideoCapture(path);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("O arquivo não foi encontrado"); 
            }

            PrepararGrafico(graph);
        }

        public VideoProcessor(string graph)
        {
            this.GraphPath = graph;
            PrepararGrafico(graph);
        }

        private void PrepararGrafico(string graph)
        {
            this.plot_model = new PlotModel
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

        public void ProcessVideo(PlotView ui_plotter)
        {
            //using (var capture = new VideoCapture(this.FilePath)) // Loading video from file
            using (this.video_feed) // Loading video from file
            {
                if (this.video_feed.IsOpened)
                {
                    VideoProcessingLoop(video_feed, ui_plotter);
                    this.SavePlot(this.GraphPath, ui_plotter);
                }
                else
                {
                    return;
                }
            }
        }

        private void VideoProcessingLoop(VideoCapture a, PlotView ui_plotter)
        {
            this.frame_rate = (int) a.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            int frame_number = 0;
            var frame = video_feed.QueryFrame();

            while (frame !=null)
            {
                var frame_img = frame.ToImage<Bgr, byte>();

                // Plota a cada meio segundo
                if (frame_number % (frame_rate/2) == 0)
                {
                   Channels pixel = this.PixelAverage(frame_img);
                   this.mediaPixels = newBitmapFromRGB(pixel.red, pixel.green, pixel.blue);
                   this.WriteToFile(pixel);

                   Plot(frame_number / frame_rate, pixel, ui_plotter);
                }

                frame = video_feed.QueryFrame();
                this.curr_frame = frame_img.ToBitmap();
                frame_number++;
            }
            return;
        }

        private void WriteToFile(Channels pixel)
        {
            this.buffered_channels.Add(pixel);

            if (this.buffered_channels.Count > 1200)
            {
                this.SaveCSV(this.GraphPath + ".csv");
                this.buffered_channels.Clear();
            }
        }

        private Bitmap newBitmapFromRGB(double red, double green, double blue)
        {
            Bitmap b = new Bitmap(32, 32);
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

        private void Plot(double seconds, Channels point, PlotView plotter)
        {
            this.RedSeries.Points.Add(new DataPoint(seconds, point.red));
            this.GreenSeries.Points.Add(new DataPoint(seconds, point.green));
            this.BlueSeries.Points.Add(new DataPoint(seconds, point.blue));

            plotter.Model = plot_model;
            plotter.Model.InvalidatePlot(true);
        }

        public void SavePlot(string filename, PlotView plotter)
        {
            var PngExporter = new PngExporter { Width = 1024, Height = 768, Background = OxyColors.White };
            string path = $"plots/{filename}.jpg";

            if (!Directory.Exists("./plots"))
            {
                Directory.CreateDirectory("./plots");
            }
            PngExporter.Export(plotter.ActualModel, path, 1024, 768);
            this.SaveCSV($"plots/{filename}.csv");
        }

        /// <summary>
        /// Salva um CSV com os pontos de dados do vídeo
        /// </summary>
        /// <param name="nome">Nome do arquivo</param>
        internal void SaveCSV(string nome)
        {
            using (FileStream fs = new FileStream(nome, FileMode.Append))
            {
                using (var str = new StreamWriter(fs))
                {
                    str.WriteLine("Segundos;R;G;B", fs);
                    for (int i = 0; i < buffered_channels.Count; i++)
                    {
                        Channels ch = buffered_channels[i];
                        str.WriteLine("{0:2};{1:3};{2:3};{3:3}", i / this.frame_rate, (int)ch.red, (int)ch.green, (int)ch.blue);
                    }
                }
            }
        }

    }
}
