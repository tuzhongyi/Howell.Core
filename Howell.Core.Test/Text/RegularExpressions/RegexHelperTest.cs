using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Text.RegularExpressions
{
    public static class RegexHelperTest
    {
        public static void Test()
        {
            try
            {
                IsInteger();
                IsFloat();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void IsInteger()
        {
            Console.WriteLine("++++++++++++++++RegexHelper.IsInteger++++++++++++++++++");
            IsInteger("+21001");
            IsInteger("-10002");
            IsInteger(".100201");
            IsInteger("019921");
            IsInteger("129876119918");
            IsInteger("190.9914");
            Console.WriteLine("===============================================");
        }
        private static void IsInteger(String value)
        {
            Console.WriteLine("{0} is {1} Integer", value, RegexHelper.IsInteger(value) ? "" : "not");
        }
        private static void IsFloat()
        {
            Console.WriteLine("++++++++++++++++RegexHelper.IsInt32++++++++++++++++++");
            IsFloat("+" + Int32.MaxValue.ToString());
            IsFloat(Int32.MaxValue.ToString());
            IsFloat(Int32.MinValue.ToString());
            IsFloat((Int32.MaxValue / 2).ToString());
            IsFloat((Int32.MinValue / 2).ToString());
            IsFloat("+0");
            IsFloat("0");
            IsFloat("-0");
            IsFloat("-999999999");
            IsFloat("999999999");
            IsFloat("2147483648");
            IsFloat("-2147483649");
            IsFloat("-120002.9929");
            IsFloat("+12.9929");
            IsFloat("+0.0");
            IsFloat("0.01");
            IsFloat("1.0001");
            IsFloat("a.12");
            IsFloat(".12");
            Console.WriteLine("===============================================");
        }
        private static void IsFloat(String value)
        {

            try
            {
                Console.WriteLine("{0} is {1} Float", value, RegexHelper.IsFloat(value) ? "" : "not");
                Single.Parse(value);
            }
            catch
            {
                Console.WriteLine("Float.Parse({0}) is not Float", value);
            }
        }
    }
}
