using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializationExample
{
    static class BinarySerialization
    {

        public static void Create()
        {
            Employee emp = new Employee()
            {
                Id = 123, FirstName = "John", LastName = "Smith",
                AccessRooms = new List<int>() { 1,2,3 },
                IsManager = null,
                StartAt = new DateTime(2020, 01, 01)               
            };
            emp.SetToken(Guid.NewGuid().ToString());
            
            // serialization
            using (FileStream fs = new FileStream("c:/tmp/dump.dat", FileMode.Create)) {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, emp);
            }

            // de-serialization
            using (FileStream fs = new FileStream("c:/tmp/dump.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Employee emp1 = bf.Deserialize(fs) as Employee;
                Console.WriteLine(emp1);
            }

            //---- serialize list of objects
            List<Employee> empList = new List<Employee>()
            {
                emp, emp, emp
            };

            // serialization
            using (FileStream fs = new FileStream("c:/tmp/dump-list.dat", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, empList);
            }

            // de-serialization
            using (FileStream fs = new FileStream("c:/tmp/dump-list.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<Employee> emp2 = bf.Deserialize(fs) as List<Employee>;
                foreach (var item in emp2)
                {
                    Console.WriteLine(item);
                }
            }


        }

    }
}
