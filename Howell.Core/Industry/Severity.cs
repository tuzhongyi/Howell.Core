using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 事件的重要性，枚举值的数值越大，越重要
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// 无，或普通
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 普通信息
        /// </summary>
        [XmlEnumAttribute("Information")]
        Information = 0x1,
        /// <summary>
        /// 告警
        /// </summary>
        [XmlEnumAttribute("Warning")]
        Warning = 0x10,
        /// <summary>
        /// 紧急
        /// </summary>
        [XmlEnumAttribute("Emergency")]
        Emergency = 0x40,
    }
}
