using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

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

    static class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(()=>Dispatch_args(args));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static void Dispatch_args(string[] args)
        {
            var menu = new MainMenu();
            Application.Run(menu);

            if (menu.filepath != null)
            {
                string nome_arquivo = Path.GetFileNameWithoutExtension(menu.filepath);
                ProcessarVideo(menu.filepath, $"gráfico_de_{nome_arquivo}");
            }
            else
            {
                ProcessarVideo("", $"gráfico_de_{new DateTime().ToLocalTime()}");
            }
        }

        private static void ProcessarVideo(string path, string nome_grafico)
        {
                    var processorTask = Task.Run(() =>
                    {
                        Application.EnableVisualStyles();
                        var Processor = new Frontend(path, nome_grafico);
                        Application.Run(Processor);
                    });
                    processorTask.Wait();
        }
    }
}