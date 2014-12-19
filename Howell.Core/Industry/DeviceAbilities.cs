using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 设备基本能力
    /// </summary>
    [Flags]
    public enum DeviceAbilities : int
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnum("None")]
        None= 0x00,
        /// <summary>
        /// 无线设备
        /// </summary>
        [XmlEnum("Wireless")]
        Wireless = 0x01,
        /// <summary>
        /// 红外设备
        /// </summary>
        [XmlEnum("Infrared")]
        Infrared = 0x02,
        /// <summary>
        /// 温度探测设备
        /// </summary>
        [XmlEnum("Temperature")]
        Temperature = 0x04,
        /// <summary>
        /// 压力传感设备
        /// </summary>
        [XmlEnum("Pressure")]
        Pressure = 0x08,
        /// <summary>
        /// 存储设备
        /// </summary>
        [XmlEnum("Storable")]
        Storable = 0x10,
    }
}
