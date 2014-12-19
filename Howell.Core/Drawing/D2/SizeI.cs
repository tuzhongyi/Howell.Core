using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 大小
    /// </summary>
    public struct SizeI : IEquatable<SizeI>,IComparable,IComparable<SizeI>
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{Width=(?<Width>[+-]?\d+),Height=(?<Height>[+-]?\d+)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly SizeI Empty = new SizeI(0,0);

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public SizeI(Int32 width, Int32 height)
        {
            m_Width = width;
            m_Height = height;
        }

        /// <summary>
        /// 是否为空对象
        /// </summary>
        /// <returns>True表示为空，否则返回False.</returns>
        public Boolean IsEmpty
        {
            get
            {
                return (this == SizeI.Empty);
            }
        }
        private Int32 m_Width;
        private Int32 m_Height;

        /// <summary>
        /// 宽度
        /// </summary>
        public Int32 Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public Int32 Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }
        #region operators
        /// <summary>
        /// 解析图形大小的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回SizeI类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static SizeI Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(SizeI).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new SizeI(Int32.Parse(collection[0].Groups["Width"].Value), Int32.Parse(collection[0].Groups["Height"].Value));
        }
        /// <summary>
        /// 获取图形大小的字符串形式
        /// </summary>
        /// <returns>返回字符串形式</returns>
        public override string ToString()
        {
            return ("{Width=" + this.Width.ToString(CultureInfo.CurrentCulture) + ",Height=" + this.Height.ToString(CultureInfo.CurrentCulture) + "}");
        }
        /// <summary>
        /// Check if this instance of <see cref="SizeI"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is SizeI) ? (this == (SizeI)obj) : false;
        }
        /// <summary>
        /// Get hash code for this instance.
        /// </summary>
        /// <returns>Returns the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 显示转换Size 2 Point
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static explicit operator PointI(SizeI size)
        {
            return new PointI(size.Width, size.Height);
        }

        /// <summary>
        /// 隐式转换SizeD To SizeI
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static implicit operator SizeD(SizeI size)
        {
            return new SizeD(size.Width, size.Height);
        }

        /// <summary>
        /// equals operator - checks if two point is the same.
        /// </summary>
        /// <param name="s1">SizeI object 1.</param>
        /// <param name="s2">SizeI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(SizeI s1, SizeI s2)
        {
            if (s1.Width.Equals(s2.Width) == false) return false;
            if (s1.Height.Equals(s2.Height) == false) return false;
            return true;
        }

        /// <summary>
        /// equals operator - checks if two point is not the same.
        /// </summary>
        /// <param name="s1">SizeI object 1.</param>
        /// <param name="s2">SizeI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(SizeI s1, SizeI s2)
        {
            return !(s1 == s2);
        }

        /// <summary>
        /// 加运算
        /// </summary>
        /// <param name="sz1"></param>
        /// <param name="sz2"></param>
        /// <returns></returns>
        public static SizeI operator +(SizeI sz1, SizeI sz2)
        {
            return new SizeI(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>
        /// 减运算
        /// </summary>
        /// <param name="sz1"></param>
        /// <param name="sz2"></param>
        /// <returns></returns>
        public static SizeI operator -(SizeI sz1, SizeI sz2)
        {
            return new SizeI(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        #endregion

        #region IEquatable<SizeI> 成员

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SizeI other)
        {
            return (this == other);
        }

        #endregion


        #region IComparable<SizeI> 成员
        /// <summary>
        /// CompareTo Width1*Height1 - Width2*Height2
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(SizeI other)
        {
            return Convert.ToInt32(this.Width * this.Height - other.Width * other.Height);
        }

        #endregion

        #region IComparable 成员
        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return CompareTo((SizeI)obj);
        }

        #endregion
    }
}
