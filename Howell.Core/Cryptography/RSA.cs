using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Howell.Cryptography
{
    /// <summary>
    /// RSA加密算法
    /// </summary>
    public class RSA
    {
        #region
        /// <summary>  
        /// RSA获取公钥私钥  
        /// </summary>  
        /// <param name="publicKey">输出公钥</param>  
        /// <param name="privateKey">输出密钥</param>  
        public static void CreateKey(out string publicKey, out string privateKey)
        {
            CreateKey(2048, out publicKey, out privateKey);
        }
        /// <summary>  
        /// RSA获取公钥私钥  
        /// </summary>  
        /// <param name="keySize">公钥密钥的位长，该值会影响最大可加密字节长度</param>
        /// <param name="publicKey">输出公钥</param>  
        /// <param name="privateKey">输出密钥</param>  
        public static void CreateKey(Int32 keySize, out string publicKey, out string privateKey)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(keySize);
            publicKey = RSA.ToXmlString(false);
            privateKey = RSA.ToXmlString(true);
        }
        /// <summary>
        /// 创建RSA加密算法对象
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="privateKey">私钥</param>
        public RSA(String publicKey,String privateKey)            
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }
        /// <summary>
        /// 公钥
        /// </summary>
        public String PublicKey { get; set; }
        /// <summary>
        /// 私钥
        /// </summary>
        public String PrivateKey { get; set; }

        /// <summary>  
        /// RSA加密  
        /// </summary>  
        /// <param name="source"></param>  
        /// <returns></returns>  
        public String Encrypt(String source)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(this.PublicKey);
                
                //str_PublicKey = Convert.ToBase64String(RSA.ExportCspBlob(false));  
                //str_PrivateKey = Convert.ToBase64String(RSA.ExportCspBlob(true));  
                byte[] DataToEncrypt = Encoding.UTF8.GetBytes(source);

                byte[] bs = RSA.Encrypt(DataToEncrypt, false);
                //str_PublicKey = Convert.ToBase64String(RSA.ExportCspBlob(false));  
                //str_PrivateKey = Convert.ToBase64String(RSA.ExportCspBlob(true));  
                string encrypttxt = Convert.ToBase64String(bs);

                return encrypttxt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>  
        /// RSA解密  
        /// </summary>  
        /// <param name="strRSA"></param>  
        /// <returns></returns>  
        public String Decrypt(string strRSA)
        {
            try
            {
                byte[] DataToDecrypt = Convert.FromBase64String(strRSA);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                RSA.FromXmlString(this.PrivateKey);
                //byte[] bsPrivatekey = Convert.FromBase64String(str_PrivateKey);  
                //RSA.ImportCspBlob(bsPrivatekey);  

                byte[] bsdecrypt = RSA.Decrypt(DataToDecrypt, false);

                string strRE = Encoding.UTF8.GetString(bsdecrypt);
                return strRE;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        #endregion  
    }
}
