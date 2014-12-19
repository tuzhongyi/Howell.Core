using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Net
{
    /// <summary>
    /// 事件通知方法
    /// </summary>
    public enum NotificationMethods
    {
        /// <summary>
        /// 无
        /// </summary>
        [XmlEnum("None")]
        None = 0,
        /// <summary>
        /// 邮件通知
        /// </summary>
        [XmlEnum("Email")]
        Email = 0x01,
        /// <summary>
        /// 短信通知
        /// </summary>
        [XmlEnum("SMS")]
        SMS = 0x02,
        /// <summary>
        /// 推送通知
        /// </summary>
        [XmlEnum("Push")]
        Push = 0x04,
        /// <summary>
        /// 全部
        /// </summary>
        [XmlEnum("All")]
        All = Email | SMS | Push,
    }
}
