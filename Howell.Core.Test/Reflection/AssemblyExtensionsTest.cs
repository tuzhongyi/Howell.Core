using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Howell.Reflection
{
    /// <summary>
    /// 类库扩展函数测试
    /// </summary>
    public class AssemblyExtensionsTest
    {
        public static void Test()
        {
            Console.WriteLine("+++++++++++++++++++AssemblyExtensionsTest Begin+++++++++++++++++++++++");
            GetFileVersion();
            Console.WriteLine("===================AssemblyExtensionsTest End========================");
        }
        private static void GetFileVersion()
        {

            Console.WriteLine("Assembly version:{0}",Assembly.GetExecutingAssembly().GetFileVersion());
        }
    }
}
