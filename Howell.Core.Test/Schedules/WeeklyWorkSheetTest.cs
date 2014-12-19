using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Schedules
{
    class WeeklyWorkSheetTest
    {

        private static System.Threading.EventWaitHandle WaitHandle = null;
        public static void Test()
        {
            Console.WriteLine("++++++++++++++++WeeklyWorkSheetTest++++++++++++++++++");
            using (WaitHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset))
            {
                DateTime beginTime = DateTime.Now.AddSeconds(-2);
                DateTime endTime = DateTime.Now.AddSeconds(3);
                using (WeeklyWorkSheet ws = new WeeklyWorkSheet())
                {
                    ws.DayOfWeekWorkSheetItemBeginning += new EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs>(ws_DayOfWeekWorkSheetItemBeginning);
                    ws.DayOfWeekWorkSheetItemEnding += new EventHandler<DayOfWeekWorkSheetItemEndingEventArgs>(ws_DayOfWeekWorkSheetItemEnding);

                    ws.Monday.Items.Add(beginTime, endTime);
                    ws.Monday.Items.Add(beginTime.AddSeconds(10), endTime.AddSeconds(10));
                    ws.CopyToAll(DayOfWeek.Monday);
                    int i = 0;
                    while (i <ws.Monday.Items.Count)
                    {
                        i++;
                        Console.WriteLine("====================================");
                        Console.WriteLine("Waiting for Beginning");
                        WaitHandle.WaitOne();
                        Console.WriteLine("Waiting for Ending");
                        WaitHandle.WaitOne();
                    }
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadKey();
                }
            }
        }

        static void ws_DayOfWeekWorkSheetItemEnding(object sender, DayOfWeekWorkSheetItemEndingEventArgs e)
        {
            WeeklyWorkSheet ws = (sender as WeeklyWorkSheet);
            Console.WriteLine("{0} [{1} - {2}] Ending", ws[e.DayOfWeek].Items[e.Index].Content, ws[e.DayOfWeek].Items[e.Index].BeginTime, ws[e.DayOfWeek].Items[e.Index].EndTime);
            WaitHandle.Set();
        }

        static void ws_DayOfWeekWorkSheetItemBeginning(object sender, DayOfWeekWorkSheetItemBeginningEventArgs e)
        {
            WeeklyWorkSheet ws = (sender as WeeklyWorkSheet);
            Console.WriteLine("{0} [{1} - {2}] Beginning", ws[e.DayOfWeek].Items[e.Index].Content, ws[e.DayOfWeek].Items[e.Index].BeginTime, ws[e.DayOfWeek].Items[e.Index].EndTime);
            WaitHandle.Set();
        }
    }
}
