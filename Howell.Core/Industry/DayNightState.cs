using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 摄像机彩转黑白模式
    /// </summary>
    public enum DayNightState
    {
        /// <summary>
        /// 彩色模式
        /// </summary>
        [XmlEnumAttribute("OFF")]
        OFF = 0,
        /// <summary>
        /// 黑白模式
        /// </summary>
        [XmlEnumAttribute("ON")]
        ON = 1,
    }
}
