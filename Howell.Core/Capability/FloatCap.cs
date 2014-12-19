using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Howell.Capability
{
    /// <summary>
    /// Single 能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class FloatCap : Capabilities
    {
        private Double m_Value = default(Double);
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator Double(FloatCap val)
        {
            return val.Value;
        }
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator FloatCap(Double val)
        {
            return new FloatCap(val);
        }
        /// <summary>
        /// 创建 FloatCap对象
        /// </summary>
        public FloatCap()
            : base()
        {
            TextSpecified = false;
        }
        /// <summary>
        /// 创建 FloatCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public FloatCap(Double val)
            : base()
        {
            TextSpecified = false;
            Value = val;
        }
        /// <summary>
        /// 数值
        /// </summary>
        [XmlIgnoreAttribute()]
        public virtual Double Value
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
            get { return XmlConvert.ToString(Value); }
            set { Value = XmlConvert.ToSingle(value); }
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
