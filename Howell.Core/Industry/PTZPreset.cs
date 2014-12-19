using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 预制点设置
    /// </summary>
    public enum PTZPreset
    {
        /// <summary>
        /// 清除
        /// </summary>
        [XmlEnum("Clear")]
        Clear = 0,
        /// <summary>
        /// 设置
        /// </summary>
        [XmlEnum("Set")]
        Set = 1,
        /// <summary>
        /// 调用
        /// </summary>
        [XmlEnum("Goto")]
        Goto = 2,
    }
}
