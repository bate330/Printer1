using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Printer
    {
        public string IPAdd;
        public int Port;

        public void DownloadDeviceId()
        {
            IPAdd = "192.168.0.6";
            Port = 3000;
            UdpClient client = new UdpClient();
            TDeviceId id = new TDeviceId();
            try
            {
                IPEndPoint sep = new IPEndPoint(IPAddress.Parse(IPAdd), Port);
                client.Connect(sep);
                client.Send(new byte[] { 0x02, 0x00, 0x02, 0x01, 0x00, 0xFD, 0x03 }, 7);
                IPEndPoint rep = new IPEndPoint(IPAddress.Any, 0);
                IPEndPoint rep1 = new IPEndPoint(IPAddress.Any, 0);
                Byte[] recBytes = client.Receive(ref rep);
                Byte[] recBytes1 = client.Receive(ref rep1);
                id.Rec = recBytes1;
                Console.WriteLine("Nawiązano Połączenie");
                id.Load();
                client.Close();
            }
            catch
            {
                Console.WriteLine("Niepoprawny adres lub nie można nawiązać połączenia", "Connection Error");
            }
        }
    }
}
