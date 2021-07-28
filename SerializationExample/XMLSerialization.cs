using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializationExample
{
    static class XMLSerialization
    {

        public static void Create()
        {
            Employee emp = new Employee()
            {
                Id = 123, FirstName = "John", LastName = "Smith",
                AccessRooms = new List<int>() { 1,2,3 },
                IsManager = null,
                StartAt = new DateTime(2020, 01, 01),
                ExtraData = new List<string>() { "AAA", "BBB" }
            };
            emp.SetToken(Guid.NewGuid().ToString());
            Employee[] empArray = new Employee[]
            {
                emp, emp, emp
            };
            
            // serialization
            using (FileStream fs = new FileStream("c:/tmp/dump.xml", FileMode.Create)) {
                XmlSerializer xs = new XmlSerializer(typeof(Employee[]));
                xs.Serialize(fs, empArray);
            }

            // de-serialization
            using (FileStream fs = new FileStream("c:/tmp/dump.xml", FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Employee[]));
                Employee[] emps = (Employee[])xs.Deserialize(fs);
                foreach (var item in emps)
                {
                    Console.WriteLine(item);
                }
            }

            


        }

    }
}
