using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net.NetworkInformation
{
    class PhyscialAddressTest
    {
        public static void Test()
        {
            Console.WriteLine("++++++++++++++++++System.Net.NetworkInformation.PhysicalAddress++++++++++++++++++");
            PhysicalAddress address = PhysicalAddress.Parse("00-00-00-00-00-00");
            Byte [] bytes = address.GetAddressBytes();
            String bytesString = "Bytes:";
            for (int i = 0; i < bytes.Length;++i)
            {
                bytesString += String.Format("{0:X2} ", bytes[i]);
            }
            Console.WriteLine(bytesString);
            Console.WriteLine("PhysicalAddress.ToString:{0}", address.ToString());
            Console.WriteLine("PhysicalAddress.ToFormattedString:{0}", address.ToFormattedString());            
        }
    }
}
