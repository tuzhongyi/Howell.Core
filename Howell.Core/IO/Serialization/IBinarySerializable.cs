using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Howell.IO.Serialization
{
    /// <summary>
    /// 二进制序列化接口
    /// </summary>
    public interface IBinarySerializable
    {        
        /// <summary>
        /// 从对象的 Bit 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 System.IO.BinaryWriter 流。</param>
        void ReadBinary(BinaryReader reader);
        /// <summary>
        /// 将对象转换为其 Bit 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 System.IO.BinaryWriter 流。</param>
        void WriteBinary(BinaryWriter writer);
    }
}
