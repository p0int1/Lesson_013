using System;
using System.Threading;

//Задание 2
//Используя Visual Studio, создайте проект по шаблону Console Application.  
//Расширьте  задание  2, так, чтобы  в  одном  столбце  одновременно  могло  быть  две  цепочки  символов. 
//Смотрите ниже снимок экрана, представленный как образец.  

namespace Task_2
{
    class Program
    {
        static object block = new object();
        static Random random = new Random();
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Clear();
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;


            void PrintLine(object arr)
            {
                int[] param = (int[])arr;
                while (true)
                {
                    int lenght = random.Next(3, 10); // длинна столбца
                    for (int pos = 0; pos < height; pos++) // цикл перемещения от верха окна к низу
                    {     // pos - позиция первого эл в цепочке
                        for (int s = 0; s < lenght; s++) // цикл печати столбца одной позиции
                        {
                            if (pos - s == -1)
                                break;

                            lock (block) // блокировка консоли
                            {
                                switch (s)
                                {   // выбор цвета первого, второго и остальных символов
                                    case 0:
                                        Console.ForegroundColor = ConsoleColor.White;
                                        break;
                                    case 1:
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        break;
                                }

                                Console.SetCursorPosition(param[0], pos - s);
                                Console.Write((char)random.Next(65, 91)); // печать случайного символа в диапазоне
                                
                                if (s == lenght - 1)
                                {
                                    Console.SetCursorPosition(param[0], pos - lenght + 1);
                                    Console.Write(" ");
                                }
                            }
                        }

                        if ((pos > (height / 2)) && (Thread.CurrentThread.Name == null))
                        { // запуск второго потока в солбце в момент доползания первого до середины высоты окна
                            Thread second = new Thread(PrintLine);
                            Thread.CurrentThread.Name = "first";
                            second.Name = "second";
                            second.Start(arr);
                        }

                        Thread.Sleep(param[1]); // пауза между сползаниями
                    }
                    
                    lock (block)
                    { // затирание столбца символов в момент когда дошел до низа
                        for (int z = 0; z < lenght; z++)
                        {
                            Console.SetCursorPosition(param[0], height - z - 1);
                            Console.Write(" ");
                        }
                    }
                }
            }

            int x = 0;
            while (true) // запуск потоков, которые поместятся в ширину окна консоли
            {
                int[] arr = { x, random.Next(300, 900) };
                new Thread(PrintLine).Start(arr);
                x += random.Next(1, 7);
                if (x >= width - 1)
                    break;
//                break;
            }
        }
    }
}
