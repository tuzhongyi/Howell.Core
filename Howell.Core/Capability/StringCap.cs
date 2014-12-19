using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Capability
{
    /// <summary>
    /// String 能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class StringCap : Capabilities
    {
        private String m_Value = default(String);
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator String(StringCap val)
        {
            return val.Value;
        }
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator StringCap(String val)
        {
            return new StringCap(val);
        }
        /// <summary>
        /// 创建 StringCap对象
        /// </summary>
        public StringCap()
            : this(String.Empty)
        {
            TextSpecified = false;
        }
        /// <summary>
        /// 创建 StringCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public StringCap(String val)
            : this(val, null)
        {
            this.TextSpecified = false;
            this.Value = val;
        }
        /// <summary>
        /// 创建 StringCap对象
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        public StringCap(String val,String def)
            : this(val, def, null, null)
        {
        }
        /// <summary>
        /// 创建 StringCap对象
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public StringCap(String val, String def, Nullable<Double> min, Nullable<Double> max)
            : base()
        {
            this.Value = val;
            this.def = def;
            this.defSpecified = (def != null);
            this.min = min ?? 0;
            this.minSpecified = (min != null);
            this.max = max ?? 0;
            this.maxSpecified = (max != null);
        }
        /// <summary>
        /// 拷贝构造
        /// </summary>
        /// <param name="val"></param>
        public StringCap(StringCap val)        
            : base(val)
        {
            this.TextSpecified = val.TextSpecified;
            this.Value = val.Value;            
        }        
        /// <summary>
        /// 数值
        /// </summary>
        [XmlIgnoreAttribute()]
        public virtual String Value
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
            get { return (String.IsNullOrEmpty(Value)) ? "" : Value; }
            set { Value = value; }
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
