using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    class EnumerableExtensionsTest
    {
        public static void Test()
        {
            PageTest();
        }
        public static void PageTest()
        {
            Int32 count = 100;
            Int32 pageSize = 1000;
            Int32 pageNo = 1;
            List<Int32> collection = new List<Int32>();
            for (int i = 0; i < count; ++i)
            {
                collection.Add(i);
            }
            Console.WriteLine("Collection Items:{0} PageSize:{1} Page:{2}/{3} RecordCount:{4}", collection.Count, pageSize, pageNo, collection.PageCount(pageSize), collection.Page(pageNo, pageSize).Count());
        }
    }
}
