using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Net
{
    /// <summary>
    /// 网络类型
    /// </summary>
    public enum WirelessNetworkType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [XmlEnum("Other")]
        Other = 0,
        /// <summary>
        /// WiFi无线网络
        /// </summary>
        [XmlEnum("WiFi")]
        WiFi = 1,
        /// <summary>
        /// 3G网络
        /// </summary>
        [XmlEnum("4G")]
        ThreeG = 2,
        /// <summary>
        /// 4G网络
        /// </summary>
        [XmlEnum("3G")]
        FourG = 3,

    }
}
