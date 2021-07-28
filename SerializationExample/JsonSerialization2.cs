using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SerializationExample
{
    static class JsonSerialization2
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


            JavaScriptSerializer js = new JavaScriptSerializer();
            String s = js.Serialize(empArray);
            File.WriteAllText("c:/tmp/dump2.json", s);

            s = File.ReadAllText("c:/tmp/dump2.json");
            EmployeeJson[] empList = js.Deserialize<EmployeeJson[]>(s);
            foreach (var item in empList)
            {
                Console.WriteLine(item);
            }
            

        }

    }
}
