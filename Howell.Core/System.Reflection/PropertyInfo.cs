using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Howell.Reflection
{
    /// <summary>
    /// 运行时属性信息
    /// </summary>
    public class RunTimePropertyInfo
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyType"></param>
        /// <param name="canRead"></param>
        /// <param name="canWrite"></param>
        public RunTimePropertyInfo(String name,Type propertyType,Boolean canRead,Boolean canWrite)
        {
            this.Name = name;
            this.PropertyType = propertyType;
            this.CanRead = canRead;
            this.CanWrite = canWrite;
        }
        /// <summary>
        /// 获取此属性的名称。
        /// </summary>
        public String Name { get; private set; }
        /// <summary>
        ///  获取此属性的类型。
        /// </summary>
        public Type PropertyType { get; private set; }
        /// <summary>
        ///  获取一个值，该值指示该属性是否可读。
        /// </summary>
        public Boolean CanRead { get; private set; }
        /// <summary>
        ///  获取一个值，该值指示此属性是否可写。
        /// </summary>
        public Boolean CanWrite { get; private set; }

    }
}
