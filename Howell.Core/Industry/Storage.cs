using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 存储方式
    /// </summary>
    public enum StorageType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 设备内部存储
        /// </summary>
        [XmlEnumAttribute("Internal")]
        Internal = 1,
        /// <summary>
        /// 设备外部存储，如挂接盘阵等
        /// </summary>
        [XmlEnumAttribute("External")]
        External = 2,
    }
    /// <summary>
    /// 卷标类型
    /// </summary>
    public enum VolumeType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 虚拟磁盘
        /// </summary>
        [XmlEnumAttribute("VirtualDisk")]
        VirtualDisk = 1,
        /// <summary>
        /// RAID0
        /// </summary>
        [XmlEnumAttribute("RAID0")]
        RAID0 = 2,
        /// <summary>
        /// RAID1
        /// </summary>
        [XmlEnumAttribute("RAID1")]
        RAID1 = 3,
        /// <summary>
        /// RAID0+1
        /// </summary>
        [XmlEnumAttribute("RAID0Plus1")]
        RAID0Plus1 = 4,
        /// <summary>
        /// RAID5
        /// </summary>
        [XmlEnumAttribute("RAID5")]
        RAID5 = 5,
        /// <summary>
        /// RAID6
        /// </summary>
        [XmlEnumAttribute("RAID6")]
        RAID6 = 6,
    }
    /// <summary>
    /// 存储介质
    /// </summary>
    public enum StorageMediumType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 硬盘
        /// </summary>
        [XmlEnumAttribute("HDD")]
        HDD = 1,
        /// <summary>
        /// 闪存
        /// </summary>
        [XmlEnumAttribute("Flash")]
        Flash = 2,
        /// <summary>
        /// 安全数字输入输出卡
        /// </summary>
        [XmlEnumAttribute("SDIO")]
        SDIO = 3,
    }
    /// <summary>
    /// 存储媒介状态
    /// </summary>
    public enum StorageMediumState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [XmlEnumAttribute("Normal")]
        Normal = 0,
        /// <summary>
        /// 正在写入
        /// </summary>
        [XmlEnumAttribute("Writing")]
        Writing = 1,
        /// <summary>
        /// 未被格式化
        /// </summary>
        [XmlEnumAttribute("Unformatted")]
        Unformatted = 2,
        /// <summary>
        /// 休眠
        /// </summary>
        [XmlEnumAttribute("Dormancy")]
        Dormancy = 3,
        /// <summary>
        /// 脱机，媒介由于无法工作而被剔除，无法识别
        /// </summary>
        [XmlEnumAttribute("Offline")]
        Offline = 4,
        /// <summary>
        /// 无法正常读写
        /// </summary>
        [XmlEnumAttribute("Error")]
        Error = 5,        
    }
}
