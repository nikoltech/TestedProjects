using Colorful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using System.Runtime.ConstrainedExecution;
using System.Collections;
using System.Diagnostics;
using Console = Colorful.Console;
using System.Drawing;
using System.Text.Json;
using System.Net.Http;
using System.Net;
using System.Reflection;
using NUnit.Framework;
using Testcode;
using System.Text;
using System.Web;

namespace Testcode
{
    #region others...
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

        void move(out int t) 
        {
            int tmp = this.Run();
            t = this.Success(tmp) ? tmp : 0;
        }

        int Run();
        bool Success(int run);
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
            protected set {  }
        }

        //public void move(out int f)
        //{
        //    f = 2 + 2;
        //}

        public int Run()
        {
            //this.move(out int t); // do not have an acces from interface
            return new Random().Next(0, 1);
        }

        public bool Success(int run)
        {
            return run > 0;
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

    public class fr
    {
        public virtual int F {get;}

        public fr()
        {
            Console.WriteLine("FFFFRRRRRRRRRRRRR");
        }
    }
    public partial class ft
    {
        partial void Foo();

    }
    public partial class ft : fr
    {
        static ft()
        {
            Console.WriteLine("FFFTTTTTTTTTT");

        }
        partial void Foo() { int i = 0; }
        public static void Run()
        {
            Console.WriteLine("Runned");
        }
    }
    public static class bugaga 
    {
        static bugaga() { }
        public static void i() { }
    }

    class Bas1
    {
        public string Do() => "Bas1";
    }
    class Bas2 : Bas1
    {
        public string Do() => "Bas2";

        public string Write()
        {
            return Do();
        }
    }

    #endregion
    class AtrTestAttribute : Attribute
    {
        public int FAttr { get; set; } = 15;
    }

    class InvokMeth
    {
        public string Str { get; set; }
        public InvokMeth(string str)
        {
            this.SetValueToStr(str);
        }

        public void SetValueToStr(string str)
        {
            this.Str = str;
        }
    }

    #region Assebly


    //public class Person
    //{
    //    public string Name;

    //    public int Age;

    //    public override string ToString()
    //    {
    //        return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
    //    }
    //}

    //public class CodeBuilder
    //{
    //    private readonly object _personBuilderData;

    //    public CodeBuilder(string person)
    //    {
    //        Type codeBuilder = typeof(CodeBuilder);
    //        Assembly codeAssembly = codeBuilder.Assembly;
    //        AssemblyName assName = codeAssembly.GetName();
    //        string assemblyName = assName.Name;

    //        this._personBuilderData = Activator.CreateInstance(Type.GetType($@"{assemblyName}.{person}") ?? throw new Exception("Something wrong"));
    //    }

    //    public CodeBuilder AddField<TEntityType>(string name, TEntityType type)
    //    {
    //        Type myType = _personBuilderData.GetType();
    //        FieldInfo[] myField = myType.GetFields();

    //        foreach (var item in myField)
    //        {
    //            if (item.Name == name)
    //            {
    //                name = item.Name;
    //            }
    //        }

    //        return this;
    //    }
    //}


    public class Person
    {
        public string Name;

        public string Type;

        public int Age;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }

    class Class
    {
        public string Name;
        public List<Person> listOfData = new List<Person>();

        public Class()
        {

        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {Name}").AppendLine("{");
            foreach (var f in listOfData)
                sb.AppendLine($"  {f};");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class CodeBuilder
    {
        private Class data = new Class();

        public CodeBuilder(string rootName)
        {
            data.Name = rootName;
        }

        public CodeBuilder AddField(string name, string type)
        {
            data.listOfData.Add(new Person { Name = name, Type = type });
            return this;
        }

        public override string ToString()
        {
            return data.ToString();
        }
    }


    #endregion

    [AtrTest]
    class Program
    {
        #region fields, classes etc...
        static int gggg { get; }
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

        interface IList2
        {
             int Count { get; set; }
        }

        interface ICounter
        {
            void Count(int i);
        }

        interface IListCounter : IList2, ICounter { }

        class C
        {
            void Count(int i) { }
            void t(int i)
            {

            }
            void Test(IListCounter x)      // 1 
            {
                x.Count(1);                // 2 
                ((IList2)x).Count = 1;             // 3 
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
            int[,] ff = new int[2, 1] { { 1}, { 2} };
            return a > b ?
                b > c ? b : a > c ? c : a
                : a > c ? a : b > c ? c : b;
        }
        private static void fgg()
        { }
        static int AverageInt2(int a, int b, int c)
        {
            return (a > b && a < c) || (a > c && a < b) ? a : ((b > a && b < c) || (b > c && b < a) ? b : c);
        }
        delegate void My();
        static event My eve;
        static byte ty;


        class P
        { }
        class Q : P
        { }

        class A1
        {
            public void abc(Q q)
            {
                Console.WriteLine("abc из A");
            }
        }

        class B1 : A1
        {
            public void abc(P p)
            {
                Console.WriteLine("abc из B");
            }
        }

        static bool SomeMethod1()
        {
            Console.WriteLine("Метод 1");
            return false;
        }

        static bool SomeMethod2()
        {
            Console.WriteLine("Метод 2");
            return true;
        }

        delegate IEnumerable<int> intsDel();

        static class Katavr 
        {
            static Katavr() { }
        }

        public class ExThread
        {

            // Non-Static method 
            public void mythread()
            {
                for (int x = 0; x < 4; x++)
                {
                    Console.WriteLine(x);
                    Thread.Sleep(1000);
                }
            }

            // Non-Static method 
            public void mythread1()
            {
                Console.WriteLine("2nd thread is Working..");
            }

            public void mythread2()
            {
                Console.WriteLine("3nd thread is Working..");
            }
        }

        class CheckIndexer
        {
            public int[] innerMass;

            public int this[int i]
            {
                get { return innerMass[i]; }
                set { innerMass[i] = value; }
            }
        }
        #endregion
        


        public static void SomethDo()
        {
            bool isdef = Attribute.IsDefined(typeof(Program), typeof(AtrTestAttribute));
            Attribute attr = System.Attribute.GetCustomAttribute(typeof(Program), typeof(AtrTestAttribute));

            if (!isdef) { Console.WriteLine($"AtrTest is not defined!"); }

            if (attr is AtrTestAttribute)
            {
                Console.WriteLine($"AtrTest: FAttr is {(attr as AtrTestAttribute).FAttr}");
            }
        }

        //public class BasicUsageModel //: PageModel
        //{
        //    private readonly IHttpClientFactory _clientFactory;

        //    public IEnumerable<GitHubBranch> Branches { get; private set; }

        //    public bool GetBranchesError { get; private set; }

        //    public BasicUsageModel(IHttpClientFactory clientFactory)
        //    {
        //        _clientFactory = clientFactory;
        //    }

        //    public async Task OnGet()
        //    {
        //        var request = new HttpRequestMessage(HttpMethod.Get,
        //            "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
        //        request.Headers.Add("Accept", "application/vnd.github.v3+json");
        //        request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

        //        var client = _clientFactory.CreateClient();

        //        var response = await client.SendAsync(request);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            using var responseStream = await response.Content.ReadAsStreamAsync();
        //            Branches = await JsonSerializer.DeserializeAsync
        //                <IEnumerable<GitHubBranch>>(responseStream);
        //        }
        //        else
        //        {
        //            GetBranchesError = true;
        //            Branches = Array.Empty<GitHubBranch>();
        //        }
        //    }
        //}

        public class BasicClass
        {
            private bool secretBasic = true;

            private InnerClass innerClass = new InnerClass();

            public bool GetSecretBasic() => this.secretBasic;

            public bool GetSecretInner() => this.innerClass.GetSecretInner();

            public bool PerformBasic()
            {
                return this.innerClass.PerformInner();
            }


            class InnerClass
            {
                private bool secretInner = true;

                public bool GetSecretInner() => this.secretInner;

                public bool PerformInner()
                {
                    BasicClass basicClass = new BasicClass();
                    bool basicSecretResult = basicClass.secretBasic; // ACCESS to PRIVATE FIELD

                    return basicSecretResult;
                }
            }
        }

        public class BasicInheritedClass : BasicClass
        {
            class Inner2Class
            {
                private bool secretInner = true;

                public bool GetSecretInner() => this.secretInner;

                public bool PerformInner()
                {
                    BasicInheritedClass basicClass = new BasicInheritedClass();
                    // CANNOT GET ACCESS to PRIVATE FIELD
                    //bool basicSecretResult = basicClass.secretBasic; // ACCESS to PRIVATE FIELD

                    //return basicSecretResult;

                    return false;
                }
            }
        }

        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int id { get; set; }
        }

        static void Main(string[] args)
        {
            do
            {
                
                CheckIndexer check = new CheckIndexer();

                int ad = 3;
                double fd = ad;
                ushort f = 'e';
                byte fdf = (byte)'f';
                bugaga.i();
                float aa = 10.5f;
                int ffg = (int)aa;
                object fff;
                Thread thread = new Thread((x) => { });
                fr[] frss = new fr[3] { new fr(), new fr(), new fr()};

                fr[] frs = new fr[1];
                ft[] fts = new ft[1] { new ft()};
                frs = fts;
                Console.Clear(); ////////////////////////////////////////////////////////////
                string secs = "wskwn7u2bvd8anlbh24hwqk7s_0sktmvbyb6ui7lmha.r87.me";
                string encoded = HttpUtility.HtmlEncode(secs);
                Console.WriteLine(secs);
                Console.WriteLine(encoded);
                Console.WriteLine(secs.Equals(encoded));

                Console.WriteLine("check if start");
                int h = 1;
                if (string.IsNullOrEmpty("") || ++h == 2)
                {
                    Console.WriteLine("enter if");
                }
                Console.WriteLine($"H = {h}");
                Console.WriteLine("check if end");




                string LeadsFormType = "profiTable";

                string productTypeDTO = null;

                string fType = LeadsFormType.ToLower();
                if (fType.Equals("accumulative")
                    || fType.Equals("free")
                    || fType.Equals("profitable"))
                {
                    productTypeDTO = "депозит";
                }
                else
                {
                    productTypeDTO = "кредит";
                }
                Console.WriteLine($"productTypeDTO: {productTypeDTO}");


                int ctr = 0;
                Pet[] pets = { 
                   new Pet { id = ++ctr, Name="Barley", Age=4 },
                   new Pet { id = ++ctr, Name="Boots", Age=4 },
                   new Pet { id = ++ctr, Name="Boots1", Age=4 },
                   new Pet { id = ++ctr, Name="Boots2", Age=4 },
                   new Pet { id = ++ctr, Name="Boots3", Age=4 },
                   new Pet { id = ++ctr, Name="Boots4", Age=4 },
                   new Pet { id = ++ctr, Name="Boots5", Age=4 },
                   new Pet { id = ++ctr, Name="Whiskers", Age=4 } };

                foreach (Pet pet in pets)
                {
                    Console.WriteLine("{0} - {1}", pet.Name, pet.Age);
                }

                IOrderedEnumerable<Pet> queryOrdered = pets.OrderBy(pet => pet.id);

                IEnumerable<Pet> query = queryOrdered.OrderBy(pet => pet.Age).Skip(4).Take(3).ToList();

                Console.WriteLine("\nSorted\n");
                foreach (Pet pet in query)
                {
                    Console.WriteLine("{0} - {1}", pet.Name, pet.Age);
                }

                // var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
                var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
                Console.WriteLine(cb);

                int[] nums = { 1, 1, 1, 1, 2, 2, 3, 4, 4, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7 };
                Dictionary<int, int> pairs = new Dictionary<int, int>();
                foreach (var number in nums)
                {
                    if(!pairs.TryGetValue(number, out int existValue))
                    {
                        pairs.Add(number, 1);
                        continue;
                    }
                    pairs[number]++;
                }

                foreach (var item in pairs.OrderBy(v => v.Value))
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        Console.Write($"{item.Key} ");
                    }
                }




                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine();

                string srcStr = @"{
                  \""On\"": true,
                  \""IpList\"": \""176.37.35.144,127.0.0.1,83.170.207.2,46.164.156.70,94.45.33.55,95.170.91.226,::1,94.125.125.154,176.36.123.192,89.184.67.44,176.36.71.183,176.126.61.26,188.163.2.222,95.67.43.166,176.37.176.64,192.168.0.77,176.37.136.28,176.116.192.85,178.151.60.141,78.27.153.252,176.38.16.54,94.154.226.229,188.163.9.204,178.94.226.71,176.36.170.210,188.163.10.4,188.163.82.4,178.94.225.11,212.9.226.158,37.229.52.63,46.219.210.204,46.219.226.208,176.36.145.160,46.211.135.92,46.211.97.233,192.168.0.127,193.106.139.101,193.106.139.39,193.106.139.252,93.74.8.144,176.37.40.174,37.73.157.103,46.211.99.128,77.47.226.131,178.54.209.65,193.106.139.118,193.106.139.26,46.219.210.252,192.168.2.5,192.168.2.1,95.67.92.21,188.163.74.31,94.153.103.216,178.128.162.234,46.219.226.158,176.115.103.23,194.44.66.28,176.36.159.45,194.44.66.19,194.44.66.,192.168.1.102,176.115.103.162,46.211.21.197,31.27.155.92,94.158.88.236,193.200.84.11,176.97.63.158,176.37.123.194,176.8.251.155,10.74.129.50,176.100.8.104,178.54.66.40,79.110.134.129,178.150.45.84,192.168.0.100,37.223.156.71,37.223.237.11,5.248.110.2,37.73.110.3\"",
                  \""DomensList\"": null
                }\""";

                List<string> chkStr = new List<string>
                {
                    "18.207.3.88 410",
                    "130.180.211.248",
                    "136.144.33.116",
                    "66.249.75.83",
                    "66.249.73.254",
                    "46.183.220.238",
                    "173.239.197.57",
                    "157.55.39.152",
                    "45.87.1.156",
                    "77.222.128.158",
                    "194.44.92.20",
                    "195.78.247.193"
                };

                foreach (var checkedIp in chkStr)
                {
                    Console.WriteLine($" {checkedIp}  \t| {srcStr.Contains(checkedIp).ToString().ToUpper()}");
                }

                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine();
                Console.WriteLine();



                foreach (var item in pairs.OrderBy(v => v.Value))
                {
                    Console.WriteLine($"Key - {item.Key}, Count - {item.Value}");
                }
                Console.WriteLine();

                long ql = long.MaxValue;
                float qf = ql;

                Console.WriteLine($"Cast numeric types result: {ql}");
                Console.WriteLine($"Cast numeric types result: {qf:F}");

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var d = new ConcurrentDictionary<int, int>();
                for (int i = 0; i < 1000000; i++) d[i] = 123;
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("Потрачено времени на выполнение: " + elapsedTime);

                stopWatch.Reset();
                stopWatch.Start();
                var dq = new Dictionary<int, int>();
                for (int i = 0; i < 1000000; i++) lock (dq) dq[i] = 123;
                stopWatch.Stop();
                ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("Потрачено времени на выполнение: " + elapsedTime);














                for (int i = 0; i < 200; i++)
                Console.Write("w");
                Console.WriteLine();

                InvokMeth w = new InvokMeth("str is set");
                Console.WriteLine(w.Str);


                SomethDo();

                TestClass testClass = new TestClass();
                ((ISomeInterface)testClass).move(out int france);
                Console.WriteLine($"Test ISomeInterface ->  {france}");

                Console.WriteLine(new Bas2().Write());
                // Creating instance for 
                // mythread() method 
                ExThread obj = new ExThread();

                // Creating and initializing threads  
                //Thread thr1 = new Thread(new ThreadStart(obj.mythread));
                //Thread thr2 = new Thread(new ThreadStart(obj.mythread1));
                //Thread thr3 = new Thread(new ThreadStart(obj.mythread2));
                //thr1.Start();

                //// Join thread 
                //thr1.Join();
                //thr2.Start();
                //thr3.Start();

                decimal x12 = decimal.MaxValue;
                decimal y12 = decimal.MaxValue;

                //unchecked
                //{
                //    decimal z = x12 * y12;
                //    Console.WriteLine(z.ToString());
                //}

                if (SomeMethod1() && SomeMethod2())
                {
                    Console.WriteLine("блок if выполнен");
                }

                B1 b1 = new B1();
                b1.abc(new Q());
                
                float tg = 5;
                tg = tg / 0;
                Console.WriteLine(tg);
                ft.Run();
                //int[,] fff = new int[4,5];
                int bb = 1;
                if (++bb < 2)
                Console.WriteLine("GG");
                byte bf = 2;
                bf--;
                ICollection r;
                var query4 = from x in Enumerable.Range(0,6)
                            join y in Enumerable.Range(3,9)
                            on x equals y select x;
                foreach (var q in query4) Console.Write(q);

                Console.WriteLine();
                Console.WriteLine(1+2+"kyky"+3+4);

                eve += () => Console.WriteLine("1t");
                eve += () => Console.WriteLine("2t");
                eve -= () => Console.WriteLine("1t");
                eve.Invoke();

                Console.WriteLine(bf);
                Console.WriteLine("Enter integer a b c:\n\n");

                /*Int32.TryParse(Console.ReadLine(), out int a);
                Int32.TryParse(Console.ReadLine(), out int b);
                Int32.TryParse(Console.ReadLine(), out int c);

                Console.WriteLine($"Average3 int({a},{b},{c}): {AverageInt3(a, b, c)}");*/


                #region Initial classes

                int DA = 244;
                int V = 212;
                int ID = 255;
                Console.WriteAscii("Test: Class INIT:", Color.FromArgb(DA, V, ID));

                Derived ddd = new Derived();
                #endregion




                Console.WriteLine("\n\nPress ANY for continue or ESCAPE for exit...");
            } while (ConsoleKey.Escape != Console.ReadKey(false).Key);
            /*
             * int count = 0;
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

        #region INITIAL classes hieracly test shows
        class AllBase
        {
            public AllBase()
            {
                Console.WriteLine("AllBase.Instance.Constructor", Color.OrangeRed);
                this.m_Field3 = new Tracker("AllBase.Instance.Field3", Color.OrangeRed);
                this.Virtual("From AllBase");
            }
            static AllBase()
            {
                Console.WriteLine("AllBase.Static.Constructor", Color.OrangeRed);
            }
            private Tracker m_Field1 = new Tracker("AllBase.Instance.Field1", Color.OrangeRed);
            private Tracker m_Field2 = new Tracker("AllBase.Instance.Field2", Color.OrangeRed);
            private Tracker m_Field3;
            static private Tracker s_Field1 = new Tracker("AllBase.Static.Field1", Color.OrangeRed);
            static private Tracker s_Field2 = new Tracker("AllBase.Static.Field2", Color.OrangeRed);
            virtual public void Virtual(string currentClassName)
            {
                Console.WriteLine("AllBase.Instance.Virtual: " + currentClassName, Color.OrangeRed);
            }
        }

        class Base : AllBase
        {
            public Base()
            {
                Console.WriteLine("Base.Instance.Constructor", Color.AntiqueWhite);
                this.m_Field3 = new Tracker("Base.Instance.Field3", Color.AntiqueWhite);
                this.Virtual("From Base");
            }
            static Base()
            {
                Console.WriteLine("Base.Static.Constructor", Color.AntiqueWhite);
            }
            private Tracker m_Field1 = new Tracker("Base.Instance.Field1", Color.AntiqueWhite);
            private Tracker m_Field2 = new Tracker("Base.Instance.Field2", Color.AntiqueWhite);
            private Tracker m_Field3;
            static private Tracker s_Field1 = new Tracker("Base.Static.Field1", Color.AntiqueWhite);
            static private Tracker s_Field2 = new Tracker("Base.Static.Field2", Color.AntiqueWhite);
            override public void Virtual(string currentClassName)
            {
                Console.WriteLine("Base.Instance.Virtual: " + currentClassName, Color.AntiqueWhite);
            }
        }
        class Derived : Base
        {
            public Derived()
            {
                Console.WriteLine("Derived.Instance.Constructor", Color.GreenYellow);
                this.m_Field3 = new Tracker("Derived.Instance.Field3", Color.GreenYellow);
            }
            static Derived()
            {
                Console.WriteLine("Derived.Static.Constructor", Color.GreenYellow);
            }
            private Tracker m_Field1 = new Tracker("Derived.Instance.Field1", Color.GreenYellow);
            private Tracker m_Field2 = new Tracker("Derived.Instance.Field2", Color.GreenYellow);
            private Tracker m_Field3;
            static private Tracker s_Field1 = new Tracker("Derived.Static.Field1", Color.GreenYellow);
            static private Tracker s_Field2 = new Tracker("Derived.Static.Field2", Color.GreenYellow);
            override public void Virtual(string currentClassName)
            {
                Console.WriteLine("Derived.Instance.Virtual: " + currentClassName, Color.GreenYellow);
            }
        }
        class Tracker
        {
            public Tracker(string text, Color color)
            {
                Console.WriteLine(text, color);
            }
        }

        #endregion
    }
}


namespace TestCode.UnitTests
{
    [TestFixture]
    public class FirstTestSuite
    {
        private static string Preprocess(string s)
        {
            return s;
            return s.Trim().Replace("\r\n", "\n");
        }

        [Test]
        public void EmptyTest()
        {
            var cb = new CodeBuilder("Foo");
            Assert.That(Preprocess(cb.ToString()), Is.EqualTo("public class Foo\n{\n}"));
        }

        [Test]
        public void PersonTest()
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Assert.That(Preprocess(cb.ToString()),
                Is.EqualTo(
                    @"public class Person
                        {
                            public string Name;
                            public int Age;
                        }"
                ));
        }
    }
}
