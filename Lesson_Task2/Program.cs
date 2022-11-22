using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson_Task2
{
    internal class Program
    {
        static void Method1(CancellationToken token)
        {
            Console.WriteLine("Metod1 начал работу");
            for (int i=0; i<100; i++)
            {
                if (((CancellationToken)token).IsCancellationRequested)
                {
                    Console.WriteLine("Операция отменена");
                    return;
                }
                Console.WriteLine($"Method1 выводит счетчик = {i}");
                Thread.Sleep(2000);
            }
            Console.WriteLine("Method1 окончил работу");
        }
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            //Action<object> action = new Action<object>(Method1);
            Task task = new Task(()=>Method1(token));
            task.Start();

            Console.WriteLine("ВВедите У для отмены операции или другой символ для ее продолжения");
            string s = Console.ReadLine();
            if (s == "Y")
                cancellationTokenSource.Cancel();

            Console.ReadKey();

        }
    }
}
