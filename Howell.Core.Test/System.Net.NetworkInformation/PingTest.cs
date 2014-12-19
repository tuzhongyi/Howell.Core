using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net.NetworkInformation
{
    class PingTest
    {
        public static void Test()
        {
            PingTest1();
            MultiPingTest1();
        }
        private static void PingTest1()
        {
            Console.WriteLine("++++++++++++++++++System.Net.NetworkInformation.Ping++++++++++++++++++");
            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send("127.0.0.1", 5000, new Byte[32]);
                Console.WriteLine(String.Format("PingReplay.ToContentString:\r\n{0}", reply.ToContentString()));
            }
        }
        private static void MultiPingTest1()
        {
            Console.WriteLine("++++++++++++++++++System.Net.NetworkInformation.MultiPing++++++++++++++++++");
            System.Threading.EventWaitHandle handler = new Threading.EventWaitHandle(false, Threading.EventResetMode.AutoReset);
            using(MultiPing ping = new MultiPing(1000))
            {
                ping.SinglePingCompleted += new PingCompletedEventHandler(SinglePingCompleted);
                ping.MultiPingCompleted += new MultiPingCompletedEventHandler(MultiPingCompleted);
                ping.SendAsync("127.0.0.1", 3, handler);
                handler.WaitOne();
                Console.WriteLine("End Ping!");
            }
        }
        private static void SinglePingCompleted(Object sender,PingCompletedEventArgs e)
        {
            Console.WriteLine(e.Reply.ToContentString());
        }
        private static void MultiPingCompleted(Object sender,MultiPingCompletedEventArgs e)
        {
            Console.WriteLine(e.Reply.ToContentString());
            (e.UserState as System.Threading.EventWaitHandle).Set();
        }
    }
}
