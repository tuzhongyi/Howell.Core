using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{

    /// <summary>
    /// 报警探头类型
    /// </summary>
    public enum AlarmInProbeType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 紧急按钮
        /// </summary>
        [XmlEnumAttribute("Panic")]
        Panic = 1,
        /// <summary>
        /// 周界报警
        /// </summary>
        [XmlEnumAttribute("Perimeter")]
        Perimeter = 2,
    }
}
