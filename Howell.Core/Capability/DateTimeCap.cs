using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Howell.Capability
{
    /// <summary>
    /// DateTime 能力
    /// </summary>
    /// <remarks>Format xs:datetime : 2004-05-03T17:30:08+08:00 </remarks>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class DateTimeCap : Capabilities
    {
        private DateTime m_Value = default(DateTime);
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator DateTime(DateTimeCap val)
        {
            return val.Value;
        }
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator DateTimeCap(DateTime val)
        {
            return new DateTimeCap(val);
        }
        /// <summary>
        /// 创建 DateTimeCap对象
        /// </summary>
        public DateTimeCap()
            : this(DateTime.Now)
        {
        }
        /// <summary>
        /// 创建 DateTimeCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public DateTimeCap(DateTime val) 
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
            get { return XmlConvert.ToString(Value, XmlDateTimeSerializationMode.RoundtripKind); }
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
