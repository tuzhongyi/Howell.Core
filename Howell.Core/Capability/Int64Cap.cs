using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Howell.Capability
{
    /// <summary>
    /// Integer 能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class Int64Cap : Capabilities
    {
        private Int64 m_Value = default(Int64);
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator Int64(Int64Cap val)
        {
            return val.Value;
        }
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator Int64Cap(Int64 val)
        {
            return new Int64Cap(val);
        }
        /// <summary>
        /// 创建 IntegerCap对象
        /// </summary>
        public Int64Cap()
            : this(0)
        {
        }
        /// <summary>
        /// 创建 IntegerCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public Int64Cap(Int64 val)
            : this(val, null, null, null)
        {
            Value = val;
        }
        /// <summary>
        /// 创建 IntegerCap对象
        /// </summary>
        /// <param name="val">数值</param>
        /// <param name="def"></param>
        public Int64Cap(Int64 val,Nullable<Int64> def)
            : this(val, def, null, null)
        {
            Value = val;
        }
        /// <summary>
        /// 创建 IntegerCap对象
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Int64Cap(Int64 val,Nullable<Int64> def,Nullable<Int64> min,Nullable<Int64> max)
            : base()
        {
            this.Value = val;
            this.def = (def != null) ? def.ToString() : "0";
            this.defSpecified = (def != null);
            this.min = min ?? 0;
            this.minSpecified = (min != null);
            this.max = max ?? 0;
            this.maxSpecified = (max != null);
        }
        /// <summary>
        /// 数值
        /// </summary>
        [XmlIgnoreAttribute()]
        public virtual Int64 Value
        {
            get { return m_Value; }
            set { m_Value = value; TextSpecified = true; }
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
            set { Value = XmlConvert.ToInt64(value); }
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
