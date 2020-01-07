using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp11
{
    class RemoteEbs
    {
        public string SerialNum;


        TcpListener Server = null;

        public TcpListener Create(TcpListener serv)
        {
            // Set the TcpListener on port 13000.
            Int32 port = 13000;
                IPAddress localAddr = IPAddress.Any;
                // TcpListener server = new TcpListener(port);
                serv = new TcpListener(localAddr, port);
            return serv;
        }

        public void StartServer()
        {
            Console.WriteLine(SerialNum);
            TcpListener server = Create(Server);
            try
            {
            server.Start();
            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();
                        byte[] answer = { 111, 107, 13 };
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(answer, 0, answer.Length);
                        if (data == "{CMD")
                        {
                            Console.WriteLine("Sent: {0}", SerialNum);
                        }
                        else
                        {
                            Console.WriteLine("error");
                        }
                }

                // Shutdown and end connection
                client.Close();
            }
            }
            catch (SocketException e)
            {
            Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }
    }
}
