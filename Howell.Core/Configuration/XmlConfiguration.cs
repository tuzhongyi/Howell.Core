using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Configuration
{
    /// <summary>
    /// XML配置信息读写工具
    /// </summary>
    public class XmlConfiguration
    {
        private Dictionary<String, Object> m_Cache = new Dictionary<string, object>();

        /// <summary>
        /// 构造
        /// </summary>
        public XmlConfiguration()
            : this(System.AppDomain.CurrentDomain.BaseDirectory)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">配置路径</param>
        public XmlConfiguration(String path)
        {
            this.Path = path;
            //创建路径
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 配置路径
        /// </summary>
        public String Path { get; private set; }
        /// <summary>
        /// 获取指定类型的配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
             where T : new()
        {
            lock (this)
            {
                Type type = typeof(T);
                T result = new T();
                if (m_Cache.ContainsKey(type.FullName) == false)
                {
                    XmlSerializer<T> xs = new XmlSerializer<T>();
                    String filePath = System.IO.Path.Combine(this.Path, String.Format("{0}.xml", type.FullName));
                    if (System.IO.File.Exists(filePath) == false)
                    {
                        xs.ToFile(new T(), filePath);
                    }
                    result = xs.FromFile(filePath);
                }
                else
                {
                    result = (T)m_Cache[type.FullName];
                }
                return result;
            }
        }
        /// <summary>
        /// 设置指定类型的配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">配置信息</param>
        public void Set<T>(T value)
            where T : new()
        {
            lock (this)
            {
                Type type = typeof(T);
                XmlSerializer<T> xs = new XmlSerializer<T>();
                String filePath = System.IO.Path.Combine(this.Path, String.Format("{0}.xml", type.FullName));
                xs.ToFile(value, filePath);
                m_Cache[type.FullName] = value;
            }
        }
    }
}
