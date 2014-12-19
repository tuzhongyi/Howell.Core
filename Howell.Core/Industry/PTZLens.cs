using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 镜头控制
    /// </summary>
    public enum PTZLens
    {
        /// <summary>
        /// 停止
        /// </summary>
        [XmlEnum("Stop")]
        Stop = 0,
        /// <summary>
        /// 光圈开
        /// </summary>
        [XmlEnum("IrisOpen")]
        IrisOpen = 1,
        /// <summary>
        /// 光圈关
        /// </summary>
        [XmlEnum("IrisClose")]
        IrisClose = 2,
        /// <summary>
        /// 镜头拉远
        /// </summary>
        [XmlEnum("ZoomTele")]
        ZoomTele = 3,
        /// <summary>
        /// 镜头拉近
        /// </summary>
        [XmlEnum("ZoomWide")]
        ZoomWide = 4,
        /// <summary>
        /// 聚焦变远
        /// </summary>
        [XmlEnum("FocusFar")]
        FocusFar = 5,
        /// <summary>
        /// 聚焦变近
        /// </summary>
        [XmlEnum("FocusNear")]
        FocusNear = 6,
    }
}
