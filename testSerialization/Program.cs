using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
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

        public Tst(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            // Десериализировать Players в массив для соответствия сериализации:
            //Person[J а = (Person[])si.GetValue("PlayerData", typeof(Person[]));
            // Сконструировать новый список List, используя этот массив:
            //Players = new List<Person>(а);
        }
    }

    [Serializable] class Person
    {
        public string Name;
        public string SecondValue;
    }
}
