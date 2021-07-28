using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SerializationExample
{
    static class JsonSerialization3
    {

        public static void Create()
        {
            EmployeeJson emp = new EmployeeJson()
            {
                Id = 123, FirstName = "John", LastName = "Smith",
                IsManager = true,
                StartAt = new DateTime(2020, 01, 01),
                ExtraData = new List<string>() { "AAA", "BBB"}
            };
            emp.SetToken(Guid.NewGuid().ToString());
            EmployeeJson[] empArray = new EmployeeJson[]
            {
                emp, emp, emp
            };


            string s = JsonConvert.SerializeObject(empArray, 
                Formatting.Indented, 
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore                    
                });
            File.WriteAllText("c:/tmp/dump3.json", s);

            String s1 = File.ReadAllText("c:/tmp/dump3.json");
            EmployeeJson[] empList = JsonConvert.DeserializeObject<EmployeeJson[]>(s1);
            object obj = JsonConvert.DeserializeObject(s1);
            foreach (var item in empList)
            {
                Console.WriteLine(item);
            }

        }

        public static void ErrorHandle()
        {
            List<string> errors = new List<string>();
            String s = @"
                [
                  '2021-07-28T00:00:00Z', '2021-08-28T00:00:00Z',
                   null, 'AAAA', [1]
                ]
            ";
            List<DateTime> dtList = JsonConvert.
                DeserializeObject<List<DateTime>>(s, new JsonSerializerSettings { 
                    Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                    {
                        errors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    },
                    Converters = { new IsoDateTimeConverter() }
                });
            foreach (var item in dtList)
            {
                Console.WriteLine(item);
            }
        }

        class MyUsers
        {
            public String FName { get; set; }
            public String LName { get; set; }
        }
        public static void MissingMembers()
        {
            String s = @"
                { 'fname' : 'Elvis', 
                  'lnaMe' : 'Presley', 'LNAME' : 'XYZ', 
                  'IsAlive' : false  }
            ";
            MyUsers user = JsonConvert.DeserializeObject<MyUsers>(s, 
                new JsonSerializerSettings { 
                    MissingMemberHandling = MissingMemberHandling.Ignore
                } );
            Console.WriteLine(user.FName);
        }

        class Rates
        {
            [JsonProperty("currency")]
            public string CurrencyName { get; set; }
            
            [JsonProperty("code")]
            public string CurrencyCode { get; set; }
            
            [JsonProperty("mid")]
            public double AverageRate { get; set; }
        }

        public static void NBP()
        {
            WebClient wb = new WebClient();
            String s = wb.DownloadString("http://api.nbp.pl/api/exchangerates/tables/A/?format=json");
            JArray ja = JArray.Parse(s);
            IList<JToken> results = ja[0]["rates"].Children().ToList();

            List<Rates> rates = new List<Rates>();
            foreach (var item in results)
            {
                Rates rate = item.ToObject<Rates>();
                rates.Add(rate);
            }
            

        }

    }
}
