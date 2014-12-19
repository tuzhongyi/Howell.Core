using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 设备类别
    /// </summary>
    public enum DeviceClassification
    {
        /// <summary>
        /// 非设备
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// IP摄像机(IP Camera)
        /// </summary>
        [XmlEnumAttribute("IPCamera")]
        IPCamera = 1,
        /// <summary>
        /// 数字视频服务器(Digital Video Server)
        /// </summary>
        [XmlEnumAttribute("DVS")]
        DVS = 2,
        /// <summary>
        /// 网络视频录像机(Net Video Recorder)
        /// </summary>
        [XmlEnumAttribute("NVR")]
        NVR = 3,
        /// <summary>
        /// 数字硬盘录像机(Digital Video Recorder)
        /// </summary>
        [XmlEnumAttribute("DVR")]
        DVR = 4,
        /// <summary>
        /// 数字矩阵(Digital Matrix)
        /// </summary>
        [XmlEnumAttribute("DigitalMatrix")]
        DigitalMatrix = 5,
        /// <summary>
        /// 高清解码器(HD Decoder)
        /// </summary>
        [XmlEnumAttribute("HDDecoder")]
        HDDecoder = 6,
        /// <summary>
        /// 模拟矩阵(Analog Matrix)
        /// </summary>
        [XmlEnumAttribute("AnalogMatrix")]
        AnalogMatrix = 7,
        /// <summary>
        /// 视频分析服务器(Video Analytics Server)
        /// </summary>
        [XmlEnumAttribute("VAS")]
        VAS = 8,
        /// <summary>
        /// 模拟报警主机(Analog Alarm Mainframe)
        /// </summary>
        [XmlEnumAttribute("AAM")]
        AAM = 9,
        /// <summary>
        /// 网络报警主机(Net Alarm Mainframe)
        /// </summary>
        [XmlEnumAttribute("NAM")]
        NAM = 10,
        /// <summary>
        /// 视频处理服务器(Video Process Server)
        /// </summary>
        [XmlEnumAttribute("VPS")]
        VPS = 11,
        /// <summary>
        /// 综合矩阵(Integrated Matrix)
        /// </summary>
        [XmlEnumAttribute("IntegratedMatrix")]
        IntegratedMatrix = 12,
        /// <summary>
        /// 数字矩阵控制单元(Matrix Control Unit)
        /// </summary>
        [XmlEnumAttribute("MatrixControlUnit")]
        MatrixControlUnit = 13,
        /// <summary>
        /// 流媒体服务器 (Streaming Media Server)
        /// </summary>
        [XmlEnumAttribute("StreamingMediaServer")]
        StreamingMediaServer = 14,
        /// <summary>
        /// 解码单元
        /// </summary>
        [XmlEnumAttribute("DecodingUnit")]
        DecodingUnit = 15,
        /// <summary>
        /// 编码单元
        /// </summary>
        [XmlEnumAttribute("EncodingUnit")]
        EncodingUnit = 16,
        /// <summary>
        /// 网络视频服务器(Net Video Server)
        /// </summary>
        [XmlEnumAttribute("NVS")]
        NVS = 17,
        /// <summary>
        /// 数据服务器 (Data Server)
        /// </summary>
        [XmlEnumAttribute("DataServer")]
        DataServer = 18,
        /// <summary>
        /// 数据采集服务器 (Acquisition Server)
        /// </summary>
        [XmlEnumAttribute("AcquisitionServer")]
        AcquisitionServer = 19,
        /// <summary>
        /// 系统网关 (System Gateway)
        /// </summary>
        [XmlEnumAttribute("SystemGateway")]
        SystemGateway = 20,
        /// <summary>
        /// 模拟摄像机
        /// </summary>
        [XmlEnumAttribute("Camera")]
        Camera = 21,
                
    }
        
}
