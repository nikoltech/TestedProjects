using System;
using System.IO.Pipes;

namespace Pipe1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello server Pipe1!");

            using (var s = new NamedPipeServerStream("pipestream"))
            {
                s.WaitForConnection();
                s.WriteByte(100);
                Console.WriteLine(s.ReadByte());
            }

            int[][] mass = new int[5][];

            Console.ReadKey();
        }
    }
}
