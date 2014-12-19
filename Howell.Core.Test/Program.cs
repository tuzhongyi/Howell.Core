using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Howell.ComponentModel;
using Howell.IO;
using System.IO;
using Howell.IO.Serialization;
using Howell.Net;
using Howell.Text;
using Howell.Conditions;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Howell.Cryptography;
using Howell.Time;
using Howell.Media;
using Howell.Schedules;
using Howell.Numeric;
using Howell.Text.RegularExpressions;
using Howell.Drawing.D2;
using Howell.Reflection;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Json;
using Howell.Industry;
using Howell.Text;

namespace Howell
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0}", DateTime.Now.GetWeekOfYear());
            Console.WriteLine(new System.Text.RegularExpressions.Regex("^(yancao|haoweis)$").IsMatch("yancao|haoweis"));
            Console.WriteLine(PathExtensions.IsFileNameValid("dfaa/.wem"));
            Console.WriteLine(PathExtensions.GetValidFileName("dfaa/.wmd"));
            IdentityTest.Test();
            //ConditionTest.Test();
            //ConnectionStringBuilderTest.Test();
            //DateTimeExtensionsTest.Test();
            //MD5Test.Test();
            //BitStreamTest.Test();
            //BitRWTest.Test();
            //AsyncFunctionTest.Test();
            //FtpClientTest.Test();
            //StringTest.Test();
            //SoundPlayerTest.Test();
            //ThresholdNumberTest.Test();
            //PlanTimeTest.Test();
            //PlanTest.Test();
            //ProgrammeTest.Test();
            //WorkSheetTest.Test();
            //WeeklyWorkSheetTest.Test();
            //RegexHelperTest.Test();
            //SharpTest.Test();            
            //AssemblyExtensionsTest.Test();
            //XmlSerializerTest.Test();
            //EnumerableExtensionsTest.Test();
            //PhyscialAddressTest.Test();
            //PingTest.Test();
            //JsonSerializerTest.Test();
            Console.Read();            
        }
    }

}
