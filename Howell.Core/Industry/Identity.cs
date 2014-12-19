using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Howell.Conditions;
using System.Text.RegularExpressions;

namespace Howell.Industry
{
    /// <summary>
    /// 通用标识符
    ///码段	        码位	    含义	            数值及说明
    ///系统编码	    1,2	        国家区域码	        保留 00
    ///                3,4	        省级编码	        参见：GT-2260-2007，如果其他国家则忽略3-8位编码信息
    ///                5,6	        市级编码	
    ///                7,8	        区级编码	
    ///设备类型编码	9,10	    设备类型	           01	IP摄像机
    ///                                                02	DVS
    ///                                                03	NVR
    ///                                                04	DVR
    ///                                                05	数据矩阵
    ///                                                06	高清解码器
    ///                                                07	模拟矩阵
    ///                                                08	VAS
    ///                                                09	模拟报警主机
    ///                                                10	网络报警主机
    ///                                                11	VPS
    ///                                                12	综合数据矩阵
    ///                                                13	矩阵控制单元
    ///                                                14	流媒体服务器
    ///                                                15	解码单元
    ///                                                16	编码单元
    ///                                                17	NVS
    ///                                                18	数据服务器
    ///                                                19	数据采集服务器
    ///                                                20	系统网关
    ///项目编码	    11-17	    公司内部项目编码	保留：默认是0
    ///设备序号编码	18-23	    设备系统内的唯一标示序号	
    ///网络识别码	    24	        网络识别码	        保留
    ///单元模块类型编码25,26	    前端设备单元模块	00	设备本身
    ///                                                01	摄像机、视频源采集单元（CMOS，CCD，HDMI等）
    ///                                                02	存储媒介
    ///                                                04	报警防区
    ///                                                05	报警输入
    ///                                                06	辅助输出
    ///                                                07	网络接口
    ///                                                08	视频分析单元
    ///                                                09	视频源输出单元（HDMI，VGA，BNC等）
    ///保留	        27,28			
    ///单元模块序号编码29-32	    单元模块序号0-9999	设备本身请填0，其他所有序号都从1开始
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// 解析标识符字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Identity Parse(String value)
        {
            Condition.WithExceptionOnFailure<FormatException>().Requires(value, "value").MatchPattern(@"^\d{32}$", "Identity pattern failed.");
            return new Identity(
                Byte.Parse(value.Substring(0, 2)),
                Byte.Parse(value.Substring(2, 2)),
                Byte.Parse(value.Substring(4, 2)),
                Byte.Parse(value.Substring(6, 2)),
                (IdentityClassification)Int32.Parse(value.Substring(8, 2)),
                Int32.Parse(value.Substring(10, 7)),
                Int32.Parse(value.Substring(17,6)),
                (NetworkType)Int32.Parse(value.Substring(23,1)),
                (ModuleType)Int32.Parse(value.Substring(24, 2)),
                Byte.Parse(value.Substring(26, 2)),
                Int32.Parse(value.Substring(28, 4)));
        }
        /// <summary>
        /// 解析标识符字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Boolean TryParse(String value, out Identity id)
        {
            id = null;
            try
            {
                id = Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nation">国家</param>
        /// <param name="province">省/市</param>
        /// <param name="city">市</param>
        /// <param name="county">区/县</param>
        /// <param name="projectNumber">项目工程</param>
        /// <param name="classification">设备类型</param>
        /// <param name="serialNumber">序列号</param>
        /// <param name="network">网络</param>
        /// <param name="moduleType">模块类型</param>
        /// <param name="moduleZoneNumber">模块所属区域</param>
        /// <param name="moduleNumber">模块编号</param>
        public Identity(Int32 nation, Int32 province, Int32 city, Int32 county, IdentityClassification classification, Int32 projectNumber, Int32 serialNumber, NetworkType network, ModuleType moduleType, Int32 moduleZoneNumber, Int32 moduleNumber)
        {
            this.Nation = nation;
            this.Province = province;
            this.City = city;
            this.County = county;
            this.ProjectNumber = projectNumber;
            this.Classification = classification;
            this.SerialNumber = serialNumber;
            this.Network = network;
            this.ModuleType = moduleType;
            this.ModuleZoneNumber = moduleZoneNumber;
            this.ModuleNumber = moduleNumber;
        }

        /// <summary>
        /// 根据现有的设备或模块Id来创建其他模块的Id值
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        /// <param name="moduleZoneNumber">模块所属区域</param>
        /// <param name="moduleNumber">模块编号</param>
        /// <returns></returns>
        public Identity Create(ModuleType moduleType, Byte moduleZoneNumber, Int32 moduleNumber)
        {
            return new Identity(this.Nation, this.Province, this.City, this.County, this.Classification, this.ProjectNumber, this.SerialNumber, this.Network, moduleType, moduleZoneNumber, moduleNumber);
        }
        /// <summary>
        /// 获取现有设备的Id
        /// </summary>
        /// <returns></returns>
        public Identity GetDeviceIdentity()
        {
            return new Identity(this.Nation, this.Province, this.City, this.County, this.Classification, this.ProjectNumber, this.SerialNumber, this.Network, ModuleType.None, 0, 0);
        }
        private Int32 m_Nation = 0;
        private Int32 m_Privonce = 0;
        private Int32 m_City = 0;
        private Int32 m_County = 0;
        private Int32 m_ProjectNumber = 0;
        private Int32 m_Classification = 0;
        private Int32 m_SerialNumber = 0;
        private Int32 m_Network = 0;
        private Int32 m_ModuleType = 0;
        private Int32 m_ModuleZoneNumber = 0;
        private Int32 m_ModuleNumber = 0;

        /// <summary>
        /// 国家编码
        /// </summary>
        public Int32 Nation
        {
            get
            {
                return m_Nation;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "Nation").IsInRange(0, 99);
                m_Nation = value;
            }
        }
        /// <summary>
        /// 省级编码 0-99
        /// </summary>
        public Int32 Province
        {
            get
            {
                return m_Privonce;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "Province").IsInRange(0, 99);
                m_Privonce = value;
            }
        }
        /// <summary>
        /// 市级编码 0-99
        /// </summary>
        public Int32 City
        {
            get
            {
                return m_City;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "City").IsInRange(0, 99);
                m_City = value;
            }
        }
        /// <summary>
        /// 区级编码 0-99
        /// </summary>
        public Int32 County
        {
            get
            {
                return m_County;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "County").IsInRange(0, 99);
                m_County = value;
            }
        }
        /// <summary>
        /// 设备类型
        /// </summary>
        public IdentityClassification Classification
        {
            get
            {
                return (IdentityClassification)m_Classification;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "Classification").IsInRange(IdentityClassification.None, IdentityClassification.Department);
                m_Classification = (Int32)value;
            }
        }
        /// <summary>
        /// 项目编码 0-9999999
        /// </summary>
        public Int32 ProjectNumber
        {
            get
            {
                return m_ProjectNumber;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "ProjectNumber").IsInRange(0, 9999999);
                m_ProjectNumber = value;
            }
        }
        /// <summary>
        /// 序号编码 0-999999
        /// </summary>
        public Int32 SerialNumber
        {
            get
            {
                return m_SerialNumber;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "SerialNumber").IsInRange(0, 999999);
                m_SerialNumber = value;
            }
        }
        /// <summary>
        /// 网络识别码
        /// </summary>
        public NetworkType Network
        {
            get
            {
                return (NetworkType)m_Network;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "Network").IsInRange(NetworkType.None, NetworkType.None);
                m_Network = (Int32)value;
            }
        }
        /// <summary>
        /// 模块类型
        /// </summary>
        public ModuleType ModuleType
        {
            get
            {
                return (ModuleType)m_ModuleType;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "ModuleType").IsInRange(ModuleType.None, ModuleType.Decoding);
                m_ModuleType = (Int32)value;
            }
        }
        /// <summary>
        /// 模块所属区域 0-99
        /// </summary>
        public Int32 ModuleZoneNumber
        {
            get
            {
                return m_ModuleZoneNumber;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "ModuleZoneNumber").IsInRange(0, 99);
                m_ModuleZoneNumber = value;
            }
        }
        /// <summary>
        /// 单元模块序号编码 0-9999
        /// </summary>
        public Int32 ModuleNumber
        {
            get
            {
                return m_ModuleNumber;
            }
            set
            {
                Condition.WithExceptionOnFailure<ArgumentOutOfRangeException>().Requires(value, "ModuleNumber").IsInRange(0, 9999);
                m_ModuleNumber = value;
            }
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0:D2}{1:D2}{2:D2}{3:D2}{4:D2}{5:D7}{6:D6}{7:D1}{8:D2}{9:D2}{10:D4}", m_Nation, m_Privonce, m_City, m_County, m_Classification, m_ProjectNumber, m_SerialNumber, m_Network, m_ModuleType, m_ModuleZoneNumber, m_ModuleNumber);
        }
        /// <summary>
        /// 获取HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }
    }

    /// <summary>
    /// 类型编码枚举值
    /// </summary>
    public enum IdentityClassification : byte
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
        /// <summary>
        /// 视频源
        /// </summary>
        [XmlEnumAttribute("VideoSource")]
        VideoSource = 51,
        /// <summary>
        /// 视频输出源
        /// </summary>
        [XmlEnumAttribute("VideoOutSource")]
        VideoOutSource = 52,
        /// <summary>
        /// 地图
        /// </summary>
        [XmlEnumAttribute("Map")]
        Map = 61,
        /// <summary>
        /// 系统用户
        /// </summary>
        [XmlEnumAttribute("User")]
        User = 81,
        /// <summary>
        /// 用户部门
        /// </summary>
        [XmlEnumAttribute("Department")]
        Department = 82,
        /// <summary>
        /// 视频视图
        /// </summary>
        [XmlEnumAttribute("VideoSourceView")]
        VideoSourceView = 91,
        /// <summary>
        /// 电视墙视图
        /// </summary>
        [XmlEnumAttribute("VideoWallView")]
        VideoWallView = 92,
        /// <summary>
        /// 地图视图
        /// </summary>
        [XmlEnumAttribute("MapView")]
        MapView = 93,
    }

    /// <summary>
    /// 通用标识符转换工具
    /// </summary>
    public static class IdentityConvert
    {
        /// <summary>
        /// 将普通模块标示符转换为设备标示符
        /// </summary>
        /// <param name="id">标示符对象</param>
        /// <returns>返回设备标示符对象</returns>
        public static Identity ToDeviceId(Identity id)
        {
            return new Identity(id.Nation, id.Province, id.City, id.County, id.Classification, id.ProjectNumber, id.SerialNumber, id.Network, ModuleType.None, 0, 0);
        }
        /// <summary>
        /// 将标示符转换为模块标示符
        /// </summary>
        /// <param name="id">标示符对象</param>
        /// <param name="moduleType">模块类型</param>
        /// <param name="moduleZoneNumber">模块区域编号[1-n]</param>
        /// <param name="moduleNumber">模块编号[1-n]</param>
        /// <returns>返回模块标示符对象</returns>
        public static Identity ToModuleId(Identity id, ModuleType moduleType, Byte moduleZoneNumber, Byte moduleNumber)
        {
            return new Identity(id.Nation, id.Province, id.City, id.County, id.Classification, id.ProjectNumber, id.SerialNumber, id.Network, moduleType, moduleZoneNumber, moduleNumber);
        }
        /// <summary>
        /// 将普通模块标示符转换为设备标示符
        /// </summary>
        /// <param name="id">标示符</param>
        /// <returns>返回设备标示符对象</returns>
        public static Identity ToDeviceId(String id)
        {
            return ToDeviceId(Identity.Parse(id));
        }
        /// <summary>
        /// 将标示符转换为模块标示符
        /// </summary>
        /// <param name="id">标示符</param>
        /// <param name="moduleType">模块类型</param>
        /// <param name="moduleZoneNumber">模块区域编号[1-n]</param>
        /// <param name="moduleNumber">模块编号[1-n]</param>
        /// <returns>返回模块标示符对象</returns>
        public static Identity ToModuleId(String id, ModuleType moduleType, Byte moduleZoneNumber, Byte moduleNumber)
        {
            return ToModuleId(Identity.Parse(id), moduleType, moduleZoneNumber, moduleNumber);
        }

    }

    /// <summary>
    /// 单元模块类型
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// 设备本身
        /// </summary>
        [XmlEnum("None")]
        None = 0,
        /// <summary>
        /// 摄像机、视频源采集单元（CMOS，CCD，HDMI等）
        /// </summary>
        [XmlEnum("VideoInput")]
        VideoInput = 1,
        /// <summary>
        /// 存储媒介
        /// </summary>
        [XmlEnum("StorageMedium")]
        StorageMedium = 2,
        /// <summary>
        /// 报警防区
        /// </summary>
        [XmlEnum("IODefenceZone")]
        IODefenceZone = 3,
        /// <summary>
        /// 报警输入
        /// </summary>
        [XmlEnum("IOInput")]
        IOInput = 4,
        /// <summary>
        /// 辅助输出
        /// </summary>
        [XmlEnum("IOOutput")]
        IOOutput = 5,
        /// <summary>
        /// 网络接口
        /// </summary>
        [XmlEnum("NetworkInterface")]
        NetworkInterface = 6,
        /// <summary>
        /// 网络接口
        /// </summary>
        [XmlEnum("VideoAnalytics")]
        VideoAnalytics = 7,
        /// <summary>
        /// 视频源输出单元（HDMI，VGA，BNC等）
        /// </summary>
        [XmlEnum("VideoOutput")]
        VideoOutput = 8,
        /// <summary>
        /// 解码通道或单元
        /// </summary>
        [XmlEnum("Decoding")]
        Decoding = 9,
    }
    /// <summary>
    /// 网络类型
    /// </summary>
    public enum NetworkType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnum("None")]
        None = 0,
    }
}
