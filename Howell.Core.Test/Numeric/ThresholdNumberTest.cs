using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Numeric
{
    class ThresholdNumberTest
    {
        public static void Test()
        {
            try
            {
                ThresholdNumber<Int32> number = new ThresholdNumber<Int32>(10, 0, 0);
                number.Increment();
                number.Decrement();
                number.Add(10);
                number.Subtract(-1);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
