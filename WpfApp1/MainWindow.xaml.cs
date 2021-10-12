using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {


        public List<MyList> someList = new List<MyList>();
        public int interval;
        public MainWindow()
        {
            InitializeComponent();


            for (int i = 0; i < 50; i++)
            {

                Thread function = new Thread(() => Fibonacci(i));
                function.Start();
              //  function.Interrupt();
                function.Join();

            }

            Thread f = new Thread(() => ListAdd("qwe",1));
            Thread q = new Thread(ListDelete);
            f.Start();
            f.Join();
            q.Start();

        }




        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            interval= (Convert.ToInt32(Slider.Value));
        }


        public void ListAdd(string str, int n)
        {
            MyList NewItem = new MyList();

            NewItem.someInt = n;
            NewItem.someString = str;

            someList.Add(NewItem);
            someList.Add(NewItem);
            someList.Add(NewItem);

        }

        public void ListDelete()
        {
            someList.RemoveAt(0);             
        }




        public void Fibonacci(int n)
        {
            try
            {
               // Thread.Sleep(Timeout.Infinite);
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
               
                        int a = 0;
                    int b = 1;
                    int tmp;

                    for (int i = 0; i < n; i++)
                    {
                        tmp = a;
                        a = b;
                        b += tmp;
                    }
                        
                        Thread.Sleep(interval);
                        TextBox.Text = a.ToString();
                   
                }));
            }

            catch (ThreadInterruptedException e)
            {
                        Trace.WriteLine("Thread cannot go to sleep - " +
                            "interrupted by main thread.");
            }
            
        }   
    }

}

