using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace testSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Tst tst = new Tst();
            tst.Name = "My name";
            tst.SecondValue = "My second value";

            Person pers = new Person();
            pers.Name = "My p name";
            pers.SecondValue = "My p second value";
            DataContractSerializer ds = new DataContractSerializer(typeof(Person));
            using (Stream xmlWriter = File.Create("dataContractWriterTst.xml"))
            {
                ds.WriteObject(xmlWriter, pers);
            }
            Person pers2;
            using (Stream xmlWriter = File.OpenRead("dataContractWriterTst.xml"))
            {
                pers2 = (Person)ds.ReadObject(xmlWriter);
            }
            Console.WriteLine(pers2.Name + ", " + pers2.SecondValue);

            tst.p = pers;
            tst.p2 = pers;
            DataContractSerializerSettings set = new DataContractSerializerSettings { PreserveObjectReferences = true };
            DataContractSerializer ds2 = new DataContractSerializer(typeof(Tst), set);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            using (XmlWriter xmlWriter = XmlWriter.Create("xmlWriterTst.xml", xmlWriterSettings))
            {
                ds2.WriteObject(xmlWriter, tst);
            }



            // Binary
            Tst2 tst2 = new Tst2();
            tst2.Name = "My name";
            tst2.SecondValue = "My second value";

            Person pers3 = new Person();
            pers3.Name = "My p name";
            pers3.SecondValue = "My p second value";

            tst2.p = pers3;
            tst2.p2 = pers3;

            IFormatter formatter = new BinaryFormatter { FilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Low };
            using (FileStream stream = File.Create("SerializedTst.bin"))
            {
                formatter.Serialize(stream, tst2);
            }
            using (FileStream stream = File.OpenRead("SerializedTst.bin"))
            {
                Tst2 t = (Tst2)formatter.Deserialize(stream);
                Console.WriteLine($@"Binary: 
                                t.Name - {t?.Name ?? "empty"},
                                t.SecondValue - {t?.SecondValue ?? "empty"},
                                p.Name - {t?.p?.Name ?? "empty"},
                                p.SecondValue - {t?.p?.SecondValue ?? "empty"},
                                p2.Name - {t?.p2?.Name ?? "empty"},
                                p2.SecondValue - {t?.p2?.SecondValue ?? "empty"}");
            }



            Console.ReadKey();
        }
    }

    [Serializable] class Tst : ISerializable
    {
        public string Name;
        public string SecondValue;
        [DataMember]public Person p;
        [DataMember]public Person p2;

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("SecondValue", this.SecondValue);
        }

        [OnSerializing] void CreatePersonIfNull(StreamingContext ctx)
        {
            this.p = this.p ?? new Person { Name = "Default pname", SecondValue = "Default pval" };
        }

        public Tst() { }
        protected Tst(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            // Десериализировать Player для соответствия сериализации:
            p = (Person)info.GetValue("p", typeof(Person));
        }
    }

    [Serializable] class Person
    {
        public string Name;
        public string SecondValue;
    }

    [Serializable]
    class Tst2
    {
        public string Name;
        public string SecondValue;
        [DataMember] public Person p;
        [DataMember] public Person p2;

        [OnSerializing]
        void CreatePersonIfNull(StreamingContext ctx)
        {
            this.p = this.p ?? new Person { Name = "Default pname", SecondValue = "Default pval" };
            this.p2 = this.p2 ?? new Person { Name = "Default pname", SecondValue = "Default pval" };
        }

    }
}
