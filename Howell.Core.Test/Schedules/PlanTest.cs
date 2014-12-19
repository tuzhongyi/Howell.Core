using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Schedules
{
    class PlanTest
    {
        private static System.Threading.EventWaitHandle WaitHandle = null;
        public static void Test()
        {
            using (WaitHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset))
            {
                //Plan_Test();
                PlanPeriod_Test();
            }
        }
        private static void Plan_Test()
        {
            Console.WriteLine("++++++++++++++++PlanTest++++++++++++++++++");
            Int32 times = 3;
            DateTime time = DateTime.Now.AddSeconds(1);
            using (Plan plan = new Plan(time.Hour, time.Minute, time.Second))
            {
                plan.RepeatInterval = TimeSpan.FromSeconds(5);
                plan.RepeatTimes = 3;
                plan.Enabled = false;
                plan.Reminding += new EventHandler<EventArgs>(plan_Reminding);
                while (times-- > 0)
                {
                    plan.Enabled = true;
                    WaitHandle.WaitOne();
                }
                Console.WriteLine("Press enter to continue.");
                Console.ReadKey();
                foreach (var item in plan.RemindedRecords)
                {
                    Console.WriteLine("Reminded at {0}.{0:ffff}", item);
                }
            }
        }
        
        private static void plan_Reminding(object sender, EventArgs e)
        {
            Console.WriteLine("PlanReminding Time:{0}.{0:ffff}", DateTime.Now);
            WaitHandle.Set();
        }

        private static void PlanPeriod_Test()
        {
            Console.WriteLine("++++++++++++++++PlanPeriodTest++++++++++++++++++");
            DateTime beginTime = DateTime.Now.AddSeconds(1);
            DateTime endTime = DateTime.Now.AddSeconds(2);
            using (PlanPeriod plan = new PlanPeriod(beginTime.Hour, beginTime.Minute, beginTime.Second, endTime.Hour, endTime.Minute, endTime.Second))
            {
                plan.Beginning += new EventHandler<EventArgs>(plan_Beginning);
                plan.Ending += new EventHandler<EventArgs>(plan_Ending);
                //    DateTime beginTime1 = DateTime.Now.AddSeconds(3);
                //    DateTime endTime1 = DateTime.Now.AddSeconds(4);
                //PlanPeriod plan1 = new PlanPeriod(beginTime1.Hour, beginTime1.Minute, beginTime1.Second, endTime1.Hour, endTime1.Minute, endTime1.Second);
                //plan1.Beginning += new EventHandler<EventArgs>(plan_Beginning);
                //plan1.Ending += new EventHandler<EventArgs>(plan_Ending);
                Console.WriteLine("Wait for beginning event raise.");
                WaitHandle.WaitOne();
                Console.WriteLine("Wait for ending event raise.");
                WaitHandle.WaitOne();
                Console.WriteLine("Press enter to continue.");
                Console.ReadKey();
            }
        }

        static void plan_Ending(object sender, EventArgs e)
        {
            Console.WriteLine("PlanEnding Time:{0}.{0:ffff}", DateTime.Now);
            WaitHandle.Set();
        }

        static void plan_Beginning(object sender, EventArgs e)
        {
            Console.WriteLine("PlanBeginning Time:{0}.{0:ffff}", DateTime.Now);
            WaitHandle.Set();
        }
    }
}
