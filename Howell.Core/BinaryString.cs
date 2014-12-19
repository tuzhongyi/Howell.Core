using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Howell.Conditions;

namespace Howell
{
    /// <summary>
    /// 二进制字符串对象, 如二进制字符串为：01，怎转换后的位值数组为 bit[0] = 0, bit[1] = 1
    /// </summary>
    /// <example>
    /// The following example shows how to use the <b>BinaryString</b> method.
    /// <code>
    /// <![CDATA[
    /// using Howell;
    /// 
    /// BinaryString binString = BinaryString.Parse("11101");
    /// Boolean[] bits = binString.GetBits();
    /// String info = String.Format("BinString:{0}=", binString.ToString());
    /// for(int i =0;i < bits.Length;++i)
    /// {
    ///     info += bits[i] ? "1" : "0";
    /// }
    /// info += " Bits";
    /// Console.WriteLine(info);
    /// ]]>
    /// </code>
    /// </example>
    public class BinaryString
    {
        private Boolean[] m_Bits;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bits">位值数组值</param>
        public BinaryString(Boolean[] bits)
        {
            m_Bits = bits;
        }
        /// <summary>
        /// 获取位值数组。
        /// </summary>
        /// <returns>返回位值数组。</returns>
        public Boolean[] GetBits()
        {
            return m_Bits;
        }
        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <returns>返回二进制字符串。</returns>
        public override string ToString()
        {
            String result = "";            
            for(Int32 i =0;i < m_Bits.Length;++i)
            {
                result += m_Bits[i] ? "1" : "0";
            }
            return result;
        }
        /// <summary>
        /// 解析2进制字符串
        /// </summary>
        /// <param name="binString">2进制字符串实例对象。</param>
        /// <returns>返回具体的2进制字符串类型</returns>
        public static BinaryString Parse(String binString)
        {
            Condition.WithExceptionOnFailure<FormatException>().Requires(binString, "binString").MatchPattern(@"^[01]*$", "match pattern failed.");
            List<Boolean> booleans = new List<Boolean>();
            for (int i = 0; i < binString.Length; ++i)
            {
                booleans.Add((binString.Substring(i, 1) == "0") ? false : true);
            }
            return new BinaryString(booleans.ToArray());
        }
    }
}
