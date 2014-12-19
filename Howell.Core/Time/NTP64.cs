using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Time
{
    /// <summary>
    /// NTP64时间
    /// </summary>
    public class NTP64
    {
        /// <summary>
        /// 起点时间
        /// </summary>
        public readonly static DateTime Origin = new DateTime(1900, 1, 1, 0, 0, 0,0);

        private readonly static UInt32 TenMillions = 10000000;
        
        /// <summary>
        /// 1900-1-1 到现在的秒数
        /// </summary>
        public UInt32 Seconds { get; set; }
        /// <summary>
        /// 2的-32次方秒
        /// </summary>
        public UInt32 Fraction { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public NTP64 Now
        {
            get
            {
                return new NTP64(DateTime.Now);
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public NTP64()
        {
            this.Seconds = 0;
            this.Fraction = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seconds">秒数</param>
        /// <param name="fraction">小数部分</param>
        public NTP64(UInt32 seconds,UInt32 fraction)
        {
            this.Seconds = seconds;
            this.Fraction = fraction;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ntp64">NTP64时间</param>
        public NTP64(UInt64 ntp64)
        {
            this.Seconds = (UInt32)(ntp64 >> 32);
            this.Fraction = (UInt32)ntp64;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="datetime">系统时间</param>
        public NTP64(DateTime datetime)
        {
            TimeSpan ts = datetime - Origin;
            this.Seconds = (UInt32)ts.TotalSeconds;
            this.Fraction = (UInt32)(ts.Milliseconds * TenMillions);
        }
        /// <summary>
        /// 转换为UInt64 NTP64
        /// </summary>
        /// <returns>返回NTP64</returns>
        public UInt64 ToUInt64()
        {
            UInt64 result = this.Seconds;
            result = (result<<32) + this.Fraction;
            return result;
        }
    }
}
