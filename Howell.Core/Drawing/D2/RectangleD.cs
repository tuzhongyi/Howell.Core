using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 矩形
    /// </summary>
    public struct RectangleD : IEquatable<RectangleD>, IEnumerable<LineD>
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{X=(?<X>[+-]?(?:\d+)(?:\.\d+)?),Y=(?<Y>[+-]?(?:\d+)(?:\.\d+)?),Width=(?<Width>[+-]?(?:\d+)(?:\.\d+)?),Height=(?<Height>[+-]?(?:\d+)(?:\.\d+)?)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly RectangleD Empty = new RectangleD(0,0,0,0);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">位置</param>
        /// <param name="size">大小</param>
        public RectangleD(PointD location, SizeD size)
            : this(location.X, location.Y, size.Width, size.Height)
        {
            
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">左上角顶点X</param>
        /// <param name="y">左上角顶点Y</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public RectangleD(Double x, Double y, Double width, Double height)
        {
            m_X = x;
            m_Y = y;
            m_Width = width;
            m_Height = height;
        }
        #region property
        private Double m_Width;
        private Double m_Height;
        private Double m_X;
        private Double m_Y;
        /// <summary>
        /// 左上X值
        /// </summary>
        public Double Left
        {
            get { return m_X; }
        }
        /// <summary>
        /// 左上Y值
        /// </summary>
        public Double Top
        {
            get { return m_Y; }
        }
        /// <summary>
        /// 右下X值
        /// </summary>
        public Double Right
        {
            get { return m_X + m_Width; }
        }
        /// <summary>
        /// 右下Y值
        /// </summary>
        public Double Bottom
        {
            get { return m_Y + m_Height; }
        }

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
        /// <summary>
        /// 左上角X轴坐标
        /// </summary>
        public Double X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        /// <summary>
        /// 左上角Y轴坐标
        /// </summary>
        public Double Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
        /// <summary>
        /// 矩形大小
        /// </summary>
        public SizeD Size
        {
            get
            {
                return new SizeD(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
        /// <summary>
        /// 矩形位置
        /// </summary>
        public PointD Location
        {
            get
            {
                return new PointD(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        #endregion

        #region operator
        /// <summary>
        /// 解析矩形的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回RectangleD类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static RectangleD Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(RectangleD).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new RectangleD(Double.Parse(collection[0].Groups["X"].Value), Double.Parse(collection[0].Groups["Y"].Value),Double.Parse(collection[0].Groups["Width"].Value),Double.Parse(collection[0].Groups["Height"].Value));
        }
        /// <summary>
        /// 获取矩形的字符串形式
        /// </summary>
        /// <returns>返回字符串形式</returns>
        public override string ToString()
        {
            return ("{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + ",Width=" + this.Width.ToString(CultureInfo.CurrentCulture) + ",Height=" + this.Height.ToString(CultureInfo.CurrentCulture) + "}");
        }
        /// <summary>
        /// Check if this instance of <see cref="RectangleD"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is RectangleD) ? (this == (RectangleD)obj) : false;
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
        /// equals operator - checks if two rect is the same.
        /// </summary>
        /// <param name="r1">RectangleD object 1.</param>
        /// <param name="r2">RectangleD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(RectangleD r1, RectangleD r2)
        {
            if (r1.Size.Equals(r2.Size) == false) return false;
            if (r1.Location.Equals(r2.Location) == false) return false;
            return true;
        }

        /// <summary>
        /// equals operator - checks if two rect is not the same.
        /// </summary>
        /// <param name="r1">Rectangle2D object 1.</param>
        /// <param name="r2">Rectangle2D object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(RectangleD r1, RectangleD r2)
        {
            return !(r1 == r2);
        }

        /// <summary>
        /// 隐式转换RectangleD To RectangleI
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static explicit operator RectangleI(RectangleD rect)
        {
            return new RectangleI((Int32)rect.X, (Int32)rect.Y, (Int32)rect.Width, (Int32)rect.Height);
        }

        /// <summary>
        /// 隐式转换RectangleD To RectangleF
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static explicit operator System.Drawing.RectangleF(RectangleD rect)
        {
            return new System.Drawing.RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }
        #endregion
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == RectangleD.Empty);
            }
        }
        /// <summary>
        /// 获取顶点
        /// </summary>
        /// <returns>返回顶点</returns>
        public IList<PointD> GetVertexs()
        {
            IList<PointD> result = new List<PointD>();
            result.Add(new PointD(Left, Top));
            result.Add(new PointD(Left, Bottom));
            result.Add(new PointD(Right, Bottom));
            result.Add(new PointD(Right, Top));
            return result;
        }


        #region IEquatable<RectangleD> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RectangleD other)
        {
            return (this == other);
        }

        #endregion

        #region IEnumerable<LineD> 成员
        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<LineD> GetEnumerator()
        {
            yield return new LineD(new PointD(Left,Top),new PointD(Left,Bottom));
            yield return new LineD(new PointD(Left, Bottom), new PointD(Right, Bottom));
            yield return new LineD(new PointD(Right, Bottom), new PointD(Right,Top));
            yield return new LineD(new PointD(Right, Top), new PointD(Left,Top));
        }

        #endregion

        #region IEnumerable 成员
        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
