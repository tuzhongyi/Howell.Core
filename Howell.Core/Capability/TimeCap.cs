using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Howell.Capability
{
    /// <summary>
    /// Time 能力
    /// </summary>
    /// <remarks>Format xs:time : 17:30:08+08:00 </remarks>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class TimeCap : Capabilities
    {
        private DateTime m_Value = default(DateTime);
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator DateTime(TimeCap val)
        {
            return val.Value;
        }
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator TimeCap(DateTime val)
        {
            return new TimeCap(val);
        }
        /// <summary>
        /// 创建 TimeCap对象
        /// </summary>
        public TimeCap()
            : this(DateTime.Now)
        {
        }
        /// <summary>
        /// 创建 TimeCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public TimeCap(DateTime val)
            : base()
        {
            TextSpecified = false;
            Value = val;
        }
        /// <summary>
        /// 数值
        /// </summary>
        [XmlIgnoreAttribute()]
        public virtual DateTime Value
        {
            set { m_Value = value; TextSpecified = true; }
            get { return m_Value; }
        }
        /// <summary>
        /// 是否包含Text XML属性
        /// </summary>
        [XmlIgnoreAttribute()]
        public virtual Boolean TextSpecified { set; get; }
        /// <summary>
        /// 文本内容
        /// </summary>
        [XmlTextAttribute()]
        public virtual String Text
        {
            get { return XmlConvert.ToString(Value, XmlDateTimeSerializationMode.RoundtripKind).Substring(11); }
            set { Value = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind); }
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
