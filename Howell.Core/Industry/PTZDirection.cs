using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Industry
{
    /// <summary>
    /// 云台方向控制
    /// </summary>
    public enum PTZDirection
    {
        /// <summary>
        /// 停止
        /// </summary>
        [XmlEnum("Stop")]
        Stop = 0,
        /// <summary>
        /// 上
        /// </summary>
        [XmlEnum("Up")]
        Up = 1,
        /// <summary>
        /// 下
        /// </summary>
        [XmlEnum("Down")]
        Down = 2,
        /// <summary>
        /// 左
        /// </summary>
        [XmlEnum("Left")]
        Left = 3,
        /// <summary>
        /// 右
        /// </summary>
        [XmlEnum("Right")]
        Right = 4,
    }
}
