using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net
{
    /// <summary>
    /// System.Net.IPAddress类型的扩展函数
    /// </summary>
    public static class IPAddressExtensions
    {
        /// <summary>
        /// IP 地址的长值。例如，Little-Endian 格式的值 0x8f181424 可能为 IP 地址“143.24.20.36”。
        /// </summary>
        /// <param name="address">IP地址对象</param>
        /// <returns>返回Little-Endian 格式的值</returns>
        public static Int64 ToLittleEdianAddress(this IPAddress address)
        {
            Int64 result = 0;
            Byte [] addressBytes = address.GetAddressBytes();
            for (int i = 0; i < 4; ++i)
            {
                result += (Int64)(((Int64)addressBytes[i]) << (8 * (3-i)));
            }
            return result;
        }
        /// <summary>
        /// 将Little-Endian 格式的值转换为System.Net.IPAddress地址
        /// </summary>
        /// <param name="littleEdianAddress">Little-Endian 格式的值</param>
        /// <returns>返回IP地址</returns>
        public static IPAddress FromLittleEndianAddress(Int64 littleEdianAddress)
        {
            Byte[] addressBytes = new byte[4];
            for(int i = 0;i < 4;++i)
            {
                addressBytes[i] =  (Byte)((littleEdianAddress >> (8 * (3-i)))&0xFF);
            }
            return new IPAddress(addressBytes);
        }
        /// <summary>
        /// 获取第一个本地IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIPAddress()
        {
            System.Net.IPHostEntry ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            return ips.AddressList.SingleOrDefault();
        }
        /// <summary>
        /// 获取本地IP地址列表
        /// </summary>
        /// <returns></returns>
        public static IPAddress [] GetLocalIPAddresses()
        {
            System.Net.IPHostEntry ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            return ips.AddressList;
        }
    }
}
