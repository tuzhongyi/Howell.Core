using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Howell.Math.Algorithm;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{

    /// <summary>
    /// 椭圆
    /// </summary>
    public struct EllipseI : IEquatable<EllipseI>
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{X=(?<X>[+-]?\d+),Y=(?<Y>[+-]?\d+),Major=(?<Major>[+-]?\d+),Minor=(?<Minor>[+-]?\d+),Angle=(?<Angle>[+-]?\d+)\}$";

        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly EllipseI Empty = new EllipseI(0,0,0,0,0);
        ///// <summary>
        ///// Constructor
        ///// </summary>
        //private EllipseD()
        //    : this(0,0,0,0)
        //{
        //}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">中心点X</param>
        /// <param name="y">中心点Y</param>
        /// <param name="major">主轴长度</param>
        /// <param name="minor">副轴长度</param>
        public EllipseI(Int32 x, Int32 y, Int32 major, Int32 minor)
            : this(x,y,major,minor,0)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">中心点X</param>
        /// <param name="y">中心点Y</param>
        /// <param name="major">主轴长度</param>
        /// <param name="minor">副轴长度</param>
        /// <param name="angle">椭圆旋转角度</param>
        public EllipseI(Int32 x, Int32 y, Int32 major, Int32 minor, Double angle)
        {
            m_X = x;
            m_Y = y;
            m_Major = major;
            m_Minor = minor;
            m_Angle = angle;
        }
        #region Property
        private Int32 m_X;
        private Int32 m_Y;
        private Int32 m_Major;
        private Int32 m_Minor;
        private Double m_Angle;

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
        /// 主轴长度
        /// </summary>
        public Int32 Major
        {
            get { return m_Major; }
            set { m_Major = value; }
        }
        /// <summary>
        /// 副轴长度
        /// </summary>
        public Int32 Minor
        {
            get { return m_Minor; }
            set { m_Minor = value; }
        }
        /// <summary>
        /// 椭圆旋转角度
        /// </summary>
        public Double Angle
        {
            get { return m_Angle; }
            set { m_Angle = value; }
        }     
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == EllipseI.Empty);
            }
        }
        #endregion



        #region operator
        /// <summary>
        /// 解析椭圆的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回EllipseI类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static EllipseI Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(EllipseI).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new EllipseI(Int32.Parse(collection[0].Groups["X"].Value), Int32.Parse(collection[0].Groups["Y"].Value),
                Int32.Parse(collection[0].Groups["Major"].Value), Int32.Parse(collection[0].Groups["Minor"].Value), Int32.Parse(collection[0].Groups["Angle"].Value));
        }
        /// <summary>
        /// 获取椭圆的字符串形式
        /// </summary>
        /// <returns>返回椭圆字符串形式</returns>
        public override string ToString()
        {
            return ("{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + ",Major=" + this.Major.ToString(CultureInfo.CurrentCulture) + ",Minor=" + this.Minor.ToString(CultureInfo.CurrentCulture) + ",Angle=" + this.Angle.ToString(CultureInfo.CurrentCulture) + "}");
        }
        /// <summary>
        /// Check if this instance of <see cref="EllipseI"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is EllipseI) ? (this == (EllipseI)obj) : false;
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
        /// <param name="e1">Ellipse2D object 1.</param>
        /// <param name="e2">Ellipse2D object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(EllipseI e1, EllipseI e2)
        {
            if (DoubleAlgorithm.Equals(e1.Angle,e2.Angle) == false) return false;
            if (e1.Major.Equals(e2.Major) == false) return false;
            if (e1.Minor.Equals(e2.Minor) == false) return false;
            if (e1.X.Equals(e2.X) == false) return false;
            if (e1.Y.Equals(e2.Y) == false) return false;
            return true;
        }
        /// <summary>
        /// equals operator - checks if two ellipse is not the same.
        /// </summary>
        /// <param name="e1">Ellipse2D object 1.</param>
        /// <param name="e2">Ellipse2D object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(EllipseI e1, EllipseI e2)
        {
            return !(e1 == e2);
        }
        /// <summary>
        /// 隐式转换 EllipseD To EllipseI
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static implicit operator EllipseD(EllipseI ellipse)
        {
            return new EllipseD(ellipse.X, ellipse.Y, ellipse.Major, ellipse.Minor, ellipse.Angle);
        }
        #endregion

        #region IEquatable<EllipseI> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EllipseI other)
        {
            return (other == this);
        }

        #endregion
    }
}
