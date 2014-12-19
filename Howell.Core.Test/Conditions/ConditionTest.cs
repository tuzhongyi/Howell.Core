using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Conditions
{
    static class ConditionTest
    {
        public static void Test()
        {
            EnumRange();
            StringMatchPattern();
        }
        public static void EnumRange()
        {
            try
            {                
                Condition.Ensures<ConsoleColor>(ConsoleColor.Black, "color").IsInRange(ConsoleColor.Blue, ConsoleColor.White);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void StringMatchPattern()
        {
            try
            {               
                Condition.WithExceptionOnFailure<FormatException>().Requires<String>("19921a", "number").MatchPattern(@"[0-9]$","match pattern failed.");
                //Condition.Ensures<String>("19921a", "number").MatchPattern(@"[0-9]$");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
