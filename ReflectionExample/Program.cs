using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Pose;

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

        public class DemoClass
        {
            private readonly double constValue = 1.23456;

            public double Show()
            {
                return constValue;
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
            property.SetValue(reflectionClass, "ABCDEFGHIJKL");
            string newValue = (string)property.GetValue(reflectionClass);
            Console.WriteLine(newValue);

            // get information about method/s inside class
            MethodInfo method = classType.GetMethod("MethodStr");
            String result = (string)method.Invoke(reflectionClass, new object[0]);
            Console.WriteLine(result);

            method = classType.GetMethod("MethodInt");
            Int32 x = (Int32)method.Invoke(reflectionClass, new object[] { 10 });
            Console.WriteLine(x);

            DemoClass dc = new DemoClass();
            FieldInfo field = typeof(DemoClass).GetField("constValue", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field!=null)
            {
                field.SetValue(dc, -9.8765);
                Console.WriteLine(dc.Show());
            }

            Shim consoleShim = Shim.Replace(
                () => Console.WriteLine(Is.A<string>())).With(
                        delegate(string s)
                        {
                            Console.WriteLine($"My text: {s}");
                        }
                );

            Shim methodShim = Shim.Replace(
                () => dc.Show()).With(
                    delegate(DemoClass @this)
                    {
                        return 111.111;
                    }
                );

            PoseContext.Isolate(
                () =>
                {
                    Console.WriteLine("ABCDEF");
                    Console.WriteLine(dc.Show().ToString());
                }, consoleShim, methodShim
            );

            Console.ReadKey();

        }
    }
}
