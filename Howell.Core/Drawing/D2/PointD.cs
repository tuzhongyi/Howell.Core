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
    public struct PointD : IEquatable<PointD>,IComparable<PointD>,IComparable
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{X=(?<X>[+-]?(?:\d+)(?:\.\d+)?),Y=(?<Y>[+-]?(?:\d+)(?:\.\d+)?)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly PointD Empty = new PointD(0,0);


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X轴</param>
        /// <param name="y">Y轴</param>
        public PointD(Double x,Double y)
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
                return (this == PointD.Empty);
            }
        }

        private Double m_X;
        private Double m_Y;
        /// <summary>
        /// X轴坐标
        /// </summary>
        public Double X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        /// <summary>
        /// Y轴坐标
        /// </summary>
        public Double Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }


        /// <summary>
        /// 偏移点的坐标
        /// </summary>
        /// <param name="dx">偏移的X轴值</param>
        /// <param name="dy">偏移的Y轴值</param>
        public void Offset(Double dx, Double dy)
        {
            this.X += dx;
            this.Y += dy;
        }
        /// <summary>
        /// 偏移点的坐标
        /// </summary>
        /// <param name="point">偏移的坐标</param>
        public void Offset(PointD point)
        {
            this.Offset(point.X, point.Y);
        }
        /// <summary>
        /// 偏移点的坐标
        /// </summary>
        /// <param name="size">偏移的大小</param>
        public void Offset(SizeD size)
        {
            this.Offset(size.Width, size.Height);
        }


        #region operators
        /// <summary>
        /// 解析点的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回PointD类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static PointD Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(PointD).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new PointD(Double.Parse(collection[0].Groups["X"].Value), Double.Parse(collection[0].Groups["Y"].Value));
        }
        /// <summary>
        /// 获取点的字符串形式
        /// </summary>
        /// <returns>返回字符串形式</returns>
        public override string ToString()
        {
            return ("{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + "}");
        }
        /// <summary>
        /// Check if this instance of <see cref="PointD"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is PointD) ? (this == (PointD)obj) : false;
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
        /// 隐式转换PointD To PointI
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static explicit operator PointI(PointD point)
        {
            return new PointI((Int32)point.X, (Int32)point.Y);
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
        /// 隐式转换PointD To PointF
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static explicit operator System.Drawing.PointF(PointD point)
        {
            return new System.Drawing.PointF((float)point.X, (float)point.Y);
        }

        /// <summary>
        /// equals operator - checks if two point is the same.
        /// </summary>
        /// <param name="p1">PointD object 1.</param>
        /// <param name="p2">PointD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(PointD p1, PointD p2)
        {
            if (DoubleAlgorithm.Equals(p1.X,p2.X) == false) return false;
            if (DoubleAlgorithm.Equals(p1.Y,p2.Y) == false) return false;
            return true;
        }

        /// <summary>
        /// equals operator - checks if two point is not the same.
        /// </summary>
        /// <param name="p1">PointD object 1.</param>
        /// <param name="p2">PointD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(PointD p1, PointD p2)
        {
            return !(p1 == p2);
        }
        

        /// <summary>
        /// 加运算
        /// </summary>
        /// <param name="pt">点</param>
        /// <param name="sz">大小</param>
        /// <returns></returns>
        public static PointD operator +(PointD pt, SizeD sz)
        {
            return new PointD(pt.X + sz.Width, pt.Y + sz.Height);
        }
        /// <summary>
        /// 减运算
        /// </summary>
        /// <param name="pt">点</param>
        /// <param name="sz">大小</param>
        /// <returns></returns>
        public static PointD operator -(PointD pt, SizeD sz)
        {
            return new PointD(pt.X - sz.Width, pt.Y - sz.Height);
        }
        /// <summary>
        /// 减运算
        /// </summary>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <returns></returns>
        public static SizeD operator -(PointD pt1, PointD pt2)
        {
            return new SizeD(pt1.X - pt2.X, pt1.Y - pt2.Y);
        }
        /// <summary>
        /// 加运算
        /// </summary>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <returns></returns>
        public static PointD operator +(PointD pt1, PointD pt2)
        {
            return new PointD(pt1.X + pt2.X, pt1.Y + pt2.Y);
        }
        #endregion



        /// <summary>
        /// 向上取整转换
        /// </summary>
        /// <param name="value">PointD对象</param>
        /// <returns>返回PointI对象</returns>
        public static PointI Ceiling(PointD value)
        {
            return new PointI((Int32)System.Math.Ceiling(value.X), (Int32)System.Math.Ceiling(value.Y));

        }
        /// <summary>
        /// 向下取整转换
        /// </summary>
        /// <param name="value">PointD对象</param>
        /// <returns>返回PointI对象</returns>
        public static PointI Truncate(PointD value)
        {
            return new PointI((Int32)value.X, (Int32)value.Y);
        }
        /// <summary>
        /// 四舍五入取整转换
        /// </summary>
        /// <param name="value">PointD对象</param>
        /// <returns>返回PointI对象</returns>
        public static PointI Round(PointD value)
        {
            return new PointI((Int32)System.Math.Round(value.X), (Int32)System.Math.Round(value.Y));
        }

        #region IEquatable<PointD> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PointD other)
        {
            return (this == other);
        }
        #endregion

        #region IComparable<PointD> 成员
        /// <summary>
        /// CompareTo X1>X2 >0 Or （X1=X2 And Y1>Y2 >0）
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PointD other)
        {
            Double factor = 1;
            if((this.X - other.X) < 0)
            {
                factor = -1;
            }
            if((this.X - other.X) == 0)
            {
                if((this.Y - other.Y) < 0)
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
            return CompareTo((PointD)obj);
        }

        #endregion
    }
}
