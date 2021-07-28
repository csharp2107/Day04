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

            CreateObject();

            var assembly = Assembly.LoadFrom("ExternalLibrary.dll");
            foreach (var type in assembly.ExportedTypes)
            {
                Console.WriteLine("====================");
                Console.WriteLine(type.FullName);
                
                foreach (var ctr in type.GetConstructors())
                {
                    Console.WriteLine($"Constructor: {ctr.Name}");
                    foreach (var _params in ctr.GetParameters())
                    {
                        Console.WriteLine($"{_params.Name}: {_params.ParameterType}");
                    }
                }

                Console.WriteLine("======================");
                Console.WriteLine("Properties");
                foreach (var prop in type.GetProperties())
                {
                    Console.WriteLine($"{prop.Name}: {prop.PropertyType}");
                }

                Console.WriteLine("======================");
                Console.WriteLine("Fields");
                foreach (var prop in type.GetFields())
                {
                    Console.WriteLine($"{prop.Name}: {prop.FieldType}");
                }

                Console.WriteLine("======================");
                Console.WriteLine("Methods");
                foreach (var method in type.GetMethods())
                {
                    Console.WriteLine($"{method.Name}");
                }

                
            } 
        }

        static void CreateObject()
        {
            Type classType = typeof(ReflectionClass);
            object reflectionClass = Activator.CreateInstance(classType);
            ReflectionClass castedClass = reflectionClass as ReflectionClass;
            if (castedClass!=null)
            {
                Console.WriteLine($"{castedClass.fieldA}, {castedClass.fieldB}");
            }

            object[] arguments = new object[] { "Hello world!" };
            reflectionClass = Activator.CreateInstance(classType, arguments);
            castedClass = reflectionClass as ReflectionClass;
            if (castedClass != null)
            {
                Console.WriteLine($"{castedClass.fieldA}, {castedClass.fieldB}");
            }

            // change value for the property
            PropertyInfo property = classType.GetProperty("fieldB");
            property.SetValue(reflectionClass, "1234567890");
            string newValue = (string)property.GetValue(reflectionClass);
            Console.WriteLine(newValue);
        }
    }
}
