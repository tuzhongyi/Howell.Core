using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Net
{

    /// <summary>
    /// NAT类型
    /// </summary>
    [Flags]
    public enum NATTypes
    {
        /// <summary>
        /// 其他
        /// </summary>
        Other = 0,
        /// <summary>
        /// 流转发
        /// </summary>
        TURN = 0x01,
        /// <summary>
        /// Udp穿透
        /// </summary>
        STUN = 0x02,
        /// <summary>
        /// UPnP打洞
        /// </summary>
        UPnP = 0x04,
    }
}
