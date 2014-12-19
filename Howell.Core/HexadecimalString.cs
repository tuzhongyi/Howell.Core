using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Howell.Conditions;

namespace Howell
{
    /// <summary>
    /// 16进制的字符串对象, 如果16进制字符串的值为FF0C,那么该方法将返回的数组为 byte[0]=255, byte[1]=12.
    /// </summary>
    /// <example>
    /// The following example shows how to use the <b>HexadecimalString</b> method.
    /// <code>
    /// <![CDATA[
    /// using Howell;
    /// 
    /// HexadecimalString hexString = HexadecimalString.Parse("FF0C");
    /// Byte[] bytes = hexString.GetBytes();
    /// String info = String.Format("hexString:{0}=", hexString.ToString());
    /// for (int i = 0; i < bytes.Length; ++i)
    /// {
    ///     info += bytes[i].ToString("X2");
    /// }
    /// info += " Bytes";
    /// Console.WriteLine(info);
    /// ]]>
    /// </code>
    /// </example>
    public class HexadecimalString
    {
        
        private Byte[] m_Buffer;
        /// <summary>
        /// 创建16进制的字符串对象
        /// </summary>
        /// <param name="buffer">字节数组</param>
        public HexadecimalString(Byte[] buffer)
            : this(buffer, 0, buffer.Length)
        {
        }
        /// <summary>
        /// 创建16进制的字符串对象
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">数组偏移量</param>
        /// <param name="length">数组长度</param>
        public HexadecimalString(Byte [] buffer,Int32 offset,Int32 length)
        {
            m_Buffer = new Byte[length];
            Array.Copy(buffer, offset, m_Buffer, 0, length);
        }
        /// <summary>
        /// 获取10进制的字节数组，如果16进制字符串的值为FF0C,那么该方法将返回的数组为 byte[0]=255, byte[1]=12.
        /// </summary>
        /// <returns>返回10进制的字节数组</returns>
        public Byte[] GetBytes()
        {
            return m_Buffer;
        }
        /// <summary>
        /// 转换为16进制的字符串
        /// </summary>
        /// <returns>返回16进制的字符串。</returns>
        public override string ToString()
        {
            String result = "";
            if (m_Buffer == null) return result;
            for (int i = 0; i < m_Buffer.Length; ++i)
            {
                result += m_Buffer[i].ToString("X2");
            }
            return result;
        }
        /// <summary>
        /// 解析16进制字符串
        /// </summary>
        /// <param name="hexString">16进制字符串实例对象。</param>
        /// <returns>返回具体的16进制字符串类型</returns>
        public static HexadecimalString Parse(String hexString)
        {
            Condition.WithExceptionOnFailure<FormatException>().Requires(hexString, "hexString").MatchPattern(@"^([0-9A-Fa-f]{2})*$", "match pattern failed.");
            List<Byte> bytes = new List<Byte>();
            for (int i = 0; i < hexString.Length / 2; ++i)
            {
                bytes.Add(Convert.ToByte(hexString.Substring(i * 2, 2), 16));
            }
            return new HexadecimalString(bytes.ToArray());
        }
    }
}
