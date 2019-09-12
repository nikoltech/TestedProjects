using System;

namespace LockSlim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Slim s = new Slim();
            s.StartThreads();

            Console.ReadKey();
        }
    }
}
