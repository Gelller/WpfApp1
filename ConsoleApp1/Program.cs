﻿using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int nWorkerThreads;
            int nCompletionThreads;
            ThreadPool.SetMaxThreads(5, 5);
            ThreadPool.GetMaxThreads(out nWorkerThreads, out nCompletionThreads);
            Console.WriteLine("Максимальное количество потоков: " + nWorkerThreads
                + "\nПотоков ввода-вывода доступно: " + nCompletionThreads);
            for (int i = 0; i < 5; i++)
                ThreadPool.QueueUserWorkItem(JobForAThread);

             Thread.Sleep(3000);

            Console.ReadLine();
        }
        static void JobForAThread(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("количество рабочих элементов, обработанных на данный момент " + ThreadPool.CompletedWorkItemCount);
                Console.WriteLine("количество рабочих элементов, находящихся в настоящее время в очереди на обработку " + 
                    ThreadPool.PendingWorkItemCount);
                Console.WriteLine("цикл {0}, выполнение внутри потока из пула {1}",
                    i, Thread.CurrentThread.ManagedThreadId);
              
                Thread.Sleep(50);
            }
        }
    }

}
