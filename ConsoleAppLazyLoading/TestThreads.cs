namespace ConsoleAppLazyLoading
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using Console = Colorful.Console;

    public class TestThreads
    {
        public void Run()
        {
            int processors = 1;
            string processorsStr = System.Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS");
            if (processorsStr != null)
            {
                processors = int.Parse(processorsStr);
            }

            Console.WriteLine($"NUMBER_OF_PROCESSORS {processors}", Color.Cyan);
            Console.WriteLine($"CurrentProcessorNumber {TestThreads.GetCurrentProcessorNumber()}", Color.OrangeRed);

            List<Thread> threads = new List<Thread>();
            for (int i = 1; i < processors; i++)
            {
                Thread.Sleep(800);
                Thread t = new Thread(new ParameterizedThreadStart(TestThread));
                t.IsBackground = true;

                TestThreads.SetThreadAffinityMask((IntPtr)t.ManagedThreadId, (UIntPtr)TestThreads.GetCurrentProcessorNumber());

                threads.Add(t);
            }

            int num = 0;
            object threadContext = new object();

            // colors value
            int DA = 244;
            int V = 212;
            int ID = 255;
            threads.ForEach(t => t.Start(new TestThreadParameters { ThreadNumber = ++num, ThreadLockContext = threadContext, DA = DA, ID = ID, V = V }));
        }

        public void TestThread(object testThreadParametersClass)
        {
            if (!(testThreadParametersClass is TestThreadParameters))
            {
                throw new ArgumentException("Wrong parameters! Need TestThreadParameters class.");
            }
            TestThreadParameters parameters = (TestThreadParameters)testThreadParametersClass;

            int k = parameters.ThreadNumber;
            /*for (int j = 0; j < 10; j++)
            {
                Thread.Sleep(2000);*/
                lock (parameters.ThreadLockContext)
                {
                    Console.WriteLine("-----------");
                    Console.Write($"Thread {k}  Core ", Color.FromArgb(parameters.DA - 10, parameters.V - 10, parameters.ID - 10));
                    Console.WriteLine(TestThreads.GetCurrentProcessorNumber().ToString(), Color.OrangeRed);
                }
            //}
        }

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

        #region private methods
        private class TestThreadParameters
        {
            public int ThreadNumber { get; set; }
            public object ThreadLockContext { get; set; }
            public int DA { get; set; }
            public int V { get; set; }
            public int ID { get; set; }
        }
        #endregion
    }
}
