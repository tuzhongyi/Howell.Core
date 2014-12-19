using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Net
{


    /// <summary>
    /// 用户代理客户端类型
    /// </summary>
    public enum UserAgentType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [XmlEnum("Other")]
        Other = 0,
        /// <summary>
        /// 手机
        /// </summary>
        [XmlEnum("CellPhone")]
        CellPhone = 1,
        /// <summary>
        /// 平板
        /// </summary>
        [XmlEnum("Tablet")]
        Tablet = 2,
        /// <summary>
        /// 个人电脑
        /// </summary>
        [XmlEnum("PC")]
        PC = 3,
    }
}
