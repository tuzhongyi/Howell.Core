using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Howell.Cryptography
{
    /// <summary>
    /// DES对称加密Password
    /// </summary>
    public class DESPassword
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="rgbKey">DES加密算法密钥</param>
        /// <param name="rgbIV">DES加密算法的初始化向量</param>
        public DESPassword(Byte[] rgbKey, Byte[] rgbIV)
        {
            this.Key = rgbKey;
            this.IV = rgbIV;
        }
        /// <summary>
        /// DES加密算法密钥
        /// </summary>
        Byte[] Key { get; set; }
        /// <summary>
        /// DES加密算法的初始化向量 (8字节)
        /// </summary>
        Byte[] IV { get; set; }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            //创建随机Key与随机IV
            Byte [] bytes = Guid.NewGuid().ToByteArray();
            Byte[] rgbKey = new Byte[8];
            Byte[] rgbIV = new Byte[8];
            Array.Copy(bytes, 0, rgbKey,0, 8);
            Array.Copy(bytes, 8, rgbIV, 0, 8);
            DES valueDES = new DES(rgbKey, rgbIV);
            String strKey = String.Empty;
            String strIV = String.Empty;
            for (Int32 i = 0; i < 8; ++i)
            {
                strKey += rgbKey[i].ToString("X2");
                strIV += rgbIV[i].ToString("X2");
            }
            String strEncryptValue = valueDES.Encrypt(value);
            String content = strKey + strIV + strEncryptValue;
            DES keyDES = new DES(Key, IV);
            return keyDES.Encrypt(content);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Decrypt(string value)
        {
            DES keyDES = new DES(Key, IV);
            String content = keyDES.Decrypt(value);
            String strKey = content.Substring(0, 16);
            String strIV = content.Substring(16, 32);
            Byte[] rgbKey = new Byte[8];
            Byte[] rgbIV = new Byte[8];
            for (Int32 i = 0; i < 8; ++i)
            {
                rgbKey[i] = Convert.ToByte(strKey.Substring(i * 2, 2), 16);
                rgbIV[i] = Convert.ToByte(strIV.Substring(i * 2, 2), 16);
            }
            DES valueDES = new DES(rgbKey, rgbIV);
            return valueDES.Decrypt(content.Substring(32));            
        }
    }
}
