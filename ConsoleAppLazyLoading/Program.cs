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

            BigFileReader reader = new BigFileReader(@"C:/Sample-SQL-File-1000000-Rows.sql", 50);
            //reader.Run();

            // test
            int processors = 1;
            string processorsStr = System.Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS");
            if (processorsStr != null)
                processors = int.Parse(processorsStr);

            Console.WriteLine(processors, Color.Cyan);
            Console.WriteLine(Program.GetCurrentProcessorNumber(), Color.OrangeRed);

            for (int i = 0; i < processors; i++)
            {
                Thread.Sleep(800);
                Thread t = new Thread(() =>
                {
                    string k = i.ToString();
                    Console.WriteLine("-----------");
                    for (int j = 0; j < 10; j++)
                    {
                        Thread.Sleep(2000);
                        Console.WriteLine($"Thread {k}", Color.FromArgb(DA - 10, V - 10, ID - 10));
                    }
                });
                t.IsBackground = true;

                Program.SetThreadAffinityMask((IntPtr)t.ManagedThreadId, (UIntPtr)1);
                t.Start();
            }



            Console.ReadLine();
        }

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcessorNumber();

        [DllImport("kernel32.dll")]
        static extern UIntPtr SetThreadAffinityMask(IntPtr hThread, UIntPtr dwThreadAffinityMask);
    }



    class BigFileReader
    {
        private string pathToFile;
        private int count;
        private Queue<string> data = new Queue<string>();
        private EventWaitHandle flagGo = new AutoResetEvent(false);
        private EventWaitHandle flagReady = new AutoResetEvent(false);

        public BigFileReader(string path, int linesCount)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not exists!");
            }
            pathToFile = path;
            count = linesCount;
        }

        public void Run()
        {
            new Thread(ProcessOnLines).Start();
            new Thread(ReadLinesFromFile).Start();
        }
        void ReadLinesFromFile()
        {
            using (StreamReader sr = File.OpenText(pathToFile))
            {
                string line = "";
                flagGo.WaitOne();
                while ((line = sr.ReadLine()) != null)
                {
                    data.Enqueue(line);
                    if (data.Count == count)
                    {
                        flagReady.Set();
                        flagGo.WaitOne();
                    }
                }
            }
            data.Enqueue(null);
            flagReady.Set();
            flagGo.WaitOne();
        }
        void ProcessOnLines()
        {
            ColorAlternatorFactory alternatorFactory = new ColorAlternatorFactory();
            ColorAlternator alternator = alternatorFactory.GetAlternator(2, Color.Plum, Color.PaleVioletRed);

            while (true)
            {
                flagGo.Set();
                flagReady.WaitOne();
                Console.WriteLine("Processing Next Data Block!", Color.GreenYellow);
                while (data.Count != 0)
                {
                    string line = data.Dequeue();
                    if (line == null)
                    {
                        Console.WriteAscii("Data is Empty!", Color.Red);
                        flagGo.Dispose();
                        flagReady.Dispose();
                        return;
                    }

                    //Тут обрабатываем строку из данной группы
                    Console.WriteLineAlternating(line, alternator);
                    //Thread.Sleep(10);
                }
            }
        }
    }
}
