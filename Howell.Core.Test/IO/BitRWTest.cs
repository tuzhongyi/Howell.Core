using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Howell.IO
{
    /// <summary>
    /// 位读写操作
    /// </summary>
    class BitRWTest
    {
        public static void Test()
        {
            Byte[] buffer = null;
            using (BitWriter writer = new BitWriter(new MemoryStream()))
            {
                writer.WriteBit(1);
                Console.WriteLine("Write Bit1 [1]");
                writer.WriteBits(12, 5);
                Console.WriteLine("Write Bits7 [12]");
                writer.Write((Byte)Byte.MaxValue);
                Console.WriteLine("Write Byte [{0}]",Byte.MaxValue);
                writer.Write((Int16)Int16.MaxValue);
                Console.WriteLine("Write Int16 [{0}]", Int16.MaxValue);
                writer.Write((Int32)Int32.MaxValue);
                Console.WriteLine("Write Int32 [{0}]", Int32.MaxValue);
                writer.Write((Int64)Int64.MaxValue);
                Console.WriteLine("Write Int64 [{0}]", Int64.MaxValue);
                writer.Write((UInt16)UInt16.MaxValue);
                Console.WriteLine("Write UInt16 [{0}]", UInt16.MaxValue);
                writer.Write((UInt32)UInt32.MaxValue);
                Console.WriteLine("Write UInt32 [{0}]", UInt32.MaxValue);
                writer.Write((UInt64)UInt64.MaxValue);
                Console.WriteLine("Write UInt64 [{0}]", UInt64.MaxValue);
                buffer = (writer.BaseStream as BitStream).ToArray();
            }
            using(BitReader reader = new BitReader(new MemoryStream(buffer)))
            {
                Console.WriteLine("Read Bit1 [{0}]", reader.ReadBit());
                Console.WriteLine("Read Bits7 [{0}]", reader.ReadBits(5));
                Console.WriteLine("Read Byte [{0}]", reader.ReadByte());
                Console.WriteLine("Read Int16 [{0}]", reader.ReadInt16());
                Console.WriteLine("Read Int32 [{0}]", reader.ReadInt32());
                Console.WriteLine("Read Int64 [{0}]", reader.ReadInt64());
                Console.WriteLine("Read UInt16 [{0}]", reader.ReadUInt16());
                Console.WriteLine("Read UInt32 [{0}]", reader.ReadUInt32());
                Console.WriteLine("Read UInt64 [{0}]", reader.ReadUInt64());
            }

        }

    }
}
