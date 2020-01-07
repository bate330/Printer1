using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{ 
    class TDeviceId
    {
        public Byte[] Rec;
        public string SerialNumber;
        public string NameLenght;
        RemoteEbs rem = new RemoteEbs();

        public void Load()
        {
            var PrinterSerialNumber = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            Array.Copy(Rec, 15, PrinterSerialNumber, 2, 2);
            Array.Copy(Rec, 34, PrinterSerialNumber, 0, 2);
            string show = BitConverter.ToString(PrinterSerialNumber).Replace("-", "");
            int intvalue = int.Parse(show, System.Globalization.NumberStyles.HexNumber);
            SerialNumber = Convert.ToString(intvalue);
            rem.SerialNum = SerialNumber;
            Console.WriteLine("Numer seryjny drukarki: " + SerialNumber);
            Array.Copy(Rec, 6, PrinterSerialNumber, 0, 1);
            string HexValue6ByteString = BitConverter.ToString(PrinterSerialNumber).Replace("-", "");
            char[] HexValues6ByteChars = HexValue6ByteString.ToCharArray();
            NameLenght = HexValues6ByteChars[0].ToString();
            Console.WriteLine("Długość znaków dozwolona przez drukarkę: " + NameLenght);
        }
    }
}
