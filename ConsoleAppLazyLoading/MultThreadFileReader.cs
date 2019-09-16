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

            List<Task> tasks = new List<Task>();
            object threadContext = new object();
            for (int i = 0; i < processorNumber; i++)
            {
                int num = i;
                Task t = new Task(() =>
                {
                    this.PerformThread(new ThreadParameters { ThreadNumber = num, ThreadLockContext = threadContext });
                });
                
                // MultThreadFileReader.SetThreadAffinityMask((IntPtr)t.ManagedThreadId, (UIntPtr)MultThreadFileReader.GetCurrentProcessorNumber());
                tasks.Add(t);
            }

            tasks.ForEach(t => t.Start());
            await Task.WhenAll(tasks);
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
        #region private methods, classesы
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
                Console.Write($"Thread ", Color.GreenYellow);
                Console.Write(k, Color.Orange);
                Console.Write($" Core ", Color.GreenYellow);
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
