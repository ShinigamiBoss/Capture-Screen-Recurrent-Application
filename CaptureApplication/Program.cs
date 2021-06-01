using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace CaptureApplication
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Insert process name, see task manager details of the running application: ");
            string ProcessName = Console.ReadLine();

            Console.WriteLine("Executable path:");
            Process.Start(Console.ReadLine());

            int XStart, YStart, AreaX, AreaY, interval;

            Console.WriteLine("Insert capture X start coordinate:");
            while (!int.TryParse(Console.ReadLine(), out XStart))
            {
                Console.WriteLine("Please enter a integer number");
            }

            Console.WriteLine("Insert capture Y start coordinate:");
            while (!int.TryParse(Console.ReadLine(), out YStart))
            {
                Console.WriteLine("Please enter a integer number");
            }

            Console.WriteLine("Insert capture area width:");
            while (!int.TryParse(Console.ReadLine(), out AreaX))
            {
                Console.WriteLine("Please enter a integer number");
            }

            Console.WriteLine("Insert capture area heigth:");
            while (!int.TryParse(Console.ReadLine(), out AreaY))
            {
                Console.WriteLine("Please enter a integer number");
            }

            Console.WriteLine("Insert optional time interval in seconds, inter 0 for none:");
            while (!int.TryParse(Console.ReadLine(), out interval))
            {
                Console.WriteLine("Please enter a integer number");
            }

            Size CaptureSize = new(AreaX, AreaY);

            int x = 0;

            while (Process.GetProcessesByName(ProcessName).Length > 0)
            {
                Image image = ImageCapturer.CaptureScreen(XStart, YStart, 0, 0, CaptureSize);
                image.Save(Environment.CurrentDirectory + $"\\image{x}.png");
                Console.WriteLine($"Image {x} caputured");
                if (interval > 0)
                {
                    Thread.Sleep(interval * 1000);
                }
                x++;
            }

            Console.WriteLine("Capture process completed, press any key to close.");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// This class is responsable for the capture process
    /// </summary>
    public class ImageCapturer
    {
        public static Image CaptureScreen(int sourceX, int sourceY, int destX, int destY,
            Size regionSize)
        {
            Bitmap bmp = new(regionSize.Width, regionSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(sourceX, sourceY, destX, destY, regionSize);
            return bmp;
        }
    }
}