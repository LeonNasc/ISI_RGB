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

        static async Task Dispatch_args(string[] args)
        {
            if (args.Length != 3)
            {
                var menu = new MainMenu();
                Application.Run(menu);
                if (menu.filepath != "")
                {
                    string nome_arquivo = Path.GetFileNameWithoutExtension(menu.filepath);
                    ProcessarVideo(menu.filepath, $"gráfico_de_{nome_arquivo}");
                }

            }
            else if (args[0] == "--v")
            {
                ProcessarVideo(args[1], args[2]);
            }
        }

        private static void ProcessarVideo(string path, string nome_grafico)
        {
                    var processorTask = Task.Run(() =>
                    {
                        Application.EnableVisualStyles();
                        var Processor = new VideoProcessor(path, nome_grafico);
                        Console.WriteLine("Iniciando processamento...");
                        var loop = Task.Run(() =>
                        {
                            Console.WriteLine("Processando...");
                            Parallel.Invoke(
                                () => Application.Run(Processor),
                                () =>
                                {
                                    Processor.ProcessVideo();
                                    Processor.SavePlot(nome_grafico);
                                }
                            );
                        });
                        loop.Wait();
                    });

                    processorTask.Wait();

        }

    }

}