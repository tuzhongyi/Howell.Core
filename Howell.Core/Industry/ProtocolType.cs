using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 协议类型常量
    /// </summary>
    internal static class ProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        public const Int32 None = 0;
        /// <summary>
        /// 皓维标准
        /// </summary>
        public const Int32 Howell5198 = 1;
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        public const Int32 Howell8000 = 2;
        /// <summary>
        /// 海康威视
        /// </summary>
        public const Int32 Hikvision = 3;
        /// <summary>
        /// 皓维5201标准 (工控式)
        /// </summary>
        public const Int32 Howell5201 = 4;
        /// <summary>
        /// PSIA
        /// </summary>
        public const Int32 PSIA = 5;
        /// <summary>
        /// ONVIF
        /// </summary>
        public const Int32 Onvif = 6;
        /// <summary>
        /// GB28181 国标协议
        /// </summary>
        public const Int32 GB28181 = 7;
        /// <summary>
        /// 皓维3204报警协议
        /// </summary>
        public const Int32 Howell3204 = 101;
        /// <summary>
        /// 深圳披克门禁控制器协议
        /// </summary>
        public const Int32 PEAK = 102;
        /// <summary>
        /// 英飞拓矩阵协议特殊应用
        /// </summary>
        public const Int32 InfinovaMatrixSpecial = 103;
    }
    /// <summary>
    /// 模拟报警主机协议类型
    /// </summary>
    public enum AAMProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// PEAK 深圳披克
        /// </summary>
        [XmlEnumAttribute("PEAK")]
        PEAK = ProtocolType.PEAK,
    }
    /// <summary>
    /// 模拟矩阵协议
    /// </summary>
    public enum AnalogMatrixProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
    }
    /// <summary>
    /// 解码单元协议类型
    /// </summary>
    public enum DecodingUnitProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
    }
    /// <summary>
    /// 数字矩阵协议类型
    /// </summary>
    public enum DigitalMatrixProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = ProtocolType.Howell5198,
    }
    /// <summary>
    /// 数字视频录像机协议类型
    /// </summary>
    public enum DVRProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = ProtocolType.Howell5198,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
        /// <summary>
        /// 海康威视
        /// </summary>
        [XmlEnumAttribute("Hikvision")]
        Hikvision = ProtocolType.Hikvision,
        /// <summary>
        /// 皓维5201标准 (工控式)
        /// </summary>
        [XmlEnumAttribute("Howell5201")]
        Howell5201 = ProtocolType.Howell5201,
    }
    /// <summary>
    /// 数字视频服务器协议类型
    /// </summary>
    public enum DVSProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = ProtocolType.Howell5198,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
        /// <summary>
        /// 海康威视
        /// </summary>
        [XmlEnumAttribute("Hikvision")]
        Hikvision = ProtocolType.Hikvision,
    }
    /// <summary>
    /// 编码单元协议类型
    /// </summary>
    public enum EncodingUnitProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
    }
    /// <summary>
    /// 数字矩阵协议类型
    /// </summary>
    public enum HDDecoderProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = ProtocolType.Howell5198,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
        /// <summary>
        /// 海康威视
        /// </summary>
        [XmlEnumAttribute("Hikvision")]
        Hikvision = ProtocolType.Hikvision,
    }
    /// <summary>
    /// 综合数字矩阵协议
    /// </summary>
    public enum IntegratedMatrixProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
    }
    /// <summary>
    /// IP摄像机协议类型
    /// </summary>
    public enum IPCameraProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = ProtocolType.Howell5198,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
        /// <summary>
        /// 海康威视
        /// </summary>
        [XmlEnumAttribute("Hikvision")]
        Hikvision = ProtocolType.Hikvision,
        /// <summary>
        /// Onvif协议
        /// </summary>
        [XmlEnumAttribute("Onvif")]
        Onvif = ProtocolType.Onvif,
    }
    /// <summary>
    /// 矩阵控制单元协议
    /// </summary>
    public enum MatrixControlUnitProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
    }
    /// <summary>
    /// 网络报警主机协议类型
    /// </summary>
    public enum NAMProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维3204报警器
        /// </summary>
        [XmlEnumAttribute("Howell3204")]
        Howell3204 = ProtocolType.Howell3204,
    }
    /// <summary>
    /// 网络视频录像机协议类型
    /// </summary>
    public enum NVRProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = ProtocolType.Howell5198,
        /// <summary>
        /// 皓维8000系列标准
        /// </summary>
        [XmlEnumAttribute("Howell8000")]
        Howell8000 = ProtocolType.Howell8000,
        /// <summary>
        /// 海康威视
        /// </summary>
        [XmlEnumAttribute("Hikvision")]
        Hikvision = ProtocolType.Hikvision,
    }
    /// <summary>
    /// 网络视频服务器协议类型
    /// </summary>
    public enum NVSProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维标准
        /// </summary>
        [XmlEnumAttribute("Howell5198")]
        Howell5198 = ProtocolType.Howell5198,
    }
    /// <summary>
    /// 流媒体服务器协议
    /// </summary>
    public enum StreamingMediaServerProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
    }
    /// <summary>
    /// 视频分析服务器协议类型
    /// </summary>
    public enum VASProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// PSIA协议
        /// </summary>
        [XmlEnumAttribute("PSIA")]
        PSIA = ProtocolType.PSIA,
    }
    /// <summary>
    /// 视频处理服务器协议类型
    /// </summary>
    public enum VPSProtocolType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = ProtocolType.None,
        /// <summary>
        /// 皓维5201标准 (工控式)
        /// </summary>
        [XmlEnumAttribute("Howell5201")]
        Howell5201 = ProtocolType.Howell5201,
    }
}
