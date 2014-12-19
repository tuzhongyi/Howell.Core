using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 视频接口类型
    /// </summary>    
    public enum VideoInterfaceType
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnumAttribute("None")]
        None = 0,
        /// <summary>
        /// 同轴电缆接插件
        /// </summary>
        [XmlEnumAttribute("BNC")]
        BNC = 1,
        /// <summary>
        /// 高清晰度多媒体接口 (High Definition Multimedia Interface)
        /// </summary>
        [XmlEnumAttribute("HDMI")]
        HDMI = 2,
        /// <summary>
        /// 视频图形阵列 (Video Graphics Array)
        /// </summary>
        [XmlEnumAttribute("VGA")]
        VGA = 3,
        /// <summary>
        /// 数字化视像接口 (Digital Visual Interface)
        /// </summary>
        [XmlEnumAttribute("DVI")]
        DVI = 4,
        /// <summary>
        /// 高清数字显示接口 
        /// </summary>
        [XmlEnumAttribute("DisplayPort")]
        DisplayPort = 5,
        /// <summary>
        /// 数据总线
        /// </summary>
        [XmlEnumAttribute("Bus")]
        Bus = 6,
    }
}
