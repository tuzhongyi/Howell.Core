using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Time
{
    class DateTimeExtensionsTest
    {
        public static void Test()
        {            
            Console.WriteLine("+++++++++++++DateTime Extensions Testing++++++++++++");
            GetAge();
            GetDaysOfMonth();
            GetFirstDayOfMonth();
            GetLastDayOfMonth();
            GetFirstDayOfWeek();            
            GetLastDayOfWeek();
            GetWeekOfYear();
            IsToday();
            IsInRange();
        }

        private static void GetAge()
        {
            Console.WriteLine("-----------DateTime.GetAge-----------");
            DateTime birthOfDate = new DateTime(1983, 8, 9);
            Console.WriteLine(String.Format("Birth of date is: {0}. Today is date: {1}. Age is: {2}.", birthOfDate.ToShortDateString(), DateTime.Today.ToShortDateString(), birthOfDate.GetAge()));
        }
        private static void GetDaysOfMonth()
        {
            Console.WriteLine("-----------DateTime.GetDaysOfMonth-----------");
            Console.WriteLine(String.Format("Date is: {0}. DaysOfMonth is:{1}", DateTime.Now.ToShortDateString(), DateTime.Now.GetDaysOfMonth()));
        }
        private static void GetFirstDayOfMonth()
        {
            Console.WriteLine("-----------DateTime.GetFirstDayOfMonth-----------");
            Console.WriteLine(String.Format("Date is: {0}. FirstDayOfMonth is:{1}", DateTime.Now.ToShortDateString(), DateTime.Now.GetFirstDayOfMonth().ToShortDateString()));
        }
        private static void GetLastDayOfMonth()
        {
            Console.WriteLine("-----------DateTime.GetLastDayOfMonth-----------");
            Console.WriteLine(String.Format("Date is: {0}. LastDayOfMonth is:{1}", DateTime.Now.ToShortDateString(), DateTime.Now.GetLastDayOfMonth().ToShortDateString()));
        }
        private static void GetFirstDayOfWeek()
        {
            Console.WriteLine("-----------DateTime.GetFirstDayOfWeek-----------");
            Console.WriteLine(String.Format("Date is: {0}. FirstDayOfWeek is:{1}", DateTime.Now.ToShortDateString(), DateTime.Now.GetFirstDayOfWeek().ToShortDateString()));
        }
        private static void GetLastDayOfWeek()
        {
            Console.WriteLine("-----------DateTime.GetLastDayOfWeek-----------");
            Console.WriteLine(String.Format("Date is: {0}. LastDayOfWeek is:{1}", DateTime.Now.ToShortDateString(), DateTime.Now.GetLastDayOfWeek().ToShortDateString()));
        }
        private static void GetWeekOfYear()
        {
            Console.WriteLine("-----------DateTime.GetWeekOfYear-----------");
            Console.WriteLine(String.Format("Date is: {0}. WeekOfYear is:{1}", DateTime.Now.ToShortDateString(), DateTime.Now.GetWeekOfYear()));
        }
        private static void IsToday()
        {            
            Console.WriteLine("-----------DateTime.IsToday-----------");
            Console.WriteLine(String.Format("Date is: {0}. Today is:{1}. IsToday is:{2}", DateTime.Now.ToShortDateString(), DateTime.Today.ToShortDateString(), DateTime.Now.IsToday()));
        }
        private static void IsInRange()
        {
            Console.WriteLine("-----------DateTime.IsInRange-----------");
            Console.WriteLine(String.Format("Date is: {0}. IsInRange is:{1}", DateTime.Now.ToShortDateString(), DateTime.Now.IsInRange(DateTime.Now, DateTime.Now)));
        }
        
    }
}
