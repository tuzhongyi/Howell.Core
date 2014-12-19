using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Linq;

namespace Howell.Capability
{
    /// <summary>
    /// String 能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class EnumStringCap : Capabilities
    {
        private String m_Value = default(String);
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator String(EnumStringCap val)
        {
            return val.Value;
        }
        ///// <summary>
        ///// 显式转换
        ///// </summary>
        ///// <param name="val">转换对象</param>
        ///// <returns>返回值</returns>
        //public static explicit operator EnumStringCap(String val)
        //{
        //    return new EnumStringCap(val);
        //}
        ///// <summary>
        ///// 创建 EnumStringCap对象
        ///// </summary>
        //public EnumStringCap()
        //    : this(String.Empty)
        //{
        //}
        ///// <summary>
        ///// 创建 EnumStringCap对象
        ///// </summary>
        ///// <param name="val">数值</param>
        //public EnumStringCap(String val)
        //    : this(val, null)
        //{
        //    this.Value = val;
        //}
        /// <summary>
        /// 创建 EnumStringCap对象
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <param name="options"></param>
        public EnumStringCap(String val, String def, params String[] options)
            : base()
        {
            this.options = options;
            this.Value = val;
            this.def = def;
            this.defSpecified = (def != null);
        }
        /// <summary>
        /// 拷贝构造
        /// </summary>
        /// <param name="val"></param>
        public EnumStringCap(EnumStringCap val)
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
            set 
            {
                //if(value == null)
                //{
                //    throw new ArgumentNullException("Can not set EnumStringCap.Value as null.");
                //}
                //if(this.options == null || this.options.Contains(value) == false)
                //{
                //    throw new ArgumentException(String.Format("EnumStringCap.Value {0} is not in options.", value));
                //}
                m_Value = value; TextSpecified = true; 
            }
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
