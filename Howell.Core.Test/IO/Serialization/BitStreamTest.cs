using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Howell.IO.Serialization
{
    public class BitStreamTest
    {
        public static void Test()
        {
            BitStream s = new BitStream();
            Boolean boolValue = true;
            Byte byteValue = 0x0F;
            Byte [] bufferValue = new Byte [2];            
            bufferValue[0] = 0x1F;
            bufferValue[1] = 0xF1;
            Boolean [] boolBufferValue = new Boolean[7] { true, false, false, false, false, true, true };
            s.WriteBit(boolValue);
            Console.WriteLine("WriteBit:{0} Position:{1}", boolValue, s.Position);
            s.WriteByte(byteValue);
            Console.WriteLine("WriteByte:{0} Position:{1}", byteValue, s.Position);
            s.Write(bufferValue, 0, 2);
            Console.WriteLine("Write:{0} {1} Position:{2}", bufferValue[0], bufferValue[1], s.Position);
            s.Write(boolBufferValue, 0, 7);
            Console.WriteLine("Write:{0} {1} {2} {3} {4} {5} {6} Position:{7}", boolBufferValue[0], boolBufferValue[1], boolBufferValue[2], boolBufferValue[3],
                boolBufferValue[4], boolBufferValue[5], boolBufferValue[6], s.Position);

            s.Seek(0, SeekOrigin.Begin);
            boolValue = (Boolean)s.ReadBit();
            Console.WriteLine("ReadBit:{0} Position:{1}", boolValue, s.Position);
            byteValue = (Byte)s.ReadByte();
            Console.WriteLine("ReadByte:{0} Position:{1}", byteValue, s.Position);
            s.Read(bufferValue, 0, 2);
            Console.WriteLine("Read:{0} {1} Position:{2}", bufferValue[0], bufferValue[1], s.Position);
            s.Read(boolBufferValue, 0, 7);
            Console.WriteLine("Write:{0} {1} {2} {3} {4} {5} {6} Position:{7}", boolBufferValue[0], boolBufferValue[1], boolBufferValue[2], boolBufferValue[3],
                boolBufferValue[4], boolBufferValue[5], boolBufferValue[6], s.Position);
            Console.WriteLine(s.ToString());            
        }
    }
}
