using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.WindowsForms;
using System.Drawing;
using System.Windows.Forms;

namespace ISI_RGB
{
    class VideoProcessor
    {
        private VideoCapture video_feed;
        private List<Channels> buffered_channels = new List<Channels>();
        private int frame_rate;
        private Frontend parent;

        public VideoProcessor(string video_path, Form parent_component)
        {
            this.parent = (Frontend)parent_component;
            
            try
            {
                this.video_feed = new VideoCapture(video_path);
                this.parent.videoFrames.Image = video_feed.QuerySmallFrame().ToImage<Bgr, Byte>().ToBitmap();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("O arquivo não foi encontrado"); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public VideoProcessor(Form parent_component)
        {
            this.parent = (Frontend) parent_component;
            this.video_feed = new VideoCapture();
            this.parent.videoFrames.Image = video_feed.QuerySmallFrame().ToImage<Bgr, Byte>().ToBitmap();
        }

        public void ProcessVideo(PlotView ui_plotter)
        {
            //using (var capture = new VideoCapture(this.FilePath)) // Loading video from file
            using (this.video_feed) // Loading video from file
            {
                if (this.video_feed.IsOpened)
                {
                    VideoProcessingLoop(video_feed, ui_plotter);
                }
                else
                {
                    parent.SavePlot(parent.GraphPath);
                    return;
                }
            }
        }

        private void VideoProcessingLoop(VideoCapture a, PlotView ui_plotter)
        {
            this.frame_rate = (int) a.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            int frame_number = 0;
            var frame = video_feed.QueryFrame();

            while ((frame = video_feed.QueryFrame()) !=null)
            {
                try
                {
                   var frame_img = frame.ToImage<Bgr, byte>();
                   Channels pixel = this.PixelAverage(frame_img);
                   this.parent.mediaPixels.Image= newBitmapFromRGB(pixel.red, pixel.green, pixel.blue);
                   this.WriteToFile(pixel);

                   Plot(frame_number / frame_rate, pixel, ui_plotter);

                    this.parent.videoFrames.Image = frame_img.ToBitmap();
                    frame_number++;
                }
                catch (Exception)
                {
                    break;
                }
            }
            return;
        }

        private void WriteToFile(Channels pixel)
        {
            this.buffered_channels.Add(pixel);

            if (this.buffered_channels.Count > 1200)
            {
                this.SaveCSV(this.parent.GraphPath + ".csv");
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
            parent.RedSeries.Points.Add(new DataPoint(seconds, point.red));
            parent.GreenSeries.Points.Add(new DataPoint(seconds, point.green));
            parent.BlueSeries.Points.Add(new DataPoint(seconds, point.blue));

            parent.Plotter.Model = parent.plot_model;
            parent.Plotter.Model.InvalidatePlot(true);
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
