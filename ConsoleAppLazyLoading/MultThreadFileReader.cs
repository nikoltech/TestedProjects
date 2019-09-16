namespace ConsoleAppLazyLoading
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Console = Colorful.Console;

    public class MultThreadFileReader
    {
        private readonly string pathToFile;
        private readonly int count;

        public MultThreadFileReader(string path, int linesCount = 50)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not exists!");
            }
            pathToFile = path;
            count = linesCount;
        }


        // TODO: Rewrite with task requirements
        public async Task Run()
        {
            Console.WriteLine("MultThreadFileReader", Color.Orange);
            Console.WriteLine("Start...", Color.LightGreen);

            int processorNumber = 1;
            string processorsStr = System.Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS");
            if (processorsStr != null)
            {
                try
                {
                    processorNumber = int.Parse(processorsStr);
                }
                catch
                {
                    Console.WriteLine("Error find NUMBER_OF_PROCESSORS! Will use one thread to perform.", Color.OrangeRed);
                }
            }

            Console.WriteLine($"NUMBER_OF_PROCESSORS {processorNumber}", Color.Cyan);

            List<Thread> threads = new List<Thread>();
            for (int i = 1; i < processorNumber; i++)
            {
                // Thread.Sleep(800);
                Thread t = new Thread(new ParameterizedThreadStart(PerformThread));
                t.IsBackground = true;

                MultThreadFileReader.SetThreadAffinityMask((IntPtr)t.ManagedThreadId, (UIntPtr)MultThreadFileReader.GetCurrentProcessorNumber());

                threads.Add(t);
            }

            int num = 0;
            object threadContext = new object();
            threads.ForEach(t => t.Start(new ThreadParameters { ThreadNumber = ++num, ThreadLockContext = threadContext }));
            
        }

        

        #region WinApi
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcessorNumber();

        /// <summary>
        /// Thread on one processors core. Does it work?
        /// </summary>
        /// <param name="hThread"></param>
        /// <param name="dwThreadAffinityMask"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern UIntPtr SetThreadAffinityMask(IntPtr hThread, UIntPtr dwThreadAffinityMask);
        #endregion
        #region private methods, classes
        // TODO: Rewrite with task requirements
        private void PerformThread(object testThreadParametersClass)
        {
            if (!(testThreadParametersClass is ThreadParameters))
            {
                throw new ArgumentException("Wrong parameters! Need ThreadParameters class.");
            }
            ThreadParameters parameters = (ThreadParameters)testThreadParametersClass;

            int k = parameters.ThreadNumber;

            lock (parameters.ThreadLockContext)
            {
                Console.WriteLine("-----------");
                Console.Write($"Thread {k}  Core ", Color.GreenYellow);
                Console.WriteLine(TestThreads.GetCurrentProcessorNumber().ToString(), Color.OrangeRed);
            }
        }

        private class ThreadParameters
        {
            public int ThreadNumber { get; set; }
            public object ThreadLockContext { get; set; }
        }
        #endregion
    }
}
