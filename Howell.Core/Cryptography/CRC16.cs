using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Howell.Cryptography
{
    /// <summary>
    /// CRC16类型
    /// </summary>
    public enum CRC16Algorithm
    {
        /// <summary>
        /// 标准 CRC16校验 (x16 + x15 + x2 + 1)
        /// </summary>
        CRC16,
        /// <summary>
        /// MODBUS CRC16校验 (x16 + x15 + x2 + 1)
        /// </summary>
        CRC16_MODBUS,
        /// <summary>
        /// SICK CRC16校验
        /// </summary>
        CRC16_SICK,
        /// <summary>
        /// CCITT CRC16校验 (x16 + x12 + x5 + 1)
        /// </summary>
        CRC16_CCITT_0000, 
        /// <summary>
        /// CCITT CRC16校验 (x16 + x12 + x5 + 1)
        /// </summary>
        CRC16_CCITT_FFFF,
        /// <summary>
        /// CCITT CRC16校验 (x16 + x12 + x5 + 1)
        /// </summary>
        CRC16_CCITT_1D0F,
        /// <summary>
        /// KERMIT CRC16校验
        /// </summary>
        CRC16_CCITT_KERMIT,
        /// <summary>
        /// DNP CRC16校验 (x16 + x13 + x12 + x11 + x10 + x8 + x6 + x5 + x2 + 1)
        /// </summary>
        CRC16_DNP,

    }
    /// <summary>Implements a 16-bits cyclic redundancy check (CRC) hash algorithm.</summary>
    /// <remarks>This class is not intended to be used for security purposes. For security applications use MD5, SHA1, SHA256, SHA384, 
    /// or SHA512 in the System.Security.Cryptography namespace.</remarks>
    public class CRC16 : IDisposable
    {
        private const UInt16 P_16 = 0xA001;
        private const UInt16 P_CCITT = 0x1021;
        private const UInt16 P_DNP = 0xA6BC;
        private const UInt16 P_KERMIT = 0x8408;
        private const UInt16 P_SICK = 0x8005;
        #region Table
        private static UInt16[] crc_tab16 = new UInt16[256];
        private static UInt16[] crc_tabccitt = new UInt16[256];
        private static UInt16[] crc_tabdnp = new UInt16[256];
        private static UInt16[] crc_tabkermit = new UInt16[256];
        private static Boolean crc_tab16_init = false;
        private static Boolean crc_tabccitt_init = false;
        private static Boolean crc_tabdnp_init = false;
        private static Boolean crc_tabkermit_init = false;
        private static void init_crc16_tab()
        {
            int i, j;
            UInt16 crc, c;

            for (i = 0; i < 256; i++)
            {
                crc = 0;
                c = (UInt16)i;
                for (j = 0; j < 8; j++)
                {
                    if ((UInt16)((crc ^ c) & 0x0001) > 0) crc = (UInt16)((crc >> 1) ^ P_16);
                    else crc = (UInt16)(crc >> 1);
                    c = (UInt16)(c >> 1);
                }
                crc_tab16[i] = crc;
            }
            crc_tab16_init = true;
        }
        private static void init_crcccitt_tab()
        {
            int i, j;
            UInt16 crc, c;
            for (i = 0; i < 256; i++)
            {
                crc = 0;
                c = (UInt16)(((UInt16)i) << 8);
                for (j = 0; j < 8; j++)
                {
                    if ((UInt16)((crc ^ c) & 0x8000) > 0) crc = (UInt16)((crc << 1) ^ P_CCITT);
                    else crc = (UInt16)(crc << 1);
                    c = (UInt16)(c << 1);
                }
                crc_tabccitt[i] = crc;
            }
            crc_tabccitt_init = true;
        }
        private static void init_crcdnp_tab()
        {
            int i, j;
            UInt16 crc, c;
            for (i = 0; i < 256; i++)
            {
                crc = 0;
                c = (UInt16)i;
                for (j = 0; j < 8; j++)
                {
                    if ((UInt16)((crc ^ c) & 0x0001) > 0) crc = (UInt16)((crc >> 1) ^ P_DNP);
                    else crc = (UInt16)(crc >> 1);
                    c = (UInt16)(c >> 1);
                }
                crc_tabdnp[i] = crc;
            }
            crc_tabdnp_init = true;
        }
        private static void init_crckermit_tab()
        {
            int i, j;
            UInt16 crc, c;
            for (i = 0; i < 256; i++)
            {
                crc = 0;
                c = (UInt16)i;
                for (j = 0; j < 8; j++)
                {
                    if ((UInt16)((crc ^ c) & 0x0001) > 0) crc = (UInt16)((crc >> 1) ^ P_KERMIT);
                    else crc = (UInt16)(crc >> 1);
                    c = (UInt16)(c >> 1);
                }
                crc_tabkermit[i] = crc;
            }
            crc_tabkermit_init = true;
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public CRC16()
            : this(CRC16Algorithm.CRC16)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">算法</param>
        public CRC16(CRC16Algorithm algorithm)
        {
            Algorithm = algorithm;
        }
        /// <summary>
        /// CRC16算法
        /// </summary>
        public CRC16Algorithm Algorithm
        {
            get; private set;
        }
        /// <summary>
        /// 计算CRC16校验值
        /// </summary>
        /// <param name="buffer">校验数据</param>
        /// <returns>返回校验结果</returns>
        public UInt16 Compute(byte[] buffer)
        {
            return Compute(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 计算CRC16校验值
        /// </summary>
        /// <param name="buffer">校验数据</param>
        /// <param name="offset">数据偏移</param>
        /// <param name="count">数据数目</param>
        /// <returns>返回校验结果</returns>
        public UInt16 Compute(byte[] buffer, int offset, int count)
        {
            switch (Algorithm)
            {
                case CRC16Algorithm.CRC16:
                    return ComputeCRC16(buffer, offset, count);
                case CRC16Algorithm.CRC16_MODBUS:
                    return ComputeCRC16_MODBUS(buffer, offset, count);
                case CRC16Algorithm.CRC16_SICK:
                    return ComputeCRC16_SICK(buffer, offset, count);
                case CRC16Algorithm.CRC16_CCITT_0000:
                    return ComputeCRC16_CCITT_0000(buffer, offset, count);
                case CRC16Algorithm.CRC16_CCITT_FFFF:
                    return ComputeCRC16_CCITT_FFFF(buffer, offset, count);
                case CRC16Algorithm.CRC16_CCITT_1D0F:
                    return ComputeCRC16_CCITT_1D0F(buffer, offset, count);
                case CRC16Algorithm.CRC16_CCITT_KERMIT:
                    return ComputeCRC16_CCITT_KERMIT(buffer, offset, count);
                case CRC16Algorithm.CRC16_DNP:
                    return ComputeCRC16_DNP(buffer, offset, count);
                default:
                    return 0;
            }
        }
        /// <summary>
        /// 计算CRC16校验值
        /// </summary>
        /// <param name="s">校验字符串</param>
        /// <returns>返回校验结果</returns>
        public UInt16 Compute(String s)
        {
            return Compute(System.Text.Encoding.ASCII.GetBytes(s));
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
        }
        #region Update CRC16
        private UInt16 update_crc_16(UInt16 crc, Byte c)
        {
            UInt16 tmp, short_c;
            short_c = (UInt16)(0x00ff & (UInt16)c);
            if (!crc_tab16_init) init_crc16_tab();
            tmp = (UInt16)(crc ^ short_c);
            crc = (UInt16)((crc >> 8) ^ crc_tab16[tmp & 0xff]);
            return crc;
        }
        private UInt16 update_crc_ccitt(UInt16 crc, Byte c)
        {
            UInt16 tmp, short_c;
            short_c = (UInt16)(0x00ff & c);
            if (!crc_tabccitt_init) init_crcccitt_tab();
            tmp = (UInt16)((crc >> 8) ^ short_c);
            crc = (UInt16)((crc << 8) ^ crc_tabccitt[tmp]);
            return crc;
        }
        private UInt16 update_crc_dnp(UInt16 crc, Byte c)
        {
            UInt16 tmp, short_c;
            short_c = (UInt16)(0x00ff & (UInt16)c);
            if (!crc_tabdnp_init) init_crcdnp_tab();
            tmp = (UInt16)(crc ^ short_c);
            crc = (UInt16)((crc >> 8) ^ crc_tabdnp[tmp & 0xff]);
            return crc;
        }
        private UInt16 update_crc_kermit(UInt16 crc, Byte c)
        {
            UInt16 tmp, short_c;
            short_c = (UInt16)(0x00ff & (UInt16)c);
            if (!crc_tabkermit_init) init_crckermit_tab();
            tmp = (UInt16)(crc ^ short_c);
            crc = (UInt16)((crc >> 8) ^ crc_tabkermit[tmp & 0xff]);
            return crc;
        }
        private UInt16 update_crc_sick(UInt16 crc, Byte c, Byte prev_byte)
        {
            UInt16 short_c, short_p;
            short_c = (UInt16)(0x00ff & (UInt16)c);
            short_p = (UInt16)((0x00ff & (UInt16)prev_byte) << 8);
            if ((UInt16)(crc & 0x8000) > 0) crc = (UInt16)((crc << 1) ^ P_SICK);
            else crc = (UInt16)(crc << 1);
            crc &= 0xffff;
            crc ^= (UInt16)(short_c | short_p);
            return crc;
        }
        #endregion
        #region Compute
        private UInt16 ComputeCRC16(byte[] buffer, int offset, int count)
        {
            UInt16 crc_16 = 0;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_16 = update_crc_16(crc_16, buffer[offset + i]);
            }
            return crc_16;
        }
        private UInt16 ComputeCRC16_MODBUS(byte[] buffer, int offset, int count)
        {
            UInt16 crc_16_modbus = 0xffff;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_16_modbus = update_crc_16(crc_16_modbus, buffer[offset + i]);
            }
            return crc_16_modbus;
        }
        private UInt16 ComputeCRC16_DNP(byte[] buffer, int offset, int count)
        {
            UInt16 crc_dnp = 0;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_dnp = update_crc_dnp(crc_dnp, buffer[offset + i]);
            } 
            crc_dnp = (UInt16)~crc_dnp;
            UInt16 low_byte = (UInt16)((crc_dnp & 0xff00) >> 8);
            UInt16 high_byte = (UInt16)((crc_dnp & 0x00ff) << 8);
            crc_dnp = (UInt16)(low_byte | high_byte);
            return crc_dnp;
        }
        private UInt16 ComputeCRC16_SICK(byte[] buffer, int offset, int count)
        {
            UInt16 crc_sick = 0;
            Byte   prev_byte = 0;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_sick = update_crc_sick(crc_sick, buffer[offset + i], prev_byte);
                prev_byte = buffer[offset + i];
            }
            UInt16 low_byte = (UInt16)((crc_sick & 0xff00) >> 8);
            UInt16 high_byte = (UInt16)((crc_sick & 0x00ff) << 8);
            crc_sick = (UInt16)(low_byte | high_byte);
            return crc_sick;
        }
        private UInt16 ComputeCRC16_CCITT_0000(byte[] buffer, int offset, int count)
        {
            UInt16 crc_ccitt_0000 = 0;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_ccitt_0000 = update_crc_ccitt(crc_ccitt_0000, buffer[offset + i]);
            }
            return crc_ccitt_0000;
        }
        private UInt16 ComputeCRC16_CCITT_FFFF(byte[] buffer, int offset, int count)
        {
            UInt16 crc_ccitt_ffff = 0xFFFF;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_ccitt_ffff = update_crc_ccitt(crc_ccitt_ffff, buffer[offset + i]);
            }
            return crc_ccitt_ffff;
        }
        private UInt16 ComputeCRC16_CCITT_1D0F(byte[] buffer, int offset, int count)
        {
            UInt16 crc_ccitt_1d0f = 0x1D0F;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_ccitt_1d0f = update_crc_ccitt(crc_ccitt_1d0f, buffer[offset + i]);
            }
            return crc_ccitt_1d0f;
        }
        private UInt16 ComputeCRC16_CCITT_KERMIT(byte[] buffer, int offset, int count)
        {
            UInt16 crc_kermit = 0;
            for (Int32 i = 0; i < count; ++i)
            {
                crc_kermit = update_crc_kermit(crc_kermit, buffer[offset + i]);
            }
            UInt16 low_byte = (UInt16)((crc_kermit & 0xff00) >> 8);
            UInt16 high_byte = (UInt16)((crc_kermit & 0x00ff) << 8);
            crc_kermit = (UInt16)(low_byte | high_byte);
            return crc_kermit;
        }
        #endregion


    }
}
