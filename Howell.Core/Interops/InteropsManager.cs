using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Interops
{
    /// <summary>
    /// 互操作管理工具 抽象类型
    /// </summary>
    public abstract class InteropsManager 
    {
         /// <summary>
        /// Initializes a new instance of the InteropsManager class.
        /// </summary>
        protected InteropsManager()
            : this(System.AppDomain.CurrentDomain.BaseDirectory)
        {

        }
        /// <summary>
        /// Initializes a new instance of the InteropsManager class.
        /// </summary>
        /// <param name="dllDirectory">The path to lib.dll</param>
        protected InteropsManager(string dllDirectory)
        {
            if (String.IsNullOrEmpty(dllDirectory))
                throw new ArgumentNullException("dllDirectory");
            this.DllDirectory = dllDirectory;
            ChangeEnvironment(this.DllDirectory);
        }
        /// <summary>
        /// 修改系统的环境变量
        /// </summary>
        /// <param name="libDllsDirectory">dll所在的文件路径</param>
        protected virtual void ChangeEnvironment(string libDllsDirectory)
        {
            String envir = System.Environment.GetEnvironmentVariable("path");
            envir += ";" + libDllsDirectory;
            System.Environment.SetEnvironmentVariable("path", envir);
        }
        /// <summary>
        /// 动态库路径
        /// </summary>
        public String DllDirectory
        {
            get; protected set; 
        }
        /// <summary>
        /// LIB版本号
        /// </summary>
        public Version LibVersion
        {
            get;
            protected set;
        }
        /// <summary>
        /// 开始加载互操作库
        /// </summary>
        public abstract void Load();
        /// <summary>
        /// 释放互操作库
        /// </summary>
        public abstract void Free();                
    }
    
}
