using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.IO.Serialization
{
    /// <summary>
    /// Bit序列化接口
    /// </summary>
    public interface IBitSerializable
    {
        /// <summary>
        /// 从对象的 Bit 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 Howell.IO..BitReader 流。</param>
        void ReadBitFormat(BitReader reader);
        /// <summary>
        /// 将对象转换为其 Bit 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 Howell.IO.BitWriter 流。</param>
        void WriteBitFormat(BitWriter writer);
    }
}
