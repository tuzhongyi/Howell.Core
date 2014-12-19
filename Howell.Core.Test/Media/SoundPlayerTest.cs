using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Howell.Media
{
    class SoundPlayerTest
    {
        public static void Test()
        {
            //using (SoundPlayer player = new SoundPlayer(@"C:\1.wav"))
            using (SoundPlayer player = new SoundPlayer(@"C:\1.wav"))
            {
                player.PlaySync();
                Console.WriteLine("State:{0}", player.GetState());
                player.Seek(new TimeSpan(0, 0, 4));
                Console.WriteLine("State:{0}", player.GetState());
                Thread.Sleep(5000);
                Console.WriteLine("PlayPosition: {0}/{1}.", player.GetPosition(), player.GetLength());
                Console.WriteLine("State:{0}", player.GetState());
                //player.Pause();
                //Console.WriteLine("State:{0}", player.GetState());
                //Thread.Sleep(5000);
                //player.Play();
                //Console.WriteLine("State:{0}", player.GetState());
                //Thread.Sleep(5000);
                player.Stop();
                //Console.WriteLine("State:{0}", player.GetState());
            }
        }
    }
}
