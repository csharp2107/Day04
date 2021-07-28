using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SerializationExample
{
    static class JsonSerialization1
    {

        public static void Create()
        {
            EmployeeJson emp = new EmployeeJson()
            {
                Id = 123, FirstName = "John", LastName = "Smith",
                IsManager = true,
                StartAt = new DateTime(2020, 01, 01)               
            };
            emp.SetToken(Guid.NewGuid().ToString());
            EmployeeJson[] empArray = new EmployeeJson[]
            {
                emp, emp, emp
            };
            
            // serialization
            using (FileStream fs = new FileStream("c:/tmp/dump1.json", FileMode.Create)) {
                DataContractJsonSerializer js = 
                    new DataContractJsonSerializer(typeof(EmployeeJson[]));
                js.WriteObject(fs, empArray);
            }

            // de-serialization
            using (FileStream fs = new FileStream("c:/tmp/dump1.json", FileMode.Open))
            {
                DataContractJsonSerializer js =
                    new DataContractJsonSerializer(typeof(EmployeeJson[]));
                EmployeeJson[] result = (EmployeeJson[])js.ReadObject(fs);
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            }

            

        }

    }
}
