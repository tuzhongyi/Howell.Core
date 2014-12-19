using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 通道分类
    /// </summary>
    public enum ChannelClassification
    {
        /// <summary>
        /// 非通道
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 音视频源
        /// </summary>
        [XmlEnumAttribute("VideoSource")]
        VideoSource = 1,
        /// <summary>
        /// 视频采集输入
        /// </summary>
        [XmlEnumAttribute("VideoCaptureInput")]
        VideoCaptureInput = 2,
        /// <summary>
        /// 视频采集输出
        /// </summary>
        [XmlEnumAttribute("VideoCaptureOutput")]
        VideoCaptureOutput = 3,
        /// <summary>
        /// 视频网络输入
        /// </summary>
        [XmlEnumAttribute("VideoNetworkInput")]
        VideoNetworkInput = 4,
        /// <summary>
        /// 视频网络输出
        /// </summary>
        [XmlEnumAttribute("VideoNetworkOutput")]
        VideoNetworkOutput = 5,
        /// <summary>
        /// 报警防区
        /// </summary>
        [XmlEnumAttribute("IODefenceZone")]
        IODefenceZone = 6,
        /// <summary>
        /// 报警输入
        /// </summary>
        [XmlEnumAttribute("IOInput")]
        IOInput = 7,
        /// <summary>
        /// 报警输出
        /// </summary>
        [XmlEnumAttribute("IOOutput")]
        IOOutput = 8,
        /// <summary>
        /// 解码
        /// </summary>
        [XmlEnumAttribute("Decoding")]
        Decoding = 9,
        /// <summary>
        /// 存储
        /// </summary>
        [XmlEnumAttribute("Storage")]
        Storage = 10,
        /// <summary>
        /// 视频分析
        /// </summary>
        [XmlEnumAttribute("VideoAnalytics")]
        VideoAnalytics = 11,
        /// <summary>
        /// 网口
        /// </summary>
        [XmlEnumAttribute("NetworkInterface")]
        NetworkInterface = 12,
    }
}
