using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp11
{
    class RemoteEbs
    {
        public Byte[] Rec;
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
            //Console.WriteLine(SerialNum);
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
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        // Send back a response.
                        TDeviceId id = new TDeviceId();
                        if (data == "{CMD1")
                        {
                            string SerialNumber = "";
                            Console.WriteLine(id.SerialNumberMethod(ref SerialNumber, Rec));
                            byte[] SerialNumberByte = ConvertStringToByte(SerialNumber);
                            stream.Write(SerialNumberByte, 0, SerialNumberByte.Length);
                        }
                        else if(data == "{CMD2")
                        {
                            string NameLenght = "";
                            Console.WriteLine(id.NameLengthMethod(ref NameLenght, Rec));
                            byte[] NameLenghtByte = ConvertStringToByte(NameLenght);
                            stream.Write(NameLenghtByte, 0, NameLenghtByte.Length);
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
        public byte[] ConvertStringToByte(string StringToByte)
        {
            char[] Chars = StringToByte.ToCharArray();
            decimal[] Dec = new decimal[Chars.Length + 1];
            byte[] Byte = new byte[Dec.Length];
            for (int j = 0; j <= Chars.Length - 1; j++)
            {
                Dec[j] = Chars[j];
            }
            Dec[Chars.Length] = 13;
            for (int j = 0; j <= Dec.Length - 1; j++)
            {
                Byte[j] = Convert.ToByte(Dec[j]);
            }
            return Byte;
        }
    }
}
