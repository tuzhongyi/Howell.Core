using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Media
{
    /// <summary>
    /// 视频编码格式
    /// </summary>
    public enum VideoCodedFormat
    {
        /// <summary>
        /// 无视频
        /// </summary>
        None = 0,
        /// <summary>
        /// H.264编码
        /// </summary>
        H264 = 1,
        /// <summary>
        /// Motion Jpeg编码
        /// </summary>
        MJPGE = 2,
        /// <summary>
        /// Mpge-4编码
        /// </summary>
        MPEG4 = 3,
    }    
    /// <summary>
    /// 视频格式信息
    /// </summary>
    public class VideoFormatExtensible
    {
        /// <summary>
        /// 视频编码格式
        /// </summary>
        public VideoCodedFormat FormatTag;
    }
}
