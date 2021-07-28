using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SerializationExample
{
    static class PerformanceCheck
    {
        public static void Run()
        {
            int MAX_ITEMS = 100000;

            List<Employee> empList = new List<Employee>();
            for (int i = 1; i <= MAX_ITEMS; i++)
            {
                Employee emp = new Employee()
                {
                    Id = i,
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    AccessRooms = new List<int>() { 1, 2, 3 },
                    IsManager = false,
                    StartAt = new DateTime(2020, 01, 01),
                    ExtraData = new List<string>() { "AAA", "BBB" }
                };
                emp.SetToken(Guid.NewGuid().ToString());
                empList.Add(emp);
            }

            // serialization
            string DEST_FILE = "c:/tmp/performance.xml";

            // XML Serializer 
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (FileStream fs = new FileStream(DEST_FILE, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Employee>));
                xs.Serialize(fs, empList);
            }
            sw.Stop();
            Console.WriteLine($"Serialization XML: {sw.ElapsedMilliseconds}");

            // de-serialization
            sw.Restart();
            using (FileStream fs = new FileStream(DEST_FILE, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Employee>));
                List<Employee> emps = (List<Employee>)xs.Deserialize(fs);                
            }
            sw.Stop();
            Console.WriteLine($"Deserialization XML: {sw.ElapsedMilliseconds}");


            // JSON 1
            DEST_FILE = "c:/tmp/performance1.json";
            sw.Restart();
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 60_000_000;
            String s = js.Serialize(empList);
            File.WriteAllText(DEST_FILE, s);
            sw.Stop();
            Console.WriteLine($"Serialization JSON1: {sw.ElapsedMilliseconds}");

            sw.Restart();
            s = File.ReadAllText(DEST_FILE);
            List<Employee> empList1 = js.Deserialize<List<Employee>>(s);
            sw.Stop();
            Console.WriteLine($"Deserialization JSON1: {sw.ElapsedMilliseconds}");


            // JSON2 - JSON.NET library
            DEST_FILE = "c:/tmp/performance2.json";
            sw.Restart();
            s = JsonConvert.SerializeObject(empList);
            File.WriteAllText(DEST_FILE, s);
            sw.Stop();
            Console.WriteLine($"Serialization JSON2: {sw.ElapsedMilliseconds}");

            sw.Restart();
            s = File.ReadAllText(DEST_FILE);
            empList1 = JsonConvert.DeserializeObject<List<Employee>>(s);
            sw.Stop();
            Console.WriteLine($"Deserialization JSON2: {sw.ElapsedMilliseconds}");

            Console.ReadKey();


        }
    }
}
