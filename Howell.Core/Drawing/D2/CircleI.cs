using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 圆
    /// </summary>
    public struct CircleI : IEquatable<CircleI>
    {       
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{X=(?<X>[+-]?\d+),Y=(?<Y>[+-]?\d+),Radius=(?<Radius>[+-]?\d+)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly CircleI Empty = new CircleI(0, 0, 0);
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="radius">半径</param>
        public CircleI(PointI center, Int32 radius)
        {
            m_X = center.X;
            m_Y = center.Y;
            m_Radius = radius;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">中心点X轴坐标</param>
        /// <param name="y">中心点Y轴坐标</param>
        /// <param name="radius">半径</param>
        public CircleI(Int32 x, Int32 y, Int32 radius)
        {
            m_X = x;
            m_Y = y;
            m_Radius = radius;
        }
        
        #region Property
        private Int32 m_Radius;
        private Int32 m_X;
        private Int32 m_Y;
        /// <summary>
        /// 中心点X轴坐标
        /// </summary>
        public Int32 X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        /// <summary>
        /// 中心点Y轴坐标
        /// </summary>
        public Int32 Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
        /// <summary>
        /// 中心点
        /// </summary>
        public PointI Center
        {
            get { return new PointI(X, Y); }
        }
        /// <summary>
        /// 半径
        /// </summary>
        public Int32 Radius
        {
            get { return m_Radius; }
            set { m_Radius = value; }
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == CircleI.Empty);
            }
        }
        #endregion

        #region operator
        /// <summary>
        /// 解析圆形的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回CircleI类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static CircleI Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(CircleI).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new CircleI(Int32.Parse(collection[0].Groups["X"].Value), Int32.Parse(collection[0].Groups["Y"].Value), Int32.Parse(collection[0].Groups["Radius"].Value));
        }
        /// <summary>
        /// 获取圆形的字符串形式
        /// </summary>
        /// <returns>返回圆形字符串形式</returns>
        public override string ToString()
        {
            return ("{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + ",Radius=" + this.Radius.ToString(CultureInfo.CurrentCulture) + "}");

        }
        /// <summary>
        /// Check if this instance of <see cref="CircleI"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is CircleI) ? (this == (CircleI)obj) : false;
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
        /// equals operator - checks if two ellipse is the same.
        /// </summary>
        /// <param name="c1">Circle object 1.</param>
        /// <param name="c2">Circle object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(CircleI c1, CircleI c2)
        {
            if (c1.Radius.Equals(c2.Radius) == false) return false;
            if (c1.X.Equals(c2.X) == false) return false;
            if (c1.Y.Equals(c2.Y) == false) return false;
            return true;
        }

        /// <summary>
        /// equals operator - checks if two ellipse is not the same.
        /// </summary>
        /// <param name="c1">CircleI object 1.</param>
        /// <param name="c2">CircleI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(CircleI c1, CircleI c2)
        {
            return !(c1 == c2);
        }
        /// <summary>
        /// 隐式转换 CircleI To CircleD
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        public static implicit operator CircleD(CircleI circle)
        {
            return new CircleD(circle.X, circle.Y, circle.Radius);
        }
        #endregion

        #region IEquatable<CircleI> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CircleI other)
        {
            return (this == other);
        }

        #endregion
    }
}
