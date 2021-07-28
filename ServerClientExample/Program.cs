using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(
                delegate()
                {
                    Server server = new Server("127.0.0.1", 3456);
                }
            );
            thread.Start();

            // creating client
            try
            {
                //Employee emp = new Employee()
                //{
                //    Id = 123,
                //    FirstName = "John",
                //    LastName = "Smith"
                //};
                //using (FileStream fs = new FileStream("c:/tmp/dump.dat", FileMode.Create))
                //{
                //    BinaryFormatter bf = new BinaryFormatter();
                //    bf.Serialize(fs, emp);
                //}

                // estabilish connection with server
                TcpClient client = new TcpClient("127.0.0.1", 3456);
                NetworkStream stream = client.GetStream();
                Byte[] data = File.ReadAllBytes("c:/tmp/dump.dat");
                stream.Write(data, 0, data.Length);

                stream.Close();
                client.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
