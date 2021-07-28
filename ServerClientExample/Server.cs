using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerClientExample
{
    class Server
    {
        TcpListener server = null;

        public Server(string ip, int port)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            StartListener();
        }

        private void StartListener()
        {
            Console.WriteLine("Waiting for connections..");
            try
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("connected");
                    Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                server.Stop();
            }
        }

        public void HandleClient(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream =  client.GetStream();
            byte[] bytes = new byte[1024*1024];
            int i;
            try
            {
                while ( (i = stream.Read(bytes, 0, bytes.Length)) != 0 )
                {
                    Console.WriteLine("some data received...");
                    // parsing/handling received data
                    BinaryFormatter bf = new BinaryFormatter();
                    using(MemoryStream ms = new MemoryStream() )
                    {
                        ms.Write(bytes, 0, i);
                        ms.Position = 0; //seek to begining of stream
                        Employee emp = bf.Deserialize(ms) as Employee;
                        Console.WriteLine(emp);
                    }
                    
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                client.Close();
            }
        }
    }
}
