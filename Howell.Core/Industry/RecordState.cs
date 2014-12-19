using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 录像状态
    /// </summary>
    public enum RecordState
    {
        /// <summary>
        /// 未录像
        /// </summary>
        [XmlEnum("OFF")]
        OFF = 0,
        /// <summary>
        /// 正在录像
        /// </summary>
        [XmlEnum("ON")]
        ON = 1,
    }
}
