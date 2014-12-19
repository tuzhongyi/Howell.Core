using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Security
{
    /// <summary>
    /// 安全激活状态
    /// </summary>
    public enum Activation
    {
        /// <summary>
        /// 默认出厂状态
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 已激活
        /// </summary>
        [XmlEnumAttribute("Activated")]
        Activated = 1,
        /// <summary>
        /// 已冻结
        /// </summary>
        [XmlEnumAttribute("Frozen")]
        Frozen = 2,
        /// <summary>
        /// 过期的
        /// </summary>
        [XmlEnumAttribute("Expired")]
        Expired = 3,
    }
}
