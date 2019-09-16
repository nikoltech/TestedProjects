using Colorful;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Console = Colorful.Console;

namespace ConsoleAppLazyLoading
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            int DA = 244;
            int V = 212;
            int ID = 255;
            Console.WriteAscii("Hello World!", Color.FromArgb(DA, V, ID));


            //FileStream f = File.Open(@"C:/Sample-SQL-File-1000000-Rows.sql", FileMode.Open);
            //byte[] ar = new byte[100000000];
            //f.Read(ar, 0, (int)f.Length);

            try
            {
                //BigFileReader reader = new BigFileReader(@"C:\Users\NWork\source\repos\ConsoleAppLazyLoading\Sample-SQL-File-1000000-Rows.sql", 50);
                //reader.Run();

                MultThreadFileReader reader = new MultThreadFileReader(@"C:\Users\NWork\source\repos\ConsoleAppLazyLoading\Sample-SQL-File-1000000-Rows.sql");
                reader.Run();

                Console.WriteLine("test line");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Color.OrangeRed);
            }

            // test
            // new TestThreads().Run();

            Console.ReadLine();
        }
    }
}
