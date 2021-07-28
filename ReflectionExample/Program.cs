using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ReflectionTest.fieldA = 1;
            ReflectionTest.fieldB = 2;
            ReflectionTest.fieldC = "AAA";
            ReflectionTest.fieldD = "BBB";
            //ReflectionTest.Lookup();

            var assembly = Assembly.LoadFrom("ExternalLibrary.dll");
            foreach (var type in assembly.ExportedTypes)
            {
                Console.WriteLine(type.FullName);
            } 
        }
    }
}
