using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{

    /// <summary>
    /// 网络地址类型
    /// </summary>
    public enum IPVersion
    {
        /// <summary>
        /// IPv4
        /// </summary>
        [XmlEnum("v4")]
        IPv4 = 0,
        /// <summary>
        /// IPv6
        /// </summary>
        [XmlEnum("v6")]
        IPv6 = 1,
        /// <summary>
        /// IPv4+IPv6
        /// </summary>
        [XmlEnum("Dual")]
        Dual = 2,
    }
    /// <summary>
    /// 网络地址获取方式
    /// </summary>
    public enum NetworkAddressingType
    {
        /// <summary>
        /// 静态获取
        /// </summary>
        [XmlEnum("Static")]
        Static = 0,
        /// <summary>
        /// 动态获取 DHCP
        /// </summary>
        [XmlEnum("Dynamic")]
        Dynamic = 1,
        /// <summary>
        /// 自动私有IP地址获取 Automatic Private IP Addressing
        /// </summary>
        [XmlEnum("APIPA")]
        APIPA = 2,
    }
    /// <summary>
    /// 网口线缆类型
    /// </summary>
    public enum NetworkCableType
    {
        /// <summary>
        /// RJ45接口
        /// </summary>
        [XmlEnum("RJ45")]
        RJ45 = 0,
        /// <summary>
        /// 光纤接口
        /// </summary>
        [XmlEnum("Fiber")]
        Fiber = 1,
        /// <summary>
        /// 无线设备
        /// </summary>
        [XmlEnum("Wireless")]
        Wireless = 2,
    }
    /// <summary>
    /// 网口连接速率和双工模式
    /// </summary>
    public enum NetworkSpeedDuplex
    {
        /// <summary>
        /// 自适应
        /// </summary>
        [XmlEnum("Auto")]
        Auto = 0,
        /// <summary>
        /// 10Mbps 半双工
        /// </summary>
        [XmlEnum("Half10MBase")]
        Half10MBase = 1,
        /// <summary>
        /// 10Mbps 全双工
        /// </summary>
        [XmlEnum("Full10MBase")]
        Full10MBase = 2,
        /// <summary>
        /// 100Mbps 半双工
        /// </summary>
        [XmlEnum("Half100MBase")]
        Half100MBase = 3,
        /// <summary>
        /// 100Mbps 全双工
        /// </summary>
        [XmlEnum("Full100MBase")]
        Full100MBase = 4,
        /// <summary>
        /// 1000Mbps 半双工
        /// </summary>
        [XmlEnum("Half1000MBase")]
        Half1000MBase = 5,
        /// <summary>
        /// 1000Mbps 全双工
        /// </summary>
        [XmlEnum("Full1000MBase")]
        Full1000MBase = 6,
    }
    /// <summary>
    /// 网口工作模式
    /// </summary>
    public enum NetworkInterfaceWorkMode
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [XmlEnum("Enable")]
        Disable = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [XmlEnum("Disable")]
        Enable = 1,
        /// <summary>
        /// 桥接或对等
        /// </summary>
        [XmlEnum("Bridge")]
        Bridge = 2,
        /// <summary>
        /// 负载均衡
        /// </summary>
        [XmlEnum("Balancing")]
        Balancing = 3,
    }
}
