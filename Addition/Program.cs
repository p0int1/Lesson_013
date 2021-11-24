using System;
using System.Threading;

//Используя Visual Studio, создайте проект по шаблону Console Application.  
//Напишите программу, в которой метод будет вызываться рекурсивно.  
//Каждый новый вызов метода выполняется в отдельном потоке.  

namespace Addition
{
    class Program
    {
        static int inside;
        static public void RecMethod()
        {
            Console.WriteLine($"Поток: {Thread.CurrentThread.Name} ");
            Thread.Sleep(3000);
            Thread thread = new Thread(RecMethod);
            inside++;
            thread.Name = "thread #" + inside;
            thread.Start();
        }

        static void Main(string[] args)
        {
            Thread thread = new Thread(RecMethod) { Name = "thread #" + inside };
            thread.Start();
        }
    }
}
