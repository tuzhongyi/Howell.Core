using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Howell.Capability
{
    /// <summary>
    /// 枚举Int32
    /// </summary>
    [Serializable]
    public abstract class EnumInt32
    {
         /// <summary>
        /// 数值
        /// </summary>
        protected Int32 m_Value = default(Int32);

        /// <summary>
        /// Constructor
        /// </summary>
        protected EnumInt32()
            : this(0)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="val">数值</param>
        protected EnumInt32(Int32 val)
        {
            TextSpecified = false;
            this.Value = val;
        }
        /// <summary>
        /// 数值
        /// </summary>
        [XmlIgnoreAttribute()]
        protected Int32 Value
        {
            set { m_Value = value; TextSpecified = true; }
            get { return m_Value; }
        }
        /// <summary>
        /// 是否包含Text XML属性
        /// </summary>
        [XmlIgnoreAttribute()]
        public Boolean TextSpecified { set; get; }
        /// <summary>
        /// 文本内容
        /// </summary>
        [XmlTextAttribute()]
        public String Text
        {
            get { return XmlConvert.ToString(Value); }
            set { Value = XmlConvert.ToInt32(value); }
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
        {
            return Text.ToString();
        }
    }
}
