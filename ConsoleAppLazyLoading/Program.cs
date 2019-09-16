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
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            int DA = 244;
            int V = 212;
            int ID = 255;
            Console.WriteAscii("Hello World!", Color.FromArgb(DA, V, ID));


            //FileStream f = File.Open(@"C:/Sample-SQL-File-1000000-Rows.sql", FileMode.Open);
            //byte[] ar = new byte[100000000];
            //f.Read(ar, 0, (int)f.Length);

            BigFileReader reader = new BigFileReader(@"C:\Users\NWork\source\repos\ConsoleAppLazyLoading\Sample-SQL-File-1000000-Rows.sql", 50);
            //reader.Run();

            // test
            //new TestThreads().Run();

            Console.ReadLine();
        }
    }
}
