using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 保密
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 男性
        /// </summary>
        [XmlEnumAttribute("Male")]
        Male = 1,
        /// <summary>
        /// 女性
        /// </summary>
        [XmlEnumAttribute("Female")]
        Female = 2,
    }
}
