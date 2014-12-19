using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
    /// <summary>
    /// OperationContext扩展函数
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OperationContextExtensions
    {
        /// <summary>
        /// 获取远程客户端终节点信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static System.Net.IPEndPoint GetRemoteEndPoint(this OperationContext context)
        {
            if (OperationContext.Current.IncomingMessageProperties.ContainsKey(RemoteEndpointMessageProperty.Name) == true)
            {
                RemoteEndpointMessageProperty remoteEP = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                if (remoteEP != null)
                {
                    System.Net.IPAddress address = null;
                    if (System.Net.IPAddress.TryParse(remoteEP.Address, out address) == true)
                    {
                        return new System.Net.IPEndPoint(address, remoteEP.Port);
                    }
                }
            }
            return null;            
        }
    }
}
