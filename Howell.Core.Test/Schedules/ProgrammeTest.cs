using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Howell.Schedules
{
    class ProgrammeTest
    {
        private static System.Threading.EventWaitHandle WaitHandle = null;
        public static void Test()
        {
            Console.WriteLine("++++++++++++++++ProgrammeTest++++++++++++++++++");
            using (WaitHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset))
            {
                DateTime triggerTime = DateTime.Now.AddSeconds(2);
                using (Programme prog = new Programme())
                {
                    prog.Reminding += new EventHandler<ProgrammeRemindingEventArgs>(prog_Reminding);
                    prog.Items.Add("1", new PlanTime(triggerTime.Hour, triggerTime.Minute, triggerTime.Second), "新闻联播");
                    prog.Items.Add(new ProgrammeItem("2", new PlanTime(triggerTime.AddSeconds(2)), "二人转 001"));
                    prog.Items.Add("3", new PlanTime(triggerTime.AddDays(1)), "二人转 002");
                    DateTime date = DateTime.Now.Date;
                    ReadOnlyCollection<ProgrammeItem> collection = prog.GetDateProgrammeItems(date);
                    Console.WriteLine("Today's Programme");
                    foreach (var item in collection)
                    {
                        Console.WriteLine("{0} {1}", item.Content, item.PlannedTime.ToDateTime(date));
                    }
                    Console.WriteLine("====================================");
                    Console.WriteLine("Waiting for {0}", prog.Items["1"].Content);
                    WaitHandle.WaitOne();
                    Console.WriteLine("Waiting for {0}", prog.Items["2"].Content);
                    WaitHandle.WaitOne();
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadKey();
                }
            }
        }
        static void prog_Reminding(object sender, ProgrammeRemindingEventArgs e)
        {
            Console.WriteLine("{0} {1}", e.Item.Content, e.Item.PlannedTime);
            WaitHandle.Set();
        }
    }
}
