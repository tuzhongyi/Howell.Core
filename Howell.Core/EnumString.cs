using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Howell
{
    /// <summary>
    /// 枚举字符串信息
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    [Serializable()]
    public class EnumString<T>
    {
        private T m_Value;
        /// <summary>
        /// 默认构造
        /// </summary>
        public EnumString()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">默认枚举值</param>
        public EnumString(T value)
        {
            m_Value = value;
        }
        /// <summary>
        /// 内部XML值类型
        /// </summary>
        [XmlTextAttribute()]
        public T Value
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
        public static implicit operator T(EnumString<T> value)
        {
            return value.m_Value;
        }
        /// <summary>
        /// 枚举类型隐式转换为枚举字符串类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator EnumString<T>(T value)
        {
            return new EnumString<T>(value);
        }
        /// <summary>
        /// 枚举字符串类型隐式转换为字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator String(EnumString<T> value)
        {
            if (value == null) return null;
            return value.ToString();
        }
        /// <summary>
        /// 字符串类型隐式转换为枚举字符串类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator EnumString<T>(String value)
        {
            if (value == null) return null;
            return new EnumString<T>((T)Enum.Parse(typeof(T), value));
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(EnumString<T> left, EnumString<T> right)
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
        public static bool operator !=(EnumString<T> left, EnumString<T> right)
        {
            return !(left == right);
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(EnumString<T> left, T right)
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
        public static bool operator !=(EnumString<T> left, T right)
        {
            return !(left == right);
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(T left, EnumString<T> right)
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
        public static bool operator !=(T left, EnumString<T> right)
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
            return m_Value.ToString();
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
