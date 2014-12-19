using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 网络输入媒体流协议类型
    /// </summary>
    public enum NetworkInputStreamProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = 1,
        /// <summary>
        /// Onvif协议
        /// </summary>
        [XmlEnumAttribute("Onvif")]
        Onvif = 2,
        /// <summary>
        /// Rtsp协议
        /// </summary>
        [XmlEnumAttribute("Rtsp")]
        Rtsp = 3,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = 4,
        /// <summary>
        /// 海康威视
        /// </summary>
        [XmlEnumAttribute("Hikvision")]
        Hikvision = 5,
    }
}
