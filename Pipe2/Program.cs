using System;
using System.IO.Pipes;

namespace Pipe2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello client Pipe2!");

            using (var s = new NamedPipeClientStream("pipestream"))
            {
                s.Connect();
                Console.WriteLine(s.ReadByte());
                s.WriteByte(200);

            }

            Console.ReadKey();
        }
    }
}
