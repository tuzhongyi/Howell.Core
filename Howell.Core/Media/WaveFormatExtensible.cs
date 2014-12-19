using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Howell.Media
{
    /// <summary>
    /// A managed representation of the multimedia WAVEFORMATEX structure
    /// declared in mmreg.h.
    /// </summary>
    /// <remarks>
    /// This was designed for usage in an environment where PInvokes are not
    /// allowed.
    /// </remarks>
    public class WaveFormatExtensible
    {
        /// <summary>
        /// Gets or sets the audio format type. A complete list of format tags can be
        /// found in the Mmreg.h header file.
        /// </summary>
        /// <remarks>
        /// wave_format_g723_adpcm 	0x0014 	/* antex electronics corporation */
        /// wave_format_antex_adpcme 	0x0033 	/* antex electronics corporation */
        /// wave_format_g721_adpcm 	0x0040 	/* antex electronics corporation */
        /// wave_format_aptx 	0x0025 	/* audio processing technology */
        /// wave_format_audiofile_af36 	0x0024 	/* audiofile, inc. */
        /// wave_format_audiofile_af10 	0x0026 	/* audiofile, inc. */
        /// wave_format_control_res_vqlpc 	0x0034 	/* control resources limited */
        /// wave_format_control_res_cr10 	0x0037 	/* control resources limited */
        /// wave_format_creative_adpcm 	0x0200 	/* creative labs, inc */
        /// wave_format_dolby_ac2 	0x0030 	/* dolby laboratories */
        /// wave_format_dspgroup_truespeech 	0x0022 	/* dsp group, inc */
        /// wave_format_digistd 	0x0015 	/* dsp solutions, inc. */
        /// wave_format_digifix 	0x0016 	/* dsp solutions, inc. */
        /// wave_format_digireal 	0x0035 	/* dsp solutions, inc. */
        /// wave_format_digiadpcm 	0x0036 	/* dsp solutions, inc. */
        /// wave_format_echosc1 	0x0023 	/* echo speech corporation */
        /// wave_format_fm_towns_snd 	0x0300 	/* fujitsu corp. */
        /// wave_format_ibm_cvsd 	0x0005 	/* ibm corporation */
        /// wave_format_oligsm 	0x1000 	/* ing c. olivettic., s.p.a. */
        /// wave_format_oliadpcm 	0x1001 	/* ing c. olivettic., s.p.a. */
        /// wave_format_olicelp 	0x1002 	/* ing c. olivettic., s.p.a. */
        /// wave_format_olisbc 	0x1003 	/* ing c. olivettic., s.p.a. */
        /// wave_format_oliopr 	0x1004 	/* ing c. olivettic., s.p.a. */
        /// wave_format_ima_adpcm 	(wave_form_dvi_adpcm) 	/* intel corporation */
        /// wave_format_dvi_adpcm 	0x0011 	/* intel corporation */
        /// wave_format_unknown 	0x0000 	/* microsoft corporation */
        /// wave_format_pcm 	0x0001 	/* microsoft corporation */
        /// wave_format_adpcm 	0x0002 	/* microsoft corporation */
        /// wave_format_alaw 	0x0006 	/* microsoft corporation */
        /// wave_format_mulaw 	0x0007 	/* microsoft corporation */
        /// wave_format_gsm610 	0x0031 	/* microsoft corporation */
        /// wave_format_mpeg 	0x0050 	/* microsoft corporation */
        /// wave_format_nms_vbxadpcm 	0x0038 	/* natural microsystems */
        /// wave_format_oki_adpcm 	0x0010 	/* oki */
        /// wave_format_sierra_adpcm 	0x0013 	/* sierra semiconductor corp */
        /// wave_format_sonarc 	0x0021 	/* speech compression */
        /// wave_format_mediaspace_adpcm 	0x0012 	/* videologic */
        /// wave_format_yamaha_adpcm 	0x0020 	/* yamaha corporation of america */
        /// </remarks>
        public short FormatTag { get; set; }
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
        /// <summary>
        /// Gets or sets the size in bytes of any extra format data added to the end of the
        /// WAVEFORMATEX structure.
        /// </summary>
        public short Size { get; set; }
        /// <summary>
        /// Returns a string representing the structure in little-endian 
        /// hexadecimal format.
        /// </summary>
        /// <remarks>
        /// The string generated here is intended to be passed as 
        /// CodecPrivateData for Silverlight 2's MediaStreamSource
        /// </remarks>
        /// <returns>
        /// A string representing the structure in little-endia hexadecimal
        /// format.
        /// </returns>
        public string ToHexString()
        {
            string s = string.Format(CultureInfo.InvariantCulture, "{0:X4}", FormatTag).ToLittleEndian();
            s += string.Format(CultureInfo.InvariantCulture, "{0:X4}", Channels).ToLittleEndian();
            s += string.Format(CultureInfo.InvariantCulture, "{0:X8}", SamplesPerSec).ToLittleEndian();
            s += string.Format(CultureInfo.InvariantCulture, "{0:X8}", AverageBytesPerSecond).ToLittleEndian();
            s += string.Format(CultureInfo.InvariantCulture, "{0:X4}", BlockAlign).ToLittleEndian();
            s += string.Format(CultureInfo.InvariantCulture, "{0:X4}", BitsPerSample).ToLittleEndian();
            s += string.Format(CultureInfo.InvariantCulture, "{0:X4}", Size).ToLittleEndian();
            return s;
        }
        /// <summary>
        /// Returns a string representing all of the fields in the object.
        /// </summary>
        /// <returns>
        /// A string representing all of the fields in the object.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "WAVEFORMATEX FormatTag: {0}, Channels: {1}, SamplesPerSec: {2}, AvgBytesPerSec: {3}, BlockAlign: {4}, BitsPerSample: {5}, Size: {6} ",
                this.FormatTag,
                this.Channels,
                this.SamplesPerSec,
                this.AverageBytesPerSecond,
                this.BlockAlign,
                this.BitsPerSample,
                this.Size);
        }
        /// <summary>
        /// 时标计算函数
        /// </summary>
        /// <param name="audioDataSize">输入的字节数</param>
        /// <returns></returns>
        public Int64 AudioDurationFromBufferSize(Int64 audioDataSize)
        {
            if (this.AverageBytesPerSecond == 0)
            {
                return 0;
            }
            return (Int64)(audioDataSize * 1000 * 1000 * 10) / this.AverageBytesPerSecond;
        }
        /// <summary>
        /// Helper function to align a block
        /// </summary>
        /// <param name="a">The value we want to align</param>
        /// <param name="b">The alignment value</param>
        /// <returns>A new aligned value</returns>
        private static int AlignUp(int a, int b)
        {
            int tmp = a + b - 1;
            return tmp - (tmp % b);
        }
    }
}
