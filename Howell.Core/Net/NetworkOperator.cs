using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Net
{

    /// <summary>
    /// 网络运营商
    /// </summary>
    public enum NetworkOperator
    {
        /// <summary>
        /// 其他
        /// </summary>
        [XmlEnum("Other")]
        Other = 0,
        /// <summary>
        /// 中国电信
        /// </summary>
        [XmlEnum("ChinaTelecom")]
        ChinaTelecom = 1,
        /// <summary>
        /// 中国网通
        /// </summary>
        [XmlEnum("ChinaNetcom")]
        ChinaNetcom = 2,
        /// <summary>
        /// 中国联通
        /// </summary>
        [XmlEnum("ChinaUnicom")]
        ChinaUnicom = 3,
        /// <summary>
        /// 中国移动
        /// </summary>
        [XmlEnum("ChinaMobile")]
        ChinaMobile = 4,
        /// <summary>
        /// ATT 美国电话电报公司
        /// </summary>
        [XmlEnum("ATT")]
        ATT = 5,
    }
}
