using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell
{
    /// <summary>
    /// Unix 时间
    /// </summary>
    public struct UnixTime : IComparable,IComparable<UnixTime>
    {
        /// <summary>
        /// 起点时间
        /// </summary>
        public readonly static DateTime Origin = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private DateTime _value;
        /// <summary>
        /// 现在的Unix时间
        /// </summary>
        public static UnixTime Now
        {
            get { return new UnixTime(DateTime.Now); }
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public UnixTime(DateTime value)
        {
            _value = value;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public UnixTime(UInt32 value)
        {
            _value = Origin.AddSeconds(value);
        }
        /// <summary>
        /// ToUInt32
        /// </summary>
        /// <returns></returns>
        public UInt32 ToUInt32()
        {
            return Convert.ToUInt32((_value - Origin).TotalSeconds);
        }
        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return _value;
        }
        #region IComparable 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return -1;
            if((obj is UnixTime)==false)
            {
                return -1;
            }
            return this._value.CompareTo(((UnixTime)obj)._value);
        }

        #endregion
        #region IComparable<UnixTime> 成员
        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(UnixTime other)
        {
            return this._value.CompareTo(other._value);
        }
        #endregion
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._value.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            return this._value.Equals(((UnixTime)obj)._value);
        }
    }
}
