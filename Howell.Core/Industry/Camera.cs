using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{

    /// <summary>
    /// 摄像机类型
    /// </summary>
    public enum CameraType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 枪机
        /// </summary>
        [XmlEnumAttribute("Gun")]
        Gun = 1,
        /// <summary>
        /// 球
        /// </summary>
        [XmlEnumAttribute("Ball")]
        Ball = 2,
        /// <summary>
        /// 半球
        /// </summary>
        [XmlEnumAttribute("HalfBall")]
        HalfBall = 3,
        /// <summary>
        /// 一体机
        /// </summary>
        [XmlEnumAttribute("AIO")]
        AIO = 4,
    }
    /// <summary>
    /// 红外IR-CUT状态
    /// </summary>
    public enum IRCutState
    {
        /// <summary>
        /// 普通滤光模式
        /// </summary>
        [XmlEnumAttribute("OFF")]
        OFF = 0,
        /// <summary>
        /// 红外滤光模式
        /// </summary>
        [XmlEnumAttribute("ON")]
        ON = 1,
    }
    /// <summary>
    /// 摄像机或视频源信号状态
    /// </summary>
    public enum SignalState
    {
        /// <summary>
        /// 正常信息
        /// </summary>
        [XmlEnum("Normal")]
        Normal = 0,
        /// <summary>
        /// 信号中断
        /// </summary>
        [XmlEnum("Interrupted")]
        Interrupted = 1,
    }
}
