using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Net
{

    /// <summary>
    /// OS类型
    /// </summary>    
    public enum UserAgentOSType
    {
        /// <summary>
        /// 其他操作系统
        /// </summary>
        [XmlEnum("Other")]
        Other = 0,
        /// <summary>
        /// IPhone OS
        /// </summary>
        [XmlEnum("IOS")]
        IOS = 1,
        /// <summary>
        /// Mac
        /// </summary>
        [XmlEnum("MacOS")]
        MacOS = 2,
        /// <summary>
        /// 安卓
        /// </summary>
        [XmlEnum("Android")]
        Android = 3,
        /// <summary>
        /// Windows Phone 8
        /// </summary>
        [XmlEnum("WP8")]
        WP8 = 4,
        /// <summary>
        /// Windows 7
        /// </summary>
        [XmlEnum("WIN7")]
        WIN7 = 5,
        /// <summary>
        /// Windows 8
        /// </summary>
        [XmlEnum("WIN8")]
        WIN8 = 6,
        /// <summary>
        /// Windows XP
        /// </summary>
        [XmlEnum("WINXP")]
        WINXP = 7,
    }
}
