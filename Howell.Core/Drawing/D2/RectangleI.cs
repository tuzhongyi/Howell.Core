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
    public struct RectangleI : IEquatable<RectangleI>,IEnumerable<LineI>
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{X=(?<X>[+-]?\d+),Y=(?<Y>[+-]?\d+),Width=(?<Width>[+-]?\d+),Height=(?<Height>[+-]?\d+)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly RectangleI Empty = new RectangleI(0, 0, 0, 0);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">位置</param>
        /// <param name="size">大小</param>
        public RectangleI(PointI location, SizeI size)
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
        public RectangleI(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            m_X = x;
            m_Y = y;
            m_Width = width;
            m_Height = height;
        }

        #region property

        private Int32 m_Width;
        private Int32 m_Height;
        private Int32 m_X;
        private Int32 m_Y;
        /// <summary>
        /// 左上X值
        /// </summary>
        public Int32 Left
        {
            get { return m_X; }
        }
        /// <summary>
        /// 左上Y值
        /// </summary>
        public Int32 Top
        {
            get { return m_Y; }
        }
        /// <summary>
        /// 右下X值
        /// </summary>
        public Int32 Right
        {
            get { return m_X + m_Width; }
        }
        /// <summary>
        /// 右下Y值
        /// </summary>
        public Int32 Bottom
        {
            get { return m_Y + m_Height; }
        }
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
        /// <summary>
        /// 左上角X轴坐标
        /// </summary>
        public Int32 X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        /// <summary>
        /// 左上角Y轴坐标
        /// </summary>
        public Int32 Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
        /// <summary>
        /// 矩形大小
        /// </summary>
        public SizeI Size
        {
            get
            {
                return new SizeI(Width, Height);
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
        public PointI Location
        {
            get
            {
                return new PointI(X, Y);
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
        /// <returns>如果解析成功，返回RectangleI类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static RectangleI Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(RectangleI).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new RectangleI(Int32.Parse(collection[0].Groups["X"].Value), Int32.Parse(collection[0].Groups["Y"].Value), Int32.Parse(collection[0].Groups["Width"].Value), Int32.Parse(collection[0].Groups["Height"].Value));
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
        /// Check if this instance of <see cref="RectangleI"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is RectangleI) ? (this == (RectangleI)obj) : false;
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
        /// <param name="r1">RectangleI object 1.</param>
        /// <param name="r2">RectangleI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(RectangleI r1, RectangleI r2)
        {
            if (r1.Size.Equals(r2.Size) == false) return false;
            if (r1.Location.Equals(r2.Location) == false) return false;
            return true;
        }

        /// <summary>
        /// equals operator - checks if two rect is not the same.
        /// </summary>
        /// <param name="r1">RectangleI object 1.</param>
        /// <param name="r2">RectangleI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(RectangleI r1, RectangleI r2)
        {
            return !(r1 == r2);
        }

        /// <summary>
        /// 隐式转换RectangleD To RectangleI
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static implicit operator RectangleD(RectangleI rect)
        {
            return new RectangleD(rect.X, rect.Y, rect.Width, rect.Height);
        }
        /// <summary>
        /// 隐式转换RectangleI To System.Drawing.Rectangle
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static explicit operator System.Drawing.Rectangle(RectangleI rect)
        {            
            return new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
        #endregion

        /// <summary>
        /// 获取顶点
        /// </summary>
        /// <returns>返回顶点</returns>
        public IList<PointI> GetVertexs()
        {
            IList<PointI> result = new List<PointI>();
            result.Add(new PointI(Left, Top));
            result.Add(new PointI(Left, Bottom));
            result.Add(new PointI(Right, Bottom));
            result.Add(new PointI(Right, Top));
            return result;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == RectangleI.Empty);
            }
        }
        

        #region IEquatable<RectangleI> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RectangleI other)
        {
            return (this == other);
        }

        #endregion

        #region IEnumerable<LineI> 成员
        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<LineI> GetEnumerator()
        {
            yield return new LineI(new PointI(Left, Top), new PointI(Left, Bottom));
            yield return new LineI(new PointI(Left, Bottom), new PointI(Right, Bottom));
            yield return new LineI(new PointI(Right, Bottom), new PointI(Right, Top));
            yield return new LineI(new PointI(Right, Top), new PointI(Left, Top));
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
