using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Time
{
    /// <summary>
    /// Virtual DateTime 是一个根据构造函数传入的远端时间模拟其在本地的变换
    /// </summary>
    /// <remarks>
    /// 如： IP摄像机的时间
    /// </remarks>
    public class RemoteDateTime
    {
        private DateTime RemoteBEG;
        private DateTime LocalBEG;
        private TimeSpan TS;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="remoteNow"></param>
        public RemoteDateTime(DateTime remoteNow)
        {
            this.RemoteBEG = remoteNow;
            this.LocalBEG = DateTime.Now;
            TS = this.RemoteBEG - this.LocalBEG;
        }

        /// <summary>
        /// 获取远端的现在时间
        /// </summary>
        public DateTime RemoteNow
        {
            get
            {
                return  DateTime.Now + TS;
            }
        }

    }
}
