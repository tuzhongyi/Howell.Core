using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Schedules
{
    class PlanTimeTest
    {
        public static void Test()
        {
            Console.WriteLine("+++++++++++++++++PlanTimeTest+++++++++++++++++");
            PlanTime time = new PlanTime(2013, 11, 12, 4, 26, 12);
            {
                Console.WriteLine("-----------------OneOff-------------------");
                PlanTime time2 = PlanTime.Parse("2013-01-12 02:03:40");
                Console.WriteLine("String:{0}", time2.ToString());
                Console.WriteLine("ToDateTime:{0}", time2.ToDateTime());
                Console.WriteLine("GetNextTime:{0}", time2.GetNextTime());
            }
            {
                Console.WriteLine("-----------------Daily-------------------");
                PlanTime time2 = PlanTime.Parse("03:16:05");
                Console.WriteLine("String:{0}", time2.ToString());
                Console.WriteLine("ToDateTime:{0}", time2.ToDateTime());
                Console.WriteLine("GetNextTime:{0}", time2.GetNextTime());
            }
            {
                Console.WriteLine("-----------------Weekly-------------------");
                PlanTime time2 = PlanTime.Parse("23:58:05 Sunday");
                Console.WriteLine("String:{0}", time2.ToString());
                Console.WriteLine("ToDateTime:{0}", time2.ToDateTime());
                Console.WriteLine("GetNextTime:{0}", time2.GetNextTime());
            }
        }
    }
}
