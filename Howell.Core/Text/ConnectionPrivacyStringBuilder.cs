using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Howell.Text;

namespace Howell.Text
{
    /// <summary>
    /// 私有信息的连接字符串构造器
    /// </summary>
    public class ConnectionPrivacyStringBuilder : ConnectionStringBuilder
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ConnectionPrivacyStringBuilder()
            : this(null)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="connectionString">连接字符</param>
        public ConnectionPrivacyStringBuilder(String connectionString)
            : base(connectionString)
        {
        }
        /// <summary>
        /// 私有信息的连接字符串
        /// </summary>
        public String PrivacyConnectionString
        {
            get
            {
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(this.ConnectionString));
            }
            set
            {
                this.ConnectionString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
        }        
        
    }
}
