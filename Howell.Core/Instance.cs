using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Howell
{
    /// <summary>
    /// 应用程序工具类
    /// </summary>
    public static class Applications
    {
        private static Mutex m_Mutex = null;
        /// <summary>
        /// 判断是否是第一个运行起来的实例
        /// </summary>
        /// <returns>如果是第一个实例则返回True，否则返回False。</returns>
        public static Boolean IsFirstInstance()
        {
            lock (typeof(Applications))
            {
                bool flag = false;
                //第一个参数:true--给调用线程赋予互斥体的初始所属权
                //第一个参数:互斥体的名称
                //第三个参数:返回值,如果调用线程已被授予互斥体的初始所属权,则返回true
                m_Mutex = new System.Threading.Mutex(true, Process.GetCurrentProcess().ProcessName, out flag);
                return flag;
            }
        }
        /// <summary>
        /// 判断是否是第一个运行起来的实例
        /// </summary>
        /// <param name="processName">实例名称</param>
        /// <returns>如果是第一个实例则返回True，否则返回False。</returns>
        public static Boolean IsFirstInstance(String processName)
        {
            lock (typeof(Applications))
            {
                bool flag = false;
                //第一个参数:true--给调用线程赋予互斥体的初始所属权
                //第一个参数:互斥体的名称
                //第三个参数:返回值,如果调用线程已被授予互斥体的初始所属权,则返回true
                m_Mutex = new System.Threading.Mutex(true, processName, out flag);
                return flag;
            }
        }
        /// <summary>
        /// 销毁Application类创建的资源
        /// </summary>
        public static void Dispose()
        {
            lock (typeof(Applications))
            {
                if (m_Mutex != null)
                {
                    m_Mutex.Dispose();
                    m_Mutex = null;
                }
            }
        }
    }
}
