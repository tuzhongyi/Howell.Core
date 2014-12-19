using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Text
{
    class ConnectionStringBuilderTest
    {
        public static void Test()
        {

            ConnectionStringBuilder connectionString = new ConnectionStringBuilder();
            Console.WriteLine(connectionString.ToString());
            connectionString["UserName"] = "admin";
            connectionString["Password"] = "12345";
            Console.WriteLine(connectionString.ToString());
        }
    }
}
