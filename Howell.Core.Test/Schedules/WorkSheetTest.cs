using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Schedules
{
     class WorkSheetTest
     {
         private static System.Threading.EventWaitHandle WaitHandle = null;
         public static void Test()
         {
             Console.WriteLine("++++++++++++++++WorkSheetTest++++++++++++++++++");
             using (WaitHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset))
             {
                 DateTime beginTime = DateTime.Now.AddSeconds(2);
                 DateTime endTime = DateTime.Now.AddSeconds(3);
                 using (WorkSheet ws = new WorkSheet())
                 {
                     ws.WorkSheetItemBeginning += new EventHandler<WorkSheetItemBeginningEventArgs>(ws_WorkSheetItemBeginning);
                     ws.WorkSheetItemEnding += new EventHandler<WorkSheetItemEndingEventArgs>(ws_WorkSheetItemEnding);
                     ws.Items.Add(new WorkSheetItem(new PlanTime(beginTime.Hour, beginTime.Minute, beginTime.Second), new PlanTime(endTime.Hour, endTime.Minute, endTime.Second), "放风", true));
                     Console.WriteLine("====================================");
                     Console.WriteLine("Waiting for {0} Beginning", ws.Items[0].Content);
                     WaitHandle.WaitOne();
                     Console.WriteLine("Waiting for {0} Ending", ws.Items[0].Content);
                     WaitHandle.WaitOne();
                     Console.WriteLine("Press enter to continue.");
                     Console.ReadKey();
                 }
             }
         }

         static void ws_WorkSheetItemEnding(object sender, WorkSheetItemEndingEventArgs e)
         {
             WorkSheet ws = (sender as WorkSheet);
             Console.WriteLine("{0} [{1} - {2}] Ending", ws.Items[e.Index].Content, ws.Items[e.Index].BeginTime, ws.Items[e.Index].EndTime);
             WaitHandle.Set();
         }

         static void ws_WorkSheetItemBeginning(object sender, WorkSheetItemBeginningEventArgs e)
         {
             WorkSheet ws = (sender as WorkSheet);
             Console.WriteLine("{0} [{1} - {2}] Beginning", ws.Items[e.Index].Content, ws.Items[e.Index].BeginTime, ws.Items[e.Index].EndTime);
             WaitHandle.Set();
         }
    }
}
