using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Media
{
    /// <summary>
    /// 音频编码格式
    /// </summary>
    public enum AudioCodedFormat
    {
        /// <summary>
        /// 无音频数据
        /// </summary>
        None = 0,
        /// <summary>
        /// G711
        /// </summary>
        G711 = 1,
        /// <summary>
        /// G722
        /// </summary>
        G722 = 2,
        /// <summary>
        /// G723
        /// </summary>
        G723 = 3,
        /// <summary>
        /// G726
        /// </summary>
        G726 = 4,
        /// <summary>
        /// AAC
        /// </summary>
        AAC = 5,
        /// <summary>
        /// PCM-U律
        /// </summary>
        PCMU = 6,
        /// <summary>
        /// PCM-A律
        /// </summary>
        PCMA = 7,
    }
    /// <summary>
    /// 音频格式信息
    /// </summary>
    public class AudioFormatExtensible
    {
        /// <summary>
        /// 音频编码格式
        /// </summary>
        public AudioCodedFormat FormatTag { get; set; }
        /// <summary>
        /// Gets or sets the number of channels in the data. 
        /// Mono            1
        /// Stereo          2
        /// Dual            2 (2 Mono channels)
        /// </summary>
        /// <remarks>
        /// Silverlight 2 only supports stereo output and folds down higher
        /// numbers of channels to stereo.
        /// </remarks>
        public short Channels { get; set; }
        /// <summary>
        /// Gets or sets the sampling rate in hertz (samples per second)
        /// </summary>
        public int SamplesPerSec { get; set; }
        /// <summary>
        /// Gets or sets the average data-transfer rate, in bytes per second, for the format.
        /// </summary>
        public int AverageBytesPerSecond { get; set; }
        /// <summary>
        /// Gets or sets the minimum size of a unit of data for the given format in Bytes.
        /// </summary>
        public short BlockAlign { get; set; }
        /// <summary>
        /// Gets or sets the number of bits in a single sample of the format's data.
        /// </summary>
        public short BitsPerSample { get; set; }
    }
}
