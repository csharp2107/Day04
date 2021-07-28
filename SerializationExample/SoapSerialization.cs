using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace SerializationExample
{
    static class SoapSerialization
    {

        public static void Create()
        {
            EmployeeNN emp = new EmployeeNN()
            {
                Id = 123, FirstName = "John", LastName = "Smith",
                IsManager = true,
                StartAt = new DateTime(2020, 01, 01)               
            };
            emp.SetToken(Guid.NewGuid().ToString());
            
            // serialization
            using (FileStream fs = new FileStream("c:/tmp/dump-soap.xml", FileMode.Create)) {
                SoapFormatter sf = new SoapFormatter();
                sf.Serialize(fs, emp);
            }

            // de-serialization
            using (FileStream fs = new FileStream("c:/tmp/dump-soap.xml", FileMode.Open))
            {
                SoapFormatter sf = new SoapFormatter();
                EmployeeNN emp1 = sf.Deserialize(fs) as EmployeeNN;
                Console.WriteLine(emp1);
            }

            

        }

    }
}
