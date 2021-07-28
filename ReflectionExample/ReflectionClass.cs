using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    class ReflectionClass
    {
        public int fieldA { get; set; }
        public string fieldB { get; set; }

        public ReflectionClass()
        {
            fieldA = 1234;
            fieldB = "ABCD";
        }

        public ReflectionClass(string s)
        {
            fieldB = s;
        }

        public int MethodInt(int ratio)
        {
            return fieldA * ratio;
        }

        public string MethodStr()
        {
            return fieldB.ToLower();
        }
    }
}
