using Autofac;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using NLog;
using System.Linq;
using Autofac.Features.Metadata;


namespace lesson_6
{
    public class WayAndByte
    {
        public WayAndByte(byte[] info, string way)
        {
            _info = info;
            _way = way;
        }
        public byte[] _info { get; set; }
        public string _way { get; set; }
    }
    class Program
    {
       

        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            int cpuUtilizationPercentage = 0;
            int memoryLoadPercentage = 0;

            Random rnd = new Random();
            cpuUtilizationPercentage = rnd.Next(0, 100);
            memoryLoadPercentage = rnd.Next(0, 100);
          
            logger.Info("Cpu=" + cpuUtilizationPercentage + "%,Memory=" + memoryLoadPercentage + "%");
            string way = Environment.CurrentDirectory;

            byte[] array = Encoding.Default.GetBytes(cpuUtilizationPercentage.ToString() + ' ' + memoryLoadPercentage.ToString());
            File.WriteAllBytes(way + "/info.bin", array);
            var byteResult = File.ReadAllBytes(way + "/info.bin");
            Console.WriteLine("Cpu=" + cpuUtilizationPercentage + "%, Memory=" + memoryLoadPercentage + "%");

            WayAndByte newWayAndByte = new WayAndByte(array, way);
            var builder = new ContainerBuilder();

            builder.RegisterType<FirstPipelineItem>().As<IPipelineItem>().WithMetadata("Name", "First pipe item");
            builder.RegisterType<SecondPipelineItem>().As<IPipelineItem>().WithMetadata("Name", "Second pipe item");

            builder.RegisterAdapter<Meta<IPipelineItem>, Operation>(
                cmd => new Operation(cmd.Value, (string)cmd.Metadata["Name"]));

            IContainer container = builder.Build();
            IReadOnlyList<Operation> operations = container.Resolve<IEnumerable<Operation>>().ToList();

            foreach (Operation operation in operations)
            {
                Console.WriteLine($"Operation name is {operation.Name}");
                operation.Execute(newWayAndByte);
            }
            Console.ReadLine();
        }
    }

    public interface IPipelineItem 
    {
        string Name { get; }
        void Run(WayAndByte newWayAndByte);
    }

    public abstract class PipelineItem : IPipelineItem
    {
        public abstract string Name { get; }
        public abstract void Run(WayAndByte newWayAndByte);
    }

    public sealed class FirstPipelineItem : PipelineItem
    {
         public override string Name => $"{nameof(FirstPipelineItem)}";

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void Run(WayAndByte newWayAndByte)
        {
            string byteTxt = Encoding.UTF8.GetString(newWayAndByte._info);
            File.WriteAllText(newWayAndByte._way + "/byteTxt.txt", byteTxt);
            logger.Info($"{Name} Create txt file");
        }   
    }

    public sealed class SecondPipelineItem : PipelineItem
    {     
        public override string Name => $"{nameof(SecondPipelineItem)}";
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override void Run(WayAndByte newWayAndByte)
        {

            var result = System.Text.Encoding.UTF8.GetString(newWayAndByte._info).Split(' ');
            int width = Convert.ToInt32(result[0]) * 10;
            int height = Convert.ToInt32(result[1]) * 10;
            logger.Info($"{Name} Create png file");
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.Green, 0, 0, width, height);
            g.Dispose();
            bmp.Save(newWayAndByte._way + @"\bytePng.png", System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
        }
    }

    public sealed class Operation
    {
        private readonly IPipelineItem _pipelineItem;
        private readonly string _name;
   
        public Operation(IPipelineItem pipelineItem, string name)
        {
            _pipelineItem = pipelineItem;       
            _name = name;
      
        }
        public string Name => _name;
        public void Execute(WayAndByte newWayAndByte)
        {   
            _pipelineItem.Run(newWayAndByte);
        }
    }
}
