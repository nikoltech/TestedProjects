using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LockSlim
{
    /// <summary>
    /// class Slim is sync context
    /// </summary>
    public class Slim
    {
        private ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        private List<int> _items = new List<int>();
        private Random _rand = new Random();

        public void StartThreads()
        {
            new Thread(new ParameterizedThreadStart(Read)).Start(1);
            new Thread(new ParameterizedThreadStart(Read)).Start(2);
            new Thread(new ParameterizedThreadStart(Read)).Start(3);
            new Thread(new ParameterizedThreadStart(Write)).Start(new WriteObject { id = 4, threadId = "A" });
            new Thread(new ParameterizedThreadStart(Write)).Start(new WriteObject { id = 5, threadId = "B" });
        }

        private void Read(object idObj)
        {
            int id = (int)idObj;

            while (true)
            {
                this._rw.EnterReadLock();
                foreach (int i in this._items)
                {
                    Thread.Sleep(10);
                    Console.WriteLine($"Read #{id} -  {i}");
                }
                this._rw.ExitReadLock();
            }
        }

        private void Write(object obj)
        {
            int id = ((WriteObject)obj).id;
            object threadId = ((WriteObject)obj).threadId;

            while (true)
            {
                int number = this.GetRandNum(100);
                this._rw.EnterWriteLock();
                this._items.Add(number);
                this._rw.ExitWriteLock();
                Console.WriteLine($"Write #{id} - Thread {threadId} added {number}");
                Thread.Sleep(100);
            }
        }

        private class WriteObject
        {
            public int id;
            public object threadId;
        }

        private int GetRandNum(int max)
        {
            lock (this._rand)
            {
                return this._rand.Next(max);
            }
        }
    }
}
