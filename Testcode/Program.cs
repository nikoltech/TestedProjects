using System;
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
        */
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
    
        [Flags]
        enum Operation
        {
            //None,
            //Addd,
            //Subtract,
            //Multiply,
            //Divide,
            //Some
            None = 0,
            Addd = 1,
            Subtract = 2,
            Multiply = 4,
            Divide = 8,
            Some = 16
        }
    /*
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

    public interface ISomeInterface
    {
        int TestProperty
        {
            // No access modifier allowed here
            // because this is an interface.
            get;
        }
    }

    public class TestClass : ISomeInterface
    {
        public int TestProperty
        {
            // Cannot use access modifier here because
            // this is an interface implementation.
            get { return 10; }

            // Interface property does not have set accessor,
            // so access modifier is allowed.
            protected set { }
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
        interface dd
        {
            void rr();
            void rf();
        }
        interface ddf : dd
        {
            new void rr();
            void rf();
        }
        class a : dd, ddf 
        { 
            int f;

             void ddf.rf()
            {
                Console.WriteLine("ddf");
            }

             void dd.rf()
            {
                Console.WriteLine("dd");
            }
            public void rr()
            {
                Console.WriteLine("rr");
            }
        }

        class b : a { string d; }

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

        delegate void OperationD();
        class Person
        { }

        class Student : Person
        { }

        #region Invariant/Covariance/Contrvariance
        /// <summary>
        /// Invariant
        /// </summary>
        /// <typeparam name="T"></typeparam>
        //interface IC<T>
        //    where T : CT
        //{
        //    public T x { get; }
        //}

        /// <summary>
        /// Contrvariance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        interface IC<in T>
            where T : CT, new()
        {
            public CT x { get; set; }
            public string GetInfo();
        }
        struct f
        {
            string fd;
        }
        // Covariance
        //interface IC<out T>
        //    where T : CT
        //{
        //    public T x { get; }
        //}

        class C<T> : IC<T>
            where T : CT, new()
        {
            public CT x { get; set; } = new T();
            public static string xd = "gg";
            public string GetInfo()
            {
                var t = xd;
                return this.GetInfo();
            }
        }
        class CT 
        {
            public string GetInfo()
            {
                return "CT";
            }
        }
        class CT2 : CT 
        {
            public string GetStyle()
            {
                return "CT2";
            }
        }
        class CT3 { }
        #endregion

        #region delegate Contrvariance
        class D { }
        class D2 : D { }
        class D3 : D2 
        {
            public string GetInfo()
            {
                return "D3";
            }
        }
        class D4 : D3 
        {
            public string GetStyle()
            {
                return "D4";
            }
        }
        class D5 : D4 { }

        delegate string GetInfo<in T>(T item);
        #endregion
        
        static IEnumerable<char> tt()
        {
            yield return 'A';
            yield return 'A';
            yield return 'A';
        }

        static void ModifyString(string s)
        {
            s = "Hello, I've been modified.";
        }

        static void ModifyClass(ModifyModifieble s)
        {
            s.ModifiableValue = "666";
        }

        class ModifyModifieble
        {
            public string ModifiableValue;
        }


        internal class AG
        {
            public void SomeMethod()
            {
                Console.WriteLine("A.SomeMethod");
            }
        }

        internal class BG : AG
        {
            new void SomeMethod()
            {
                Console.WriteLine("B.SomeMethod");
            }
        }
        internal class CG : BG
        {
            int hh;
            public void CallSomeMethod()
            {
                SomeMethod();
            }
            class fff
            {
                int jj;
                void gf(CG cG)
                {
                    cG.hh = 8;
                }
            }
        }

        interface IList
        {
             int Count { get; set; }
        }

        interface ICounter
        {
            void Count(int i);
        }

        interface IListCounter : IList, ICounter { }

        class C
        {
            void Count(int i) { }
            void t(int i)
            {

            }
            void Test(IListCounter x)      // 1 
            {
                x.Count(1);                // 2 
                ((IList)x).Count = 1;             // 3 
            }
        }

        internal class AH
        {
            private int i = initA();
            public static int initA() { return 2; }

            public AH()
            { print(); }

            public virtual void print()
            { Console.Write(i + "A "); }
        }
        internal class BH : AH
        {
            private int i = initB();
            public static int initB() { return 8; }

            public override void print()
            { Console.Write(i + "B "); }
        }

        static int AverageInt(int a, int b, int c)
        {
            if (a > b)
            {
                if (b > c)
                {
                    return b;
                }
                else if (a > c)
                {
                    return c;
                }
                else return a;
            }
            else
            {
                if (a > c)
                {
                    return a;
                }
                else if (b > c)
                {
                    return c;
                }
                else return b;
            }
        }

        static int AverageInt3(int a, int b, int c)
        {
            return a > b ?
                b > c ? b : a > c ? c : a
                : a > c ? a : b > c ? c : b;
        }

        static int AverageInt2(int a, int b, int c)
        {
            return (a > b && a < c) || (a > c && a < b) ? a : ((b > a && b < c) || (b > c && b < a) ? b : c);
        }
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Enter integer a b c:\n\n");

                Int32.TryParse(Console.ReadLine(), out int a);
                Int32.TryParse(Console.ReadLine(), out int b);
                Int32.TryParse(Console.ReadLine(), out int c);

                Console.WriteLine($"Average3 int({a},{b},{c}): {AverageInt3(a, b, c)}");

                Console.WriteLine("\n\nPress ANY for continue or ESCAPE for exit...");
            } while (ConsoleKey.Escape != Console.ReadKey(false).Key);
            int count = 0;/*
            do
            {
                Root.StaticRoot = null;
                Console.Clear();
                Console.WriteLine($"count - {++count}\n\n");

                TestClass testClass = new TestClass();
                Console.WriteLine(testClass.TestProperty);

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"i={i}");

                }
                byte r = 1;
                a af = new a();
                ((dd)af).rf();
                ((ddf)af).rf();
                ((dd)af).rr();
                ((ddf)af).rr();

                Console.WriteLine($"Average int(1, 2, 3): {AverageInt(1, 2, 3)}");
                Console.WriteLine($"Average int(1, 3, 2): {AverageInt(1, 3, 2)}");
                Console.WriteLine($"Average int(3, 2, 1): {AverageInt(3, 2, 1)}");
                Console.WriteLine($"Average int(3, 1, 2): {AverageInt(3, 1, 2)}");
                Console.WriteLine($"Average int(2, 1, 3): {AverageInt(2, 1, 3)}");
                Console.WriteLine($"Average int(2, 3, 1): {AverageInt(2, 3, 1)}");
                Console.WriteLine($"Average int(2, 2, 1): {AverageInt(2, 2, 1)}");
                Console.WriteLine($"Average int(2, 2, 3): {AverageInt(2, 2, 3)}");
                Console.WriteLine($"Average2 int(2, 2, 3): {AverageInt2(2, 2, 3)}");
                Console.WriteLine($"Average3 int(2, 2, 3): {AverageInt3(2, 2, 3)}");

                #region Invariant/Covariance/Contrvariance code
                //IC<CT> c = new C<CT2>();
                CT cT = new CT();
                IC<CT2> iCT2 = new C<CT>();
                Console.WriteLine($"Invariant/Covariance/Contrvariance result -- {iCT2.x.GetInfo()}");
                #endregion
                
                #region delegate Contrvariance code
                GetInfo<D4> getInfo = (item) => item.GetStyle();
                GetInfo<D3> getInfo3 = (item) => item.GetInfo();
                D4 d4 = new D4();
                D3 d3 = new D3();
                D5 d5 = new D5();

                getInfo = getInfo3;
                string delegateInvokationResult = getInfo == null ? "delegate is NULL" : getInfo(d4);
                Console.WriteLine($"delegate Contrvariance result -- {delegateInvokationResult}");
                #endregion

                #region ModifyNotModified
                string s = "Hello, world";
                ModifyString(s); // Inside the method we create an new object and try change existing incoming reference to new. For this feature is need ref keyword!
                Console.WriteLine("s = " + s);

                ModifyModifieble modifieble = new ModifyModifieble();
                modifieble.ModifiableValue = "5555";
                Console.WriteLine("ModifyModifieble = " + modifieble.ModifiableValue);
                ModifyClass(modifieble);
                Console.WriteLine("ModifyModifieble = " + modifieble.ModifiableValue);
                ModifyString(modifieble.ModifiableValue);
                Console.WriteLine("ModifyModifieble = " + modifieble.ModifiableValue);
                #endregion

                new CG().CallSomeMethod();
                AH a = new BH();
                a.print();
                Console.WriteLine();

                Operation op = Operation.Divide | Operation.Addd;
                Console.WriteLine($"op {op}");

                string @string = "ABC";
                Console.WriteLine(@string);

                #if DEBUG
                Console.WriteLine("DEBUG");
#else
                Console.WriteLine("ELSE");  
#endif

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
            */
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
