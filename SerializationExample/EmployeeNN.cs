using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializationExample
{
    [Serializable]
    public class EmployeeNN
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsManager { get; set; }
        //public List<int> AccessRooms { get; set; }
        public DateTime StartAt { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        private String token;

        public void SetToken(string token)
        {
            this.token = token;
        }

        //[XmlIgnore]
        //public List<string> ExtraData { get; set; }
    }
}
