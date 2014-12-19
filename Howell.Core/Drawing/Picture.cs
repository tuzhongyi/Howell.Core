using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Howell.Drawing
{
    /// <summary>
    /// Windows标准图片对象
    /// </summary>
    [Serializable]
    public class Picture
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Image m_Image;

 
        #region Constructors
        /// <summary>
        /// 创建Windows标准图片对象
        /// </summary>
        /// <param name="bytes">图片的二进制数据</param>
        public Picture(byte[] bytes)
        {
            //called by NHibernate
            try
            {
                using (var ms = new MemoryStream(bytes))
                {
                    m_Image = Image.FromStream(ms);
                }
            }
            catch (ArgumentException) //parameter is not valid
            {
                //has 78-byte OLE header... a Northwind thing :(
                using (var ms = new MemoryStream(bytes, 78, bytes.Length - 78))
                {
                    m_Image = Image.FromStream(ms);
                }
            }            
            ImageFormat = ImageFormat.Bmp; //default
        }
        /// <summary>
        /// 创建Windows标准图片对象
        /// </summary>
        /// <param name="image">图片对象</param>
        /// <param name="format">图片格式</param>
        public Picture(Image image, ImageFormat format)
        {
            //construct as simple decorator with format information
            m_Image = image;
            ImageFormat = format;
        }
        #endregion
        #region Properties
        /// <summary>
        /// 图片对象
        /// </summary>
        public Image Image  {get { return m_Image; }}
        /// <summary>
        /// 图片格式
        /// </summary>
        public ImageFormat ImageFormat { get; set; }
        #endregion
        /// <summary>
        /// 将图片对象转换为可以被存储的二进制数据
        /// </summary>
        /// <returns>返回图片的二进制数据.</returns>
        public byte[] ToArray()
        {
            using (var ms = new MemoryStream())
            {
                this.Image.Save(ms, ImageFormat);
                return ms.ToArray();
            }
        }
        #region Identity
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Image.Equals(obj);
        }
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Image.GetHashCode();
        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Image != null) return this.Image.Width + " *" + this.Image.Height;
            return "";
        }
        #endregion
    }
    
}
