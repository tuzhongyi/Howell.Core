using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Capability
{
    /// <summary>
    /// 时间段
    /// </summary>
    [Serializable]
    [XmlRootAttribute(ElementName = "TimeSpan", Namespace = Constants.Namespace)]
    public class TimeSpanCap
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TimeSpanCap()
            : this(new DateTimeCap(DateTime.Now), new DateTimeCap(DateTime.Now))
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="beginTime">开始</param>
        /// <param name="endTime">结束</param>
        public TimeSpanCap(DateTimeCap beginTime, DateTimeCap endTime)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <remarks>required</remarks>
        [XmlElementAttribute(Order = 1)]
        public DateTimeCap beginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <remarks>required</remarks>
        [XmlElementAttribute(Order = 2)]
        public DateTimeCap endTime { get; set; }
    }
}
