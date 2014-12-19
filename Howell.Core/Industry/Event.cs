using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{

    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 无信息
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0x0000,
        /// <summary>
        /// IO输入，当IO输入状态改变时触发 （单元模块类型 报警输入）
        /// </summary>
        [XmlEnumAttribute("IO")]
        IO = 0x0001,
        /// <summary>
        /// 运动侦测，当视频中有运动物体时触发 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("VMD")]
        VMD = 0x0002,
        /// <summary>
        /// 视频信号丢失，当视频输入信号中断时触发 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("Videoloss")]
        Videoloss = 0x0003,
        /// <summary>
        /// IR-CUT 红外滤光片切换模式  ON/OFF
        /// </summary>
        [XmlEnumAttribute("IRCut")]
        IRCut = 0x0004,
        /// <summary>
        /// 彩色转黑白模式 ON/OFF
        /// </summary>
        [XmlEnumAttribute("DayNight")]
        DayNight = 0x0005,
        /// <summary>
        /// 录像状态 ON/OFF
        /// </summary>
        [XmlEnumAttribute("RecordState")]
        RecordState = 0x0006,
        /// <summary>
        /// 创建RAID(磁盘冗余阵列)失败（单元模块类型 存储介质）
        /// </summary>
        [XmlEnumAttribute("RAIDFailure")]
        RAIDFailure = 0x0007,
        /// <summary>
        /// 存储介质丢失或损坏，当硬盘无法识别或由于读写失败过于频繁而被卸载时触发 （单元模块类型 存储介质）
        /// </summary>
        [XmlEnumAttribute("StorageMediumFailure")]
        StorageMediumFailure = 0x0008,
        /// <summary>
        /// 录像状态异常，指定的视频源处于录像状态时无法正常录像 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("RecordingFailure")]
        RecordingFailure = 0x0009,
        /// <summary>
        /// 不正常的视频，当视频源有干扰或扭曲时触发 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("BadVideo")]
        BadVideo = 0x000A,
        /// <summary>
        /// 非法的销售点 （单元模块类型 设备本身）
        /// </summary>
        [XmlEnumAttribute("POS")]
        POS = 0x000B,
        /// <summary>
        /// 风扇异常 （单元模块类型 设备本身）
        /// </summary>
        [XmlEnumAttribute("FanFailure")]
        FanFailure = 0x000C,
        /// <summary>
        /// CPU使用率异常，当CPU的使用率超过指定阀值时触发（单元模块类型 设备本身）
        /// </summary>
        [XmlEnumAttribute("CpuUsage")]
        CpuUsage = 0x000D,
        /// <summary>
        /// 内存使用率异常，当Memory的使用率超过指定阀值时触发（单元模块类型 设备本身）
        /// </summary>
        [XmlEnumAttribute("MemoryUsage")]
        MemoryUsage = 0x000E,
        /// <summary>
        /// 温度异常，当温度超过指定阀值时触发（单元模块类型 设备本身）
        /// </summary>
        [XmlEnumAttribute("Temperature")]
        Temperature = 0x000F,
        /// <summary>
        /// 压力异常，当压力超过指定阀值时触发（单元模块类型 设备本身）
        /// </summary>
        [XmlEnumAttribute("Pressure")]
        Pressure = 0x0010,
        /// <summary>
        /// 电压异常，当电压超出指定范围时触发（单元模块类型 设备本身）
        /// </summary>
        [XmlEnumAttribute("Voltage")]
        Voltage = 0x0011,
        /// <summary>
        /// 超出视频连接上限, 当视频连接数已达上限时又有新的视频请求连接时触发（单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("MaximumConnections")]
        MaximumConnections = 0x0012,
        /// <summary>
        /// 网络码率异常，当网口码流超过指定阀值时触发（单元模块类型 网络接口）
        /// </summary>
        [XmlEnumAttribute("NetworkBitrate")]
        NetworkBitrate = 0x0013,
        /// <summary>
        /// 视频码率异常，当视频码率低于或超过指定阀值时触发（单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("VideoBitrate")]
        VideoBitrate = 0x0014,
        /// <summary>
        /// 聚焦模糊
        /// </summary>
        [XmlEnumAttribute("Squint")]
        Squint = 0x0015,
        /// <summary>
        /// 视频转动
        /// </summary>
        [XmlEnumAttribute("VideoTurned")]
        VideoTurned = 0x0016,
        /// <summary>
        /// 区域入侵 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("IntrusionArea")]
        IntrusionArea = 0x1001,
        /// <summary>
        /// 越线 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("IntrusionLine")]
        IntrusionLine = 0x1002,
        /// <summary>
        /// 滞留/徘徊 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("Loitering")]
        Loitering = 0x1003,
        /// <summary>
        /// 抛弃物 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("StationaryInserted")]
        StationaryInserted = 0x1004,
        /// <summary>
        /// 物品遗失 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("StationaryRemoved")]
        StationaryRemoved = 0x1005,
        /// <summary>
        /// 逆向入侵 （单元模块类型 摄像机/视频源）
        /// </summary>
        [XmlEnumAttribute("ReversedIntrustion")]
        ReversedIntrustion = 0x1006,
    }

    /// <summary>
    /// 事件触发状态
    /// </summary>
    public enum EventState
    {
        /// <summary>
        /// 未触发，当不符合事件触发条件时的状态
        /// </summary>
        [XmlEnum("Inactive")]
        Inactive = 0,
        /// <summary>
        /// 触发，当符合事件触发条件时的状态
        /// </summary>
        [XmlEnum("Active")]
        Active = 1,
        /// <summary>
        /// 常态(非信号量的常态)
        /// </summary>
        [XmlEnum("Normalcy")]
        Normalcy = 2,
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 无状态
        /// </summary>
        [XmlEnum("None")]
        None = 0,
        /// <summary>
        /// 预览视频
        /// </summary>
        [XmlEnum("RealPlay")]
        RealPlay = 1,
        /// <summary>
        /// 文件回放
        /// </summary>
        [XmlEnum("PlayBack")]
        PlayBack = 2,
        /// <summary>
        /// 下载
        /// </summary>
        [XmlEnum("Download")]
        Download = 3,
        /// <summary>
        /// 停止录像
        /// </summary>
        [XmlEnum("RecordStopped")]
        RecordStopped = 4,
        /// <summary>
        /// 重启
        /// </summary>
        [XmlEnum("Reboot")]
        Reboot = 5,
        /// <summary>
        /// IP地址被修改
        /// </summary>
        [XmlEnum("IPAddressModified")]
        IPAddressModified = 6,
        /// <summary>
        /// 网络测速开始
        /// </summary>
        [XmlEnum("NetworkSpeedDiagnosticsStarted")]
        NetworkSpeedDiagnosticsStarted = 7,
        /// <summary>
        /// 网络测速停止
        /// </summary>
        [XmlEnum("NetworkSpeedDiagnosticsStopped")]
        NetworkSpeedDiagnosticsStopped = 8,
        //....
    }
}
