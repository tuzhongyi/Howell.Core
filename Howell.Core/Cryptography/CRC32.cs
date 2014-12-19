using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Cryptography
{
    /// <summary>
    /// CRC32 校验工具 (x32 + x26 + x23 + x22 + x16 + x12 + x11 + x10 + x8 + x7 + x5 + x4 + x2 + x1 + 1)
    /// </summary>
    public class CRC32 : IDisposable
    {
        private const UInt32 P_32 = 0xEDB88320;
        #region Table
        private static UInt32[] crc_tab32 = new UInt32[256];
        private static Boolean crc_tab32_init = false;
        private static void init_crc32_tab()
        {
            int i, j;
            UInt32 crc;
            for (i = 0; i < 256; i++)
            {
                crc = (UInt32)i;
                for (j = 0; j < 8; j++)
                {

                    if ((UInt32)(crc & 0x00000001) > 0) crc = (crc >> 1) ^ P_32;
                    else crc = (UInt32)(crc >> 1);
                }
                crc_tab32[i] = crc;
            }
            crc_tab32_init = true;
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public CRC32()
        {
        }
        /// <summary>
        /// 计算CRC32校验值
        /// </summary>
        /// <param name="buffer">校验数据</param>
        /// <returns>返回校验结果</returns>
        public UInt32 Compute(byte[] buffer)
        {
            return Compute(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 计算CRC32校验值
        /// </summary>
        /// <param name="buffer">校验数据</param>
        /// <param name="offset">数据偏移</param>
        /// <param name="count">数据数目</param>
        /// <returns>返回校验结果</returns>
        public UInt32 Compute(byte[] buffer, int offset, int count)
        {
            return ComputeCRC32(buffer, offset, count);
        }
        /// <summary>
        /// 计算CRC32校验值
        /// </summary>
        /// <param name="s">校验字符串</param>
        /// <returns>返回校验结果</returns>
        public UInt32 Compute(String s)
        {
            return Compute(System.Text.Encoding.ASCII.GetBytes(s));
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
        }

        private UInt32 update_crc_32(UInt32 crc, Byte c)
        {
            UInt32 tmp, long_c;
            long_c = (UInt32)(0x000000ff & (UInt32)c);
            if (!crc_tab32_init) init_crc32_tab();
            tmp = crc ^ long_c;
            crc = (UInt32)((crc >> 8) ^ crc_tab32[tmp & 0xff]);
            return crc;
        }
        #region Compute
        private UInt32 ComputeCRC32(byte[] buffer, int offset, int count)
        {
            UInt32 crc_32 = 0xffffffff;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_32 = update_crc_32(crc_32, buffer[offset + i]);
            }
            crc_32 ^= 0xffffffff;
            return crc_32;
        }
        #endregion
    }

}
