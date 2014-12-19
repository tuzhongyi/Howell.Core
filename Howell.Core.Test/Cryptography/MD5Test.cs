using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Cryptography
{
    static class MD5Test
    {
        public static void Test()
        {
            String value = "11aba1928301*3";
            Console.WriteLine("Value: {0} MD5: {1}.",value,new MD5().EncryptToString(value));
        }
    }
}
