using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Howell.Capability
{

    /// <summary>
    /// Boolean 能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class BooleanCap : Capabilities
    {
        private Boolean m_Value = default(Boolean);
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator Boolean(BooleanCap val)
        {
            return val.Value;
        }        
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator BooleanCap(Boolean val)
        {
            return new BooleanCap(val);
        }
        /// <summary>
        /// 创建 BooleanCap对象
        /// </summary>
        public BooleanCap()
            : this(false)
        {
        }            
        /// <summary>
        /// 创建 BooleanCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public BooleanCap(Boolean val)
            : base()
        {
            this.TextSpecified = false;
            this.Value = val;
            this.options = new string[] { Boolean.FalseString, Boolean.TrueString };
        }
        /// <summary>
        /// 拷贝构造
        /// </summary>
        /// <param name="val"></param>
        public BooleanCap(BooleanCap val)
            : base(val)
        {
            this.TextSpecified = val.TextSpecified;
            this.Value = val.Value;
        }
        /// <summary>
        /// 数值
        /// </summary>
        [XmlIgnoreAttribute()]
        public virtual Boolean Value 
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
            set { Value = XmlConvert.ToBoolean(value); }
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
