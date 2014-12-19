using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{

    /// <summary>
    /// 布防类型
    /// </summary>
    public enum ArmType
    {
        /// <summary>
        /// 撤防
        /// </summary>
        [XmlEnumAttribute("Disarm")]
        Disarm = 0,
        /// <summary>
        /// 布防
        /// </summary>
        [XmlEnumAttribute("Arm")]
        Arm = 1,
        /// <summary>
        /// 旁路
        /// </summary>
        [XmlEnumAttribute("Bypass")]
        Bypass = 2,
    }
}
