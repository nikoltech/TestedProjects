using System;

namespace LabsHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            //int t = -8;
            //double a = Math.Sqrt(-8);
            //double a2 = Math.Pow(8, 1/3);

            //Console.WriteLine(a);
            //Console.WriteLine(a2);

            //Console.ReadKey(false);


            //return;
            int lbNum = 0;
            while (true)
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("Enter lab num: ");
                    lbNum = 0;
                    lbNum = Int32.Parse(Console.ReadLine());

                    switch (lbNum)
                    {
                        case 1:
                        case 2:
                            { Program.Lab1(); break; }

                        default: { Console.WriteLine("Enter another lab number!"); break; }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    Console.WriteLine();
                    Console.WriteLine("Enter any key for continue...");
                    Console.ReadKey(false);
                    Console.Clear();
                }
            }
            
        }

        /// <summary>
        /// Computing number of task from the list
        /// </summary>
        static void Lab1()
        {
            int numStudent = 0, numTask = 0;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("numStudent");
                numStudent = Int32.Parse(Console.ReadLine());
                Console.WriteLine("numTask");
                numTask = Int32.Parse(Console.ReadLine());

                Console.WriteLine("numStudent");

                double result = 1 + ((Math.Pow(numStudent, 2) * numTask + (3 * numStudent)) % 46);

                Console.WriteLine($"Result {result}");
                ConsoleKeyInfo key = Console.ReadKey(false);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        static void Lab3()
        {
            Lab1();
        }
    }
}
