using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    static class ReflectionTest
    {
        public static int fieldA;
        public static int fieldB;
        public static string fieldC;
        public static string fieldD;
        private static string fieldE = "XYZ";

        public static void Lookup()
        {
            Type type = typeof(ReflectionTest);
            FieldInfo[] fields = type.GetFields();// type.GetFields(BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                string name = field.Name;
                object obj = field.GetValue(null);
                
                if (obj is int)
                {
                    int value = (int)obj;
                    Console.WriteLine($"[{name}] int value ={value}");
                }
                if (obj is string)
                {
                    string value = (string)obj;
                    Console.WriteLine($"[{name}] string value={value}");
                }
            }
        }
    }
}
