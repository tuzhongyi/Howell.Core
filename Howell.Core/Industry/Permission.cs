using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{

    /// <summary>
    /// 权限枚举类型
    /// </summary>
    [Flags()]
    public enum DevicePermissions : uint
    {
        /// <summary>
        /// 无权限
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 系统控制，包括重启，恢复出厂值，网络参数等
        /// </summary>
        [XmlEnumAttribute("System")]
        System = 0x01,
        /// <summary>
        /// 视频源参数设置
        /// </summary>
        [XmlEnumAttribute("Media")]
        Media = 0x02,
        /// <summary>
        /// 日志查询功能
        /// </summary>
        [XmlEnumAttribute("Logs")]
        Logs = 0x04,
        /// <summary>
        /// 完全控制权限
        /// </summary>
        [XmlEnumAttribute("All")]
        All = UInt32.MaxValue,
    }
    /// <summary>
    /// 权限枚举类型
    /// </summary>
    [Flags()]
    public enum VideoSourcePermissions : uint
    {
        /// <summary>
        /// 无权限
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 预览权限
        /// </summary>
        [XmlEnumAttribute("Preview")]
        Preview = 0x01,
        /// <summary>
        /// 回放权限
        /// </summary>
        [XmlEnumAttribute("Playback")]
        Playback = 0x02,
        /// <summary>
        /// 云台控制权限
        /// </summary>
        [XmlEnumAttribute("PTZ")]
        PTZ = 0x04,
        /// <summary>
        /// 完全控制权限
        /// </summary>
        [XmlEnumAttribute("All")]
        All = UInt32.MaxValue,
    }
    /// <summary>
    /// 权限枚举类型
    /// </summary>
    [Flags()]
    public enum VideoOutSourcePermissions : uint
    {
        /// <summary>
        /// 无权限
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 完全控制权限
        /// </summary>
        [XmlEnumAttribute("All")]
        All = UInt32.MaxValue,
    }
}
