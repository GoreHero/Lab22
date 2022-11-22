using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Task1
{
    //Сформировать массив случайных целых чисел (размер  задается пользователем).
    //Вычислить сумму чисел массива и максимальное число в массиве.
    //Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Размер массива:");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func < Task <int[]>,int> func2 = new Func<Task<int[]>,int>(CalcTotal);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Action<Task<int>> action2 = new Action<Task<int>>(PrintTotal);
            Task task3 = task2.ContinueWith(action2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(FindMaxValue);
            Task<int> task4 = task1.ContinueWith<int>(func3);

            Action<Task<int>> action3 = new Action<Task<int>>(PrintMax);
            Task task5 = task4.ContinueWith(action3);

            //Action<Task<int[]>> action = new Action<Task<int[]>>(PrintArray);
            //Task task6 = task1.ContinueWith(action);






            task1.Start();
            Console.WriteLine();
            
            Console.ReadKey();

        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 10);
            }
            return array;
        }
        static int CalcTotal(Task<int[]> task)
        {
            int[] array = task.Result;
            int total = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                total += array[i];
            }
            return total;
        }
        static int FindMaxValue(Task<int[]> task)
        {
            int[] array = task.Result;
            int maxValue = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i] > maxValue)
                    maxValue = array[i];
            }
            return maxValue;
        }
        static void PrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
                Console.Write($"{array[i]} ");
        }
        static void PrintTotal(Task<int> task)
        {
            int total = task.Result;
            Console.WriteLine($"TOTAL = {total}");
        }
        static void PrintMax(Task<int> task)
        {
            int max = task.Result;
            Console.WriteLine($"MAX = {max}");
        }


    }
}
