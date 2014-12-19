using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Reflection
{
    /// <summary>
    /// Assembly extension functions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取System.Reflection.Assembly对象的文件版本号
        /// </summary>
        /// <param name="assembly">System.Reflection.Assembly对象</param>
        /// <returns>返回System.Reflection.Assembly对象的文件版本号.</returns>
        /// <exception cref="System.InvalidOperationException">Assembly does not contain file version attribute.</exception>
        public static Version GetFileVersion(this Assembly assembly)
        {
            object[] attributes =  assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            if(attributes.Length > 0)
            {                
                return Version.Parse(((AssemblyFileVersionAttribute)attributes[0]).Version);
            }
            else
            {
                throw new InvalidOperationException("Assembly does not contain file version attribute.");
            }            
        }
    }
}
