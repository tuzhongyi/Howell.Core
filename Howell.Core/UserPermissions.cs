using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell
{
    /// <summary>
    /// 用户权限信息
    /// </summary>
    public enum UserPermission
    {
        /// <summary>
        /// 匿名用户，只有获取数据信息的权限
        /// </summary>
        [XmlEnumAttribute("Anonymous")]
        Anonymous = 0,
        /// <summary>
        /// 操作者用户，来宾用户权限加上系统设置
        /// </summary>
        [XmlEnumAttribute("Operator")]
        Operator = 1,
        /// <summary>
        /// 管理员权限，所有权限
        /// </summary>
        [XmlEnumAttribute("Administrator")]
        Administrator = 2,
        /// <summary>
        /// 扩展用户权限，具体权限参见扩展内容信息
        /// </summary>
        [XmlEnumAttribute("Extended")]
        Extended = 3,
    }
    /// <summary>
    /// 用户详细权限信息
    /// </summary>
    [FlagsAttribute()]
    public enum UserPermissions : uint
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0x00,
        /// <summary>
        /// 系统信息
        /// </summary>
        [XmlEnumAttribute("Information")]
        Information = 0x01,
        /// <summary>
        /// 系统设置
        /// </summary>
        [XmlEnumAttribute("System")]
        System = 0x02,
        /// <summary>
        /// 用户管理控制
        /// </summary>
        [XmlEnumAttribute("User")]
        User = 0x04,
        /// <summary>
        /// 修改添加设备信息权限
        /// </summary>
        [XmlEnumAttribute("Device")]
        Device = 0x08,
        /// <summary>
        /// 全功能
        /// </summary>
        [XmlEnumAttribute("All")]
        All = UInt32.MaxValue,
    }
}
