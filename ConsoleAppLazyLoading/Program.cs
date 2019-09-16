namespace ConsoleAppLazyLoading
{
    using System;
    using System.Drawing;
    using Console = Colorful.Console;

    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            int DA = 244;
            int V = 212;
            int ID = 255;
            Console.WriteAscii("Hello World!", Color.FromArgb(DA, V, ID));

            //FileStream f = File.Open(@"C:/Sample-SQL-File-1000000-Rows.sql", FileMode.Open);
            //byte[] ar = new byte[100000000];
            //f.Read(ar, 0, (int)f.Length);

            try
            {
                //BigFileReader reader = new BigFileReader(@"C:\Users\NWork\source\repos\ConsoleAppLazyLoading\Sample-SQL-File-1000000-Rows.sql", 50);
                //reader.Run();

                MultThreadFileReader reader = new MultThreadFileReader(@"C:\Users\NWork\source\repos\ConsoleAppLazyLoading\Sample-SQL-File-1000000-Rows.sql");
                await reader.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Color.OrangeRed);
            }

            // test
            // new TestThreads().Run();

            Console.ReadLine();
        }
    }
}
