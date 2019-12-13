using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace LabsHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Type program = typeof(Program);
            StringBuilder methodName = new StringBuilder();
            int lbNum = 0;

            //Lab4Cycle();

            while (true)
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("Enter lab num: ");
                    lbNum = 0;
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Bye bye!!!");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }
                    Int32.TryParse(key.KeyChar.ToString(), out lbNum);

                    methodName.Append($"Lab{lbNum}");
                    MethodInfo labMethod = program.GetMethod(methodName.ToString(), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    methodName.Clear();

                    if (labMethod == null)
                    {
                        Console.WriteLine("Enter another lab number!");
                        Console.ReadKey(false);
                    }
                    else
                    {
                        labMethod.Invoke(null, null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    Console.WriteLine("Enter any key for continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            
        }

        #region Labs algorithm
        /// <summary>
        /// Computing number of task from the list
        /// </summary>
        static void Lab1()
        {
            int numStudent = 0, taskCount = 5;
            while (true)
            {
                Console.Clear();
                numStudent = 0;

                Console.WriteLine("numStudent");
                Int32.TryParse(Console.ReadLine(), out numStudent);
                if (numStudent == 0)
                {
                    Console.WriteLine("Please enter another value!");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                }

                double[] result = new double[taskCount];
                for (int numTask = 1; numTask <= taskCount; numTask++)
                {
                    result[numTask - 1] = 1 + ((Math.Pow(numStudent, 2) * numTask + (3 * numStudent)) % 46);
                }

                Console.WriteLine("Result:");
                foreach (double numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        static void Lab2()
        {
            Lab1();
        }

        static void Lab3()
        {
            int numStudent = 0, taskCount = 2;
            while (true)
            {
                Console.Clear();
                numStudent = 0;

                Console.WriteLine("numStudent");
                Int32.TryParse(Console.ReadLine(), out numStudent);
                if (numStudent == 0)
                {
                    Console.WriteLine("Please enter another value!");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                }

                double[] result = new double[taskCount];
                for (int numTask = 1; numTask <= taskCount; numTask++)
                {
                    result[numTask - 1] = 1 + ((Math.Pow(numStudent, 2) * numTask + (7 * numStudent)) % 23);
                }

                Console.WriteLine("Result:");
                foreach (double numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        static void Lab4()
        {
            int numStudent = 0, taskCount = 10;
            while (true)
            {
                Console.Clear();
                numStudent = 0;

                Console.WriteLine("numStudent");
                Int32.TryParse(Console.ReadLine(), out numStudent);
                if (numStudent == 0)
                {
                    Console.WriteLine("Please enter another value!");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                }

                int[] result = new int[taskCount];
                for (int numTask = 1; numTask <= taskCount; numTask++)
                {
                    result[numTask - 1] = ((numStudent + (numTask - 1) * 11) % 112) + 56;
                }

                Console.WriteLine("Result:");
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Result (Sorted):");
                Array.Sort(result, Comparer.Default);
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        static void Lab5()
        {
            int numStudent = 0, taskCount = 8, addTaskCount = 3;
            while (true)
            {
                Console.Clear();
                numStudent = 0;

                Console.WriteLine("numStudent");
                bool success = Int32.TryParse(Console.ReadLine(), out numStudent);
                if (numStudent == 0)
                {
                    Console.WriteLine("Please enter another value!");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                }

                int[] result = new int[taskCount];
                for (int numTask = 1; numTask <= taskCount; numTask++)
                {
                    result[numTask - 1] = numStudent + (numTask - 1) * 35;
                }

                int[] addResult = new int[addTaskCount];
                for (int numTask = 1; numTask <= addTaskCount; numTask++)
                {
                    addResult[numTask - 1] = numStudent + (numTask - 1) * 35;
                }

                Console.WriteLine("Result:");
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Additional Result:");
                foreach (int numTask in addResult)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Result (Sorted):");
                Array.Sort(result, Comparer.Default);
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Additional Result (Sorted):");
                Array.Sort(addResult, Comparer.Default);
                foreach (int numTask in addResult)
                {
                    Console.Write($"{numTask}  ");
                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        static void Lab6()
        {
            int numStudent = 0, taskCount = 6, addTaskCount = 3;
            while (true)
            {
                Console.Clear();
                numStudent = 0;

                Console.WriteLine("numStudent");
                bool success = Int32.TryParse(Console.ReadLine(), out numStudent);
                if (numStudent == 0)
                {
                    Console.WriteLine("Please enter another value!");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                }

                int[] result = new int[taskCount];
                for (int numTask = 1; numTask <= taskCount; numTask++)
                {
                    result[numTask - 1] = numStudent + (numTask - 1) * 35;
                }

                int[] addResult = new int[addTaskCount];
                for (int numTask = 1; numTask <= addTaskCount; numTask++)
                {
                    addResult[numTask - 1] = (numStudent + (numTask - 1) * 35 ) % 23 + 211;
                }

                Console.WriteLine("Result:");
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Additional Result:");
                foreach (int numTask in addResult)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Result (Sorted):");
                Array.Sort(result, Comparer.Default);
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Additional Result (Sorted):");
                Array.Sort(addResult, Comparer.Default);
                foreach (int numTask in addResult)
                {
                    Console.Write($"{numTask}  ");
                }


                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        static void Lab7()
        {
            int numStudent = 0, taskCount = 5;
            while (true)
            {
                Console.Clear();
                numStudent = 0;

                Console.WriteLine("numStudent");
                bool success = Int32.TryParse(Console.ReadLine(), out numStudent);
                if (numStudent == 0)
                {
                    Console.WriteLine("Please enter another value!");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                }

                int[] result = new int[taskCount];
                for (int numTask = 1; numTask <= taskCount; numTask++)
                {
                    result[numTask - 1] = numStudent + (numTask - 1) * 35 + 902;
                }

                int addTasksVariantsCount = 1081 - 1078;
                int[] addResult = new int[addTasksVariantsCount + 1];
                for (int i = 0, numTask = 1078; numTask <= 1081; i++)
                {
                    addResult[i] = numTask++;
                }

                Console.WriteLine("Result:");
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Additional Result (two of ...):");
                foreach (int numTask in addResult)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Result (Sorted):");
                Array.Sort(result, Comparer.Default);
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Additional Result  (two of ..., Sorted):");
                Array.Sort(addResult, Comparer.Default);
                foreach (int numTask in addResult)
                {
                    Console.Write($"{numTask}  ");
                }


                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }
        #endregion

        #region Cycle Labs algorithm
        static void Lab4Cycle()
        {
            int numStudent = 0, taskCount = 10;
            for (numStudent = 1; numStudent <= 30; numStudent++)
            {
                Console.Clear();
                numStudent = 0;

                Console.WriteLine("numStudent");
                Int32.TryParse(Console.ReadLine(), out numStudent);
                if (numStudent == 0)
                {
                    Console.WriteLine("Please enter another value!");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    continue;
                }

                int[] result = new int[taskCount];
                for (int numTask = 1; numTask <= taskCount; numTask++)
                {
                    result[numTask - 1] = ((numStudent + (numTask - 1) * 11) % 112) + 56;
                }

                Console.WriteLine("Result:");
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Result (Sorted):");
                Array.Sort(result, Comparer.Default);
                foreach (int numTask in result)
                {
                    Console.Write($"{numTask}  ");
                }

                //ConsoleKeyInfo key = Console.ReadKey(true);

                //if (key.Key == ConsoleKey.Escape)
                //{
                //    return;
                //}
            }
        }
        #endregion
    }
}
