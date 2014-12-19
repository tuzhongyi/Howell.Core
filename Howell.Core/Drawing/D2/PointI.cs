using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Howell.Math.Algorithm;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 点
    /// </summary>
    public struct PointI : IEquatable<PointI>,IComparable<PointI>,IComparable
    {        
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{X=(?<X>[+-]?\d+),Y=(?<Y>[+-]?\d+)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly PointI Empty = new PointI(0,0);


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X轴</param>
        /// <param name="y">Y轴</param>
        public PointI(Int32 x, Int32 y)
        {
            m_X = x;
            m_Y = y;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == PointI.Empty);
            }
        }

        private Int32 m_X;
        private Int32 m_Y;
        /// <summary>
        /// X轴坐标
        /// </summary>
        public Int32 X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        /// <summary>
        /// Y轴坐标
        /// </summary>
        public Int32 Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }


        /// <summary>
        /// 偏移点的坐标
        /// </summary>
        /// <param name="dx">偏移的X轴值</param>
        /// <param name="dy">偏移的Y轴值</param>
        public void Offset(Int32 dx, Int32 dy)
        {
            this.X += dx;
            this.Y += dy;
        }
        /// <summary>
        /// 偏移点的坐标
        /// </summary>
        /// <param name="p">偏移的坐标</param>
        public void Offset(PointI p)
        {
            this.Offset(p.X, p.Y);
        }
        /// <summary>
        /// 偏移点的坐标
        /// </summary>
        /// <param name="size">偏移的大小</param>
        public void Offset(SizeI size)
        {
            this.Offset(size.Width, size.Height);
        }

        #region operators
        /// <summary>
        /// 解析点的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回PointI类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static PointI Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(PointI).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new PointI(Int32.Parse(collection[0].Groups["X"].Value), Int32.Parse(collection[0].Groups["Y"].Value));
        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>返回字符串形式</returns>
        public override string ToString()
        {
            return ("{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + "}");
        }
        /// <summary>
        /// Check if this instance of <see cref="PointI"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is PointI) ? (this == (PointI)obj) : false;
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
        /// 隐式转换PointI To PointI
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static implicit operator PointD(PointI point)
        {
            return new PointD(point.X, point.Y);
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
        /// 隐式转换PointI To Point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static explicit operator System.Drawing.Point(PointI point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }


        /// <summary>
        /// equals operator - checks if two point is the same.
        /// </summary>
        /// <param name="p1">PointI object 1.</param>
        /// <param name="p2">PointI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(PointI p1, PointI p2)
        {
            if (p1.X.Equals(p2.X) == false) return false;
            if (p1.Y.Equals(p2.Y) == false) return false;
            return true;
        }

        /// <summary>
        /// equals operator - checks if two point is not the same.
        /// </summary>
        /// <param name="p1">PointI object 1.</param>
        /// <param name="p2">PointI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(PointI p1, PointI p2)
        {
            return !(p1 == p2);
        }


        /// <summary>
        /// 加运算
        /// </summary>
        /// <param name="pt">点</param>
        /// <param name="sz">大小</param>
        /// <returns></returns>
        public static PointI operator +(PointI pt, SizeI sz)
        {
            return new PointI(pt.X + sz.Width, pt.Y + sz.Height);
        }
        /// <summary>
        /// 减运算
        /// </summary>
        /// <param name="pt">点</param>
        /// <param name="sz">大小</param>
        /// <returns></returns>
        public static PointI operator -(PointI pt, SizeI sz)
        {
            return new PointI(pt.X - sz.Width, pt.Y - sz.Height);
        }
        /// <summary>
        /// 减运算
        /// </summary>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <returns></returns>
        public static SizeI operator -(PointI pt1, PointI pt2)
        {
            return new SizeI(pt1.X - pt2.X, pt1.Y - pt2.Y);
        }
        /// <summary>
        /// 加运算
        /// </summary>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <returns></returns>
        public static PointI operator +(PointI pt1, PointI pt2)
        {
            return new PointI(pt1.X + pt2.X, pt1.Y + pt2.Y);
        }
        #endregion


        #region IEquatable<PointI> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PointI other)
        {
            return (this == other);
        }

        #endregion

        #region IComparable<PointD> 成员
        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PointI other)
        {
            Double factor = 1;
            if ((this.X - other.X) < 0)
            {
                factor = -1;
            }
            if ((this.X - other.X) == 0)
            {
                if ((this.Y - other.Y) < 0)
                {
                    factor = -1;
                }
            }
            Double result = factor * PointAlgorithm.Distance(this, other);
            if (factor < 0) return (Int32)System.Math.Floor(result);
            else return (Int32)System.Math.Ceiling(result);
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
            return CompareTo((PointI)obj);
        }

        #endregion

    }
}
