using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var work1 = new Task(() =>
              {
                  doWork(1,2000);
              });

            
            work1.Start();
            var work2=Task.Factory.StartNew(() => {
                doWork(2,1000);
            }).ContinueWith(t => {
                doWork(3,1500);
                });

            List<Task> tasks = new List<Task>();
            tasks.Add(work1);
            tasks.Add(work2);

            Task.WaitAll(tasks.ToArray());
            start();

            Console.WriteLine("end");
            Console.ReadKey();

        }



        static async void start()
        {
            var s=await doWork();
        }

        static Task<string> doWork()
        {
            return Task.Factory.StartNew(() => doWork(4,500));
        }

        static string doWork(int id,int time)
        {
            Console.WriteLine("message from task {0}.(start)", id);
            Thread.Sleep(time);
            Console.WriteLine("message from task {0}.(end)",id);
            return "hello";
        }
    }
}
