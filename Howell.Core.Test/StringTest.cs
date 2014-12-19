using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell
{
    static class StringTest
    {
        public static void Test()
        {
            BinaryStringTest();
            HexadecimalStringTest();
        }
        private static void BinaryStringTest()
        {
            Console.WriteLine("=============BinaryString===============");
            BinaryString binString = BinaryString.Parse("11101");
            Boolean[] bits = binString.GetBits();
            String info = String.Format("BinString:{0}=", binString.ToString());
            for(int i =0;i < bits.Length;++i)
            {
                info += bits[i] ? "1" : "0";
            }
            info += " Bits";
            Console.WriteLine(info);
            return;
        }
        private static void HexadecimalStringTest()
        {
            Console.WriteLine("=============HexadecimalString===============");
            HexadecimalString hexString = HexadecimalString.Parse("FF0C");
            Byte[] bytes = hexString.GetBytes();
            String info = String.Format("hexString:{0}=", hexString.ToString());
            for (int i = 0; i < bytes.Length; ++i)
            {
                info += bytes[i].ToString("X2");
            }
            info += " Bytes";
            Console.WriteLine(info);
        }
    }
}
