using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Howell
{
    /// <summary>
    /// 日期字符串格式
    /// </summary>
    [Serializable()]
    public class DateTimeString
    {

        private DateTime m_Value;
        /// <summary>
        /// 默认构造
        /// </summary>
        public DateTimeString()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">默认枚举值</param>
        public DateTimeString(DateTime value)
        {
            m_Value = value;
        }
        /// <summary>
        /// 内部XML值类型
        /// </summary>
        [XmlTextAttribute()]
        public DateTime Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
        #region Operators
        /// <summary>
        /// 枚举字符串类型隐式转换为枚举类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator DateTime(DateTimeString value)
        {
            return value.m_Value;
        }
        /// <summary>
        /// 枚举类型隐式转换为枚举字符串类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator DateTimeString(DateTime value)
        {
            return new DateTimeString(value);
        }
        /// <summary>
        /// 枚举字符串类型隐式转换为字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator String(DateTimeString value)
        {
            if (value == null) return null;
            return value.ToString();
        }
        /// <summary>
        /// 字符串类型隐式转换为枚举字符串类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator DateTimeString(String value)
        {
            if (value == null) return null;
            return new DateTimeString(DateTime.Parse(value));            
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DateTimeString left, DateTimeString right)
        {
            if (left as object == null && right as object == null) return true;
            if (left as object == null || right as object == null) return false;
            return left.GetHashCode() == right.GetHashCode();
        }
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DateTimeString left, DateTimeString right)
        {
            return !(left == right);
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DateTimeString left, DateTime right)
        {
            if (left as object == null && right as object == null) return true;
            if (left as object == null || right as object == null) return false;
            return left.GetHashCode() == right.GetHashCode();
        }
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DateTimeString left, DateTime right)
        {
            return !(left == right);
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DateTime left, DateTimeString right)
        {
            if (left as object == null && right as object == null) return true;
            if (left as object == null || right as object == null) return false;
            return left.GetHashCode() == right.GetHashCode();
        }
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DateTime left, DateTimeString right)
        {
            return !(left == right);
        }
        #endregion

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return XmlConvert.ToString(m_Value, XmlDateTimeSerializationMode.Utc);            
        }
        /// <summary>
        /// 获取HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return m_Value.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }
    }
}
