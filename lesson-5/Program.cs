using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using NLog;


namespace lesson_5
{
    class Program
    {
        public interface IStrategy
        {
            void Algorithm(string way, byte[] info);
        }
        public class CreateTxt : IStrategy
        {
            public void Algorithm(string way, byte[] info)
            {

                logger.Info("Create txt file");
                string byteTxt = Encoding.UTF8.GetString(info);
                File.WriteAllText(way+ "/byteTxt.txt", byteTxt);
            }
        }
        public class CreatePng : IStrategy
        {
            public void Algorithm(string way, byte[] info)
            {
                logger.Info("Create png file");
                var result = System.Text.Encoding.UTF8.GetString(info).Split(' ');
                int width = Convert.ToInt32(result[0])*10;
                int height = Convert.ToInt32(result[1])*10;

                Bitmap bmp = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(bmp);
                g.FillRectangle(Brushes.Green, 0, 0, width, height);
                g.Dispose();
                bmp.Save(way+@"\bytePng.png", System.Drawing.Imaging.ImageFormat.Png);
                bmp.Dispose();
            }
        }

        public class Context
        {
            private IStrategy _strategy;
            public Context(IStrategy strategy)
            {
                _strategy = strategy;
            }
            public void SetStrategy(IStrategy strategy)
            {
                _strategy = strategy;
            }
            public void ExecuteOperation(string way, byte[] info)
            {
                _strategy.Algorithm(way, info);
            }
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        static void Main(string[] args)
        {          
            int cpuUtilizationPercentage = 0;
            int memoryLoadPercentage = 0;
          
            Random rnd = new Random();
            cpuUtilizationPercentage = rnd.Next(0, 100);
            memoryLoadPercentage = rnd.Next(0, 100);

            logger.Info("Cpu="+ cpuUtilizationPercentage+"%,Memory="+ memoryLoadPercentage+"%");
            string way = Environment.CurrentDirectory;

            byte[] array = Encoding.Default.GetBytes(cpuUtilizationPercentage.ToString()+' '+ memoryLoadPercentage.ToString());
            File.WriteAllBytes(way + "/info.bin", array);
            var byteResult = File.ReadAllBytes(way + "/info.bin");
            Console.WriteLine("Cpu=" + cpuUtilizationPercentage + "%, Memory=" + memoryLoadPercentage + "%");

            Context context = new Context(new CreateTxt());
            context.ExecuteOperation(way, byteResult);
            context.SetStrategy(new CreatePng());
            context.ExecuteOperation(way, byteResult);

            Console.ReadLine();
        }
    }
}
