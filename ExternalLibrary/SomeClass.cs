using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalLibrary
{
    public class SomeClass
    {
        public string SomeProperty { get; set; }
        public int SomeIntegerProperty { get; set; }

        public int Field = 3;

        public SomeClass()
        {
            SomeProperty = "ABCDEF";
            SomeIntegerProperty = 1;
        }

        public SomeClass(string s)
        {
            SomeProperty = s;
        }

        public int AddFieldToProperty()
        {
            return Field + SomeIntegerProperty;
        }
    }
}
