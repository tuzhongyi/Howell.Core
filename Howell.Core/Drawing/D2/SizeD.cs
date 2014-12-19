using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Howell.Math.Algorithm;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 图形大小2D
    /// </summary>
    public struct SizeD : IEquatable<SizeD>, IComparable<SizeD>, IComparable
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{Width=(?<Width>[+-]?(?:\d+)(?:\.\d+)?),Height=(?<Height>[+-]?(?:\d+)(?:\.\d+)?)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly SizeD Empty = new SizeD(0,0);


        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public SizeD(Double width, Double height)
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
                return (this == SizeD.Empty);
            }
        }
        private Double m_Width;
        private Double m_Height;

        /// <summary>
        /// 宽度
        /// </summary>
        public Double Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public Double Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }
        #region operators
        /// <summary>
        /// 解析图形大小的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回SizeD类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static SizeD Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(SizeD).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new SizeD(Double.Parse(collection[0].Groups["Width"].Value), Double.Parse(collection[0].Groups["Height"].Value));
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
        /// Check if this instance of <see cref="SizeD"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is SizeD) ? (this == (SizeD)obj) : false;
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
        public static explicit operator PointD(SizeD size)
        {
            return new PointD(size.Width, size.Height);
        }

        /// <summary>
        /// 隐式转换SizeD To SizeI
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static explicit operator SizeI(SizeD size)
        {
            return new SizeI((Int32)size.Width, (Int32)size.Height);
        }

        /// <summary>
        /// equals operator - checks if two point is the same.
        /// </summary>
        /// <param name="s1">SizeD object 1.</param>
        /// <param name="s2">SizeD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(SizeD s1, SizeD s2)
        {
            if (DoubleAlgorithm.Equals(s1.Width,s2.Width) == false) return false;
            if (DoubleAlgorithm.Equals(s1.Height,s2.Height) == false) return false;
            return true;
        }

        /// <summary>
        /// equals operator - checks if two point is not the same.
        /// </summary>
        /// <param name="s1">SizeD object 1.</param>
        /// <param name="s2">SizeD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(SizeD s1, SizeD s2)
        {
            return !(s1 == s2);
        }

        /// <summary>
        /// 加运算
        /// </summary>
        /// <param name="sz1"></param>
        /// <param name="sz2"></param>
        /// <returns></returns>
        public static SizeD operator +(SizeD sz1, SizeD sz2)
        {
            return new SizeD(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>
        /// 减运算
        /// </summary>
        /// <param name="sz1"></param>
        /// <param name="sz2"></param>
        /// <returns></returns>
        public static SizeD operator -(SizeD sz1, SizeD sz2)
        {
            return new SizeD(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        #endregion

        /// <summary>
        /// 向上取整转换
        /// </summary>
        /// <param name="value">SizeD对象</param>
        /// <returns>返回SizeI对象</returns>
        public static SizeI Ceiling(SizeD value)
        {
            return new SizeI((Int32)System.Math.Ceiling(value.Width), (Int32)System.Math.Ceiling(value.Height));

        }
        /// <summary>
        /// 向下取整转换
        /// </summary>
        /// <param name="value">SizeD对象</param>
        /// <returns>返回SizeI对象</returns>
        public static SizeI Truncate(SizeD value)
        {
            return new SizeI((Int32)value.Width, (Int32)value.Height);
        }

        /// <summary>
        /// 四舍五入取整转换
        /// </summary>
        /// <param name="value">SizeD对象</param>
        /// <returns>返回SizeI对象</returns>
        public static SizeI Round(SizeD value)
        {
            return new SizeI((Int32)System.Math.Round(value.Width), (Int32)System.Math.Round(value.Height));
        }


        #region IEquatable<SizeD> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SizeD other)
        {
            return (this == other);
        }

        #endregion



        #region IComparable<SizeD> 成员
        /// <summary>
        /// CompareTo Width1*Height1 - Width2*Height2
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(SizeD other)
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
            return CompareTo((SizeD)obj);
        }

        #endregion
    }
}
