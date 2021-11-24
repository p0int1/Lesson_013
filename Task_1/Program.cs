using System;
using System.Threading;

//Задание 1
//Используя Visual Studio, создайте проект по шаблону Console Application.  
//Создайте  программу, которая  будет  выводить  на  экран  цепочки  падающих  символов.  Длина  каждой 
//цепочки  задается  случайно.  Первый  символ  цепочки  –  белый, второй  символ  –  светло-зеленый,
//остальные символы темно-зеленые. Во время падения цепочки, на каждом шаге, все символы меняют 
//свое значение. Дойдя до конца, цепочка исчезает и сверху формируется новая цепочка. Смотрите ниже 
//снимок экрана, представленный как образец.  

namespace Task_1
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

            void ClearColumn(int x) // функция отчистки всего столбца
            {
                for (int y = 0; y < height; y++)
                {
                    lock (block) // блокировка консоли
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(" ");
                    }
                }
            }

            void PrintLine(object x)
            {
                while(true)
                {
                    int lenght = random.Next(3, 10); // длинна столбца
                    int speed = random.Next(300, 900); // значение задержки слип
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

                                Console.SetCursorPosition((int)x, pos - s);
                                Console.Write((char)random.Next(65, 91)); // печать случайного символа в диапазоне

                            }
                        }

                        Thread.Sleep(speed); // пауза между сползаниями
                        ClearColumn((int)x); // отчистка столбца
                    }
                }
            }

            ParameterizedThreadStart writeSnake = new ParameterizedThreadStart(PrintLine);
            int x = 0;

            while(true) // запуск потоков, которые поместятся в ширину окна консоли
            {
                new Thread(writeSnake).Start(x);
                x += random.Next(1, 4);
                if (x >= width - 1)
                    break;
            }
        }
    }
}
