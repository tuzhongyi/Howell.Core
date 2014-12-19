using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Net
{
    /// <summary>
    /// Uri连接字符串特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UriConnectionStringAttribute : Attribute
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        public UriConnectionStringAttribute()
        {
        }
    }
}
