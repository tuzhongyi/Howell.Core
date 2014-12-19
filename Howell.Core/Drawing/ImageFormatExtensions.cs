using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using Howell;
using System.ComponentModel;

namespace Howell.Drawing.Imaging
{
    /// <summary>
    /// System.Drawing.Imaging.ImageFormat对象的扩展函数
    /// </summary>
    public static class ImageFormatExtensions
    {
        /// <summary>
        /// 将二进制文件数据转换为System.Drawing.Imaging.ImageFormat对象
        /// </summary>
        /// <param name="binaryData">二进制文件数据</param>
        /// <returns>如果成功返回System.Drawing.Imaging.ImageFormat对象，否则将抛出异常。</returns>        
        public static ImageFormat FormBinaryData(Byte [] binaryData)
        {
            String hexString = new HexadecimalString(binaryData, 0, 16).ToString();
            if(hexString.Substring(0,6) == "FFD8FF")
            {
                return ImageFormat.Jpeg;
            }
            else if (hexString.Substring(0, 8) == "89504E47" || hexString.Substring(0, 16) == "89504E470D0A1A0A")
            {
                return ImageFormat.Png;
            }
            else if(hexString.Substring(0,8) == "47494638")
            {
                return ImageFormat.Gif;
            }
            else if (hexString.Substring(0, 8) == "49492A00" || hexString.Substring(0, 8) == "4D4D002A")
            {
                return ImageFormat.Tiff;
            }
            else if (hexString.Substring(0, 4) == "424D")
            {
                return ImageFormat.Bmp;
            }
            else if (hexString.Substring(0,8) == "D7CDC69A" || hexString.Substring(0,8) == "01000900" || hexString.Substring(0,8) == "02000900")
            {
                return ImageFormat.Wmf;
            }
            else if (hexString.Substring(0, 16) == "0100000058000000")
            {
                return ImageFormat.Emf;
            }            
            throw new FormatException("Unknow image file format.");
        }
    }
}
