namespace ConsoleAppLazyLoading
{
    using Colorful;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using Console = Colorful.Console;

    public class BigFileReader
    {
        private readonly string pathToFile;
        private readonly int count;
        private readonly Queue<string> data = new Queue<string>();
        private readonly EventWaitHandle flagGo = new AutoResetEvent(false);
        private readonly EventWaitHandle flagReady = new AutoResetEvent(false);

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
            Thread t1 = new Thread(ProcessOnLines);
            Thread t2 = new Thread(ReadLinesFromFile);
            t1.IsBackground = true;
            t2.IsBackground = true;
            t1.Start();
            t2.Start();
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
