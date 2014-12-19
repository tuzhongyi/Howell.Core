using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace Howell.Net.NetworkInformation
{
    /// <summary>
    /// 网络监测器
    /// </summary>
    public class NetworkDector 
    {
        public NetworkDector(String hostNameOrAddress)            
        {
            this.HostNameOrAddress = hostNameOrAddress;
        }
        /// <summary>
        /// 主机名或地址
        /// </summary>
        public String HostNameOrAddress { get; private set; }
        public Boolean Ping()
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            PingOptions options = new PingOptions() { Ttl = 255};            
            PingReply reply = ping.Send(HostNameOrAddress,5000,new Byte[32],options);
        }
        
    }
}
