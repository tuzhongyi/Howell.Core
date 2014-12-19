using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 视频编码格式
    /// </summary>
    public enum VideoCodecType
    {
        /// <summary>
        /// None
        /// </summary>
        [XmlEnum("None")]
        None = 0,
        /// <summary>
        /// H.264
        /// </summary>
        [XmlEnum("H264")]
        H264 = 1,
        /// <summary>
        /// Motion Jpeg
        /// </summary>
        [XmlEnum("MJPEG")]
        MJPEG = 2,
        /// <summary>
        /// Mpeg-4
        /// </summary>
        [XmlEnum("MPEG4")]
        MPEG4 = 3,
    }
    /// <summary>
    /// 视频码率控制类型
    /// </summary>
    public enum VideoQualityControlType
    {
        /// <summary>
        /// 变码率 (Variable Bitrate)
        /// </summary>
        [XmlEnum("VBR")]
        VBR = 0,
        /// <summary>
        /// 定码率 (Constant Bitrate)
        /// </summary>
        [XmlEnum("CBR")]
        CBR = 1,
    }
}
