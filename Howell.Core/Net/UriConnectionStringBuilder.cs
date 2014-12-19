using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Howell.Text;
using System.Reflection;

namespace Howell.Net
{
    /// <summary>
    /// Uri 连接字符串创建器
    /// </summary>
    public abstract class UriConnectionStringBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        private String _schemeName;
        /// <summary>
        /// 
        /// </summary>
        private String _privacyData;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemeName"></param>
        protected UriConnectionStringBuilder(String schemeName)
            : this(schemeName, null)
        {
        }
        /// <summary>
        /// 内部构造
        /// </summary>
        /// <param name="schemeName"></param>
        /// <param name="connectionString"></param>
        protected UriConnectionStringBuilder(String schemeName, Uri connectionString)
        {
            _schemeName = schemeName;
            if(connectionString != null)
                this.ConnectionString = connectionString;
        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public Uri ConnectionString
        {
            get
            {
                String userInfo ="";
                if(String.IsNullOrEmpty(this.Username)==false)
                {
                    userInfo += this.Username;
                    if(String.IsNullOrEmpty(this.Password) == false)
                    {
                        userInfo += String.Format(":{0}", this.Password);
                    }
                    userInfo += "@";
                }
                String uriString = String.Format("{0}://{1}{2}:{3}/{4}", Scheme,userInfo, Host, Port,AbsolutePath);
                if (String.IsNullOrEmpty(this.PrivacyData) == false)
                {
                    uriString += String.Format("?{0}", this.PrivacyData);
                }
                return new Uri(uriString);
            }
            set
            {
                if (String.Compare(this.Scheme, value.Scheme, true) != 0)
                    throw new UriFormatException(String.Format("Uri {0} format error, scheme is {1}.", value, Scheme));
                this.Host = value.Host;
                this.Port = value.Port;
                String[] userInfos = value.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                this.Username = (userInfos.Length > 0)? userInfos[0] : "";
                this.Password = (userInfos.Length > 1) ? userInfos[1] : "";
                this.AbsolutePath = value.AbsolutePath.Trim('\\', '/');
                this.PrivacyData = value.Query.TrimStart('?');
            }
        }
        /// <summary>
        /// 此实例的端口号部分。
        /// </summary>
        public Int32 Port { get; set; }
        /// <summary>
        /// 此实例的主机部分。
        /// </summary>
        public String Host { get; set; }
        /// <summary>
        /// 相对路径
        /// </summary>
        public String AbsolutePath { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public String Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// 此实例的私有数据部分
        /// </summary>
        public String PrivacyData
        {
            get { return GetPrivacyData(); }
            set { SetPrivacyData(value); }
        }
        /// <summary>
        /// 获取Uri视图
        /// </summary>
        public String Scheme
        {
            get { return _schemeName; }
        }
        /// <summary>
        /// 设置私有数据内容
        /// </summary>
        /// <param name="value"></param>
        protected virtual void SetPrivacyData(string value)
        {
            _privacyData = value;
        }
        /// <summary>
        /// 获取私有数据内容
        /// </summary>
        /// <returns></returns>
        protected virtual string GetPrivacyData()
        {
            return _privacyData;
        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ConnectionString.ToString();
        }
        /// <summary>
        /// 获取连接字符串的私有数据的属性列表
        /// </summary>
        /// <returns></returns>
        public PropertyInfo[] GetPrivacyDataProperties()
        {
            List<PropertyInfo> result = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(UriConnectionStringAttribute), true);
                if (attrs.Length > 0)
                    result.Add(prop);
            }
            return result.ToArray();
        }
    }
}
