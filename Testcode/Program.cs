﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using System.Runtime.ConstrainedExecution;

namespace Testcode
{
    interface IWrap
    {
        int wrap { get; set; }
    }

    public struct Coords : IWrap
    {
        public int x, y;

        public Coords(int p1, int p2)
        {
            x = p1;
            y = p2;
            wrap = 2;
        }

        public int wrap { get; set; }
    }
    class Wrap : CriticalFinalizerObject, IWrap, IDisposable
    {
        private static int init = 0;
        private bool _disposed;

        public int Value
        {
            get { return ++init; }
        }

        public int wrap { get; set; } = 1;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed) return;

            if (disposing)
            {
                // Освобождаем только управляемые ресурсы
                this._disposed = true;
            }

            // Освобождаем неуправляемые ресурсы

            this._disposed = disposing && true;
        }

        // Optional
        ~Wrap()
        {
            Dispose(false /*not called by user directly*/);
        }
    }

    /*class Program
    {
        //static void Main(string[] args)
        //{
        //    Wrap wrap = new Wrap();
        //    Coords coords = new Coords(1,1);

        //    Console.WriteLine(GetWrapResult(wrap));
        //    Console.WriteLine(wrap.wrap);

        //    Console.WriteLine();

        //    Console.WriteLine(GetWrapResult(coords));
        //    Console.WriteLine(coords.wrap);

        //    string s1 = String.Copy("foo");
        //    string s2 = s1;
        //    string s3 = String.Copy("foo");
        //    string s4 = String.Copy("bar");
        //    Console.WriteLine(s1 == s3);
        //    Console.WriteLine(s1 == s4);
        //    Console.WriteLine((Object)s1 == (Object)s3);
        //    Console.WriteLine((Object)s1 == (Object)s2);

        //    Console.ReadKey();
        //}

        static int GetWrapResult(IWrap wrap)
        {
            wrap.wrap = 10;
            return wrap.wrap;
        }


        class MyCustomException : DivideByZeroException
        {

        }

        class A1
        {
            public virtual void Foo()
            {
                Console.Write("Class A");
            }
        }
        class B : A1
        {
            public override void Foo()
            {
                Console.Write("Class B");
            }
        }

        //[Flags]
        enum Operation
        {
            None,
            Addd,
            Subtract,
            Multiply,
            Divide,
            Some
            //None = 0,
            //Addd = 1,
            //Subtract = 2,
            //Multiply = 4,
            //Divide = 8,
            //Some = 16
        }

        void Data(Operation op)
        {
            switch (op)
            {
                case Operation.Subtract:
                case Operation.Multiply:
                case Operation.Divide:
                    break;

                default: throw new ArgumentException("Invalid enum value");
            }
            //...
        }
        public struct S : IDisposable
        {
            private bool dispose;
            public void Dispose()
            {
                dispose = true;
            }
            public bool GetDispose()
            {
                return dispose;
            }
        }

        [Flags]
        enum FileAttributes
        {
            None = 0,
            Cached = 1,
            Current = 2,
            Obsolete = 4,
        }

        static void Main(string[] args)
        {
            // Create new enum instance with flags.
            Console.WriteLine("SET CACHED AND CURRENT FLAGS");
            var attributes = FileAttributes.Cached | FileAttributes.Current | FileAttributes.Obsolete | FileAttributes.None;

            // See if current flag is set.
            if ((attributes & FileAttributes.Current) == FileAttributes.Current)
            {
                Console.WriteLine("File is current");
            }

            // See if obsolete flag is not set.
            if ((attributes & FileAttributes.Obsolete) != FileAttributes.Obsolete)
            {
                Console.WriteLine("File is not obsolete");
            }

            // Remove current flag.
            Console.WriteLine("REMOVE CURRENT FLAG");
            attributes &= ~FileAttributes.Current;

            // See if current flag is set again.
            if ((attributes & FileAttributes.Current) != FileAttributes.Current)
            {
                Console.WriteLine("File is not current");
            }

            //------------------------------------------------------------------


            Operation op = Operation.Multiply;
            var ops = Operation.Addd | Operation.Divide | Operation.Multiply | Operation.Some | Operation.None;
            ops &= ~Operation.Addd; // remove

            if ((op & ops) == op)
                Console.WriteLine("Operation GRANTED");
            else
                Console.WriteLine("Operation DENIED");

            
            Console.WriteLine(ops.HasFlag(op));

            List<Action> actions = new List<Action>();
            for (var count = 0; count < 10; count++)
            {
                actions.Add(() => Console.WriteLine(count));
            }
            foreach (var action in actions)
            {
                action();
            }

            var s = new S();
            using (s)
            {
                Console.WriteLine(s.GetDispose());
            }
            Console.WriteLine(s.GetDispose());
            B obj2 = new B();
            obj2.Foo();

            A1 obj3 = new B();
            obj3.Foo();

            //try
            //{
            //    Calc();
            //}
            //catch (MyCustomException e)
            //{
            //    Console.WriteLine("Catch MyCustomException");
            //    throw;
            //}
            //catch (DivideByZeroException e)
            //{
            //    Console.WriteLine("Catch Exception");
            //}
            A f = new A();
            f.DoWork += (object sender, EventArgs e) => { Console.WriteLine("first dowork"); };
            //f.DoWork(f, EventArgs.Empty);
            f.OnDoWork();

            UserInfo userInfo = new UserInfo("name", "family", "shortname");
            userInfo.WriteUserInfo();

            kk kk = new kk();
            kk.TypeName();

            caca caca = new caca();
            shit shit = new shit();

            fac ff = caca as fac;
            shit f2 = ff as shit;

            
            if (ff == null) Console.WriteLine("ff NULL");

            Console.WriteLine(ff is caca);
            Console.WriteLine(ff is shit);

            Console.WriteLine("\t\t");
            Console.WriteLine("good");
            Console.ReadLine();
        }

        interface fac
        { }

        class caca  { }

        class shit  { }

        public class A
        {
            public void OnDoWork()
            {
                if (DoWork != null)
                    DoWork(this, EventArgs.Empty);
            }

            public event EventHandler DoWork;
        }

        private static void Calc()
        {
            int result = 0;
            var x = 5;
            int y = 0;
            try
            {
                result = x / y;
            }
            catch (MyCustomException e)
            {
                Console.WriteLine("Catch DivideByZeroException");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch Exception");
            }
            finally
            {
                throw new MyCustomException();
            }
        }

        // same methods in interfaces
        public interface IName
        {
            public void WriteName();
        }

        public interface INameFamily
        {
            // Объявляем в данном интерфейсе такой же метод
            public void WriteName();
            void WriteFamily();
        }

        public interface IUserInfo : INameFamily
        {
            // Обязательно нужно указать ключевое слово new
            // чтобы не скрывались методы базового интерфейса
            new void WriteName();
            void WriteUserInfo();
        }

        class kk : UserInfo, INameFamily, IName
        {
            public kk() : base("", "", "") { }

            public void WriteFamily()
            {
                throw new NotImplementedException();
            }

            void INameFamily.WriteName()
            {
                Console.WriteLine("INameFamily");
            }

            void IName.WriteName()
            {
                Console.WriteLine("IName");
            }

            public void TypeName()
            {
                INameFamily fam = this;
                fam.WriteName();
                IName nam = this;
                nam.WriteName();
            }
        }

        // Класс, реализующий два интерфейса
        class UserInfo : IUserInfo, IName
        {
            string ShortName, Family, Name;

            public UserInfo(string Name, string Family, string ShortName)
            {
                this.Name = Name;
                this.Family = Family;
                this.ShortName = ShortName;
            }

            // Используем явную реализацию интерфейсов
            // для исключения неоднозначности
             void IName.WriteName()
            {
                Console.WriteLine("Короткое имя: " + ShortName);
            }

            void INameFamily.WriteFamily()
            {
                Console.WriteLine("Фамилия: " + Family);
            }

            void INameFamily.WriteName()
            {
                Console.WriteLine("Полное имя: " + Name);
            }

            void IUserInfo.WriteName() { }

            public void WriteUserInfo()
            {
                UserInfo obj = new UserInfo(Name, Family, ShortName);
                // Для использования закрытых методов необходимо
                // создать интерфейсную ссылку
                IName link1 = (IName)obj;
                link1.WriteName();
                INameFamily link2 = (INameFamily)obj;
                link2.WriteName();
                link2.WriteFamily();
                IUserInfo link3 = (IUserInfo)obj;
                link3.WriteName();
            }
        }
    
        
    }
    */
    class Root
    {
        public volatile static Root StaticRoot = null;
        public Nested Nested = null;

        ~Root()
        {
            Console.WriteLine("Finalization of Root");
            StaticRoot = this;
        }
    }
    class Nested
    {
        public void DoSomeWork(string threadLocation)
        {
            Console.WriteLine(String.Format(
                $"Thread {0} enters DoSomeWork threadLocation-{threadLocation}",
                Thread.CurrentThread.ManagedThreadId));
            Thread.Sleep(2000);
            Console.WriteLine(String.Format(
                $"Thread {0} leaves DoSomeWork threadLocation-{threadLocation}",
                Thread.CurrentThread.ManagedThreadId));
        }
        ~Nested()
        {
            Console.WriteLine("Finalization of Nested");
            DoSomeWork("Finalize");
        }

    }

    class Program
    {

        static void CreateObjects()
        {
            Nested nested = new Nested();
            Root root = new Root();
            root.Nested = nested;
        }

        static string write1()
        {
            Console.WriteLine($"SSwrite1");
            return "write2";
        }

        static string write2()
        {
            Console.WriteLine($"SSwrite2");
            return "write2";
        }

        delegate void Operation();

        static void Main(string[] args)
        {
            int count = 0;
            do
            {
                Root.StaticRoot = null;
                Console.Clear();
                Console.WriteLine($"count - {++count}\n\n");

                Func<int> d;
                d = () => 0;
                d += () => 1;
                d += () => 2;
                Console.WriteLine(d());

                Func<string> f = write1;
                f += write2;
                Console.WriteLine(f());

                Console.WriteLine();
                CreateObjects();
                GC.Collect();
                while (Root.StaticRoot == null) { }
                Root.StaticRoot.Nested.DoSomeWork("Main");
            }
            while (ConsoleKey.Escape != Console.ReadKey(false).Key);

            //int[] numbers = { -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6 };

            //int n = numbers.Length; // длина массива
            //int k = n / 2;          // середина массива
            //int temp;               // вспомогательный элемент для обмена значениями
            //for (int i = 0; i < k; i++)
            //{
            //    temp = numbers[i];
            //    numbers[i] = numbers[n - i - 1];
            //    numbers[n - i - 1] = temp;
            //}
            //foreach (int i in numbers)
            //{
            //    Console.Write($"{i} \t");
            //}
        }
    }
}
