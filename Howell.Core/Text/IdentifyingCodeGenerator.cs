using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Text
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class IdentifyingCodeGenerator
    {
        /// <summary>
        /// 验证码源
        /// </summary>
        public static readonly string[] source ={"0","1","2","3","4","5","6","7","8","9",
            "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z",
            "A","B","C","D","E","F","G","H","J","K","M","N","P","Q","R","S","T","U","V","W","X","Y","Z"};
        /// <summary>
        /// 创建验证码生成器
        /// </summary>
        public IdentifyingCodeGenerator()
            : this(6)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        public IdentifyingCodeGenerator(Int32 length)
        {
            this.Length = length;
            m_Random = new Random(Environment.TickCount);
        }
        private Random m_Random;

        /// <summary>
        /// 验证码长度
        /// </summary>
        public Int32 Length { get; private set; }
        /// <summary>
        /// 获取下一个随机的验证码
        /// </summary>
        /// <returns>返回验证码字符串</returns>
        public String Next()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < this.Length; i++)
            {
                int index = m_Random.Next(0, source.Length - 1);
                s.Append(source[index]);
            }
            return s.ToString();
        }
    }
        
}
