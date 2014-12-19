using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Howell.IO;

namespace Howell.Net
{
    /// <summary>
    /// FTP 客户端
    /// </summary>
    public class FtpClient
    {
        /// <summary>
        /// 创建FTP客户端
        /// </summary>
        /// <param name="uriString">FTP 服务器地址</param>
        public FtpClient(String uriString)
            : this(new Uri(uriString))
        {
        }
        /// <summary>
        /// 创建FTP客户端
        /// </summary>
        /// <param name="uri">FTP 服务器地址</param>
        public FtpClient(Uri uri)
        {
            if (String.CompareOrdinal(uri.Scheme, System.Uri.UriSchemeFtp) != 0)
                throw new UriFormatException("Uri scheme is not ftp.");
            this.Uri = uri;
            this.Credentials = null;
        }
        /// <summary>
        /// FTP服务器地址
        /// </summary>
        public Uri Uri { get; private set; }
        /// <summary>
        /// 获取或设置用于与 FTP 服务器通信的凭据。
        /// </summary>
        public ICredentials Credentials { get; set; }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">FTP下的相对路径</param>        
        public void CreateDirectory(String path)
        {
            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, path));
            webRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            webRequest.Credentials = this.Credentials;
            using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode != FtpStatusCode.PathnameCreated)
                {
                    throw new FtpException(webResponse.StatusCode, webResponse.StatusDescription);
                }
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">FTP下的相对路径</param>
        public void DeleteDirectory(String path)
        {
            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, path));
            webRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            webRequest.Credentials = this.Credentials;
            using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode != FtpStatusCode.FileActionOK)
                {
                    throw new FtpException(webResponse.StatusCode, webResponse.StatusDescription);
                }
            }
        }
        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="path">FTP下的相对路径</param>
        /// <param name="renameTo">FTP下重命名的相对路径</param>
        public void RenameDirectory(String path, String renameTo)
        {
            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, path));
            webRequest.Method = WebRequestMethods.Ftp.Rename;
            webRequest.Credentials = this.Credentials;
            webRequest.RenameTo = renameTo;
            using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode != FtpStatusCode.FileActionOK)
                {
                    throw new FtpException(webResponse.StatusCode, webResponse.StatusDescription);
                }
            }
        }
        /// <summary>
        /// 判断指定的文件夹是否已存在
        /// </summary>
        /// <param name="path">FTP路径</param>
        /// <returns>已存在返回True，否则返回False。</returns>
        public Boolean DirectoryExists(String path)
        {
            bool result = false;
            try
            {
                String ftpPath = PathExtensions.GetPathUpper(path);
                String directory = PathExtensions.GetPathLeaf(path);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, ftpPath));
                webRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                webRequest.Credentials = this.Credentials;
                //获取FTP服务器的响应
                using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.Default))
                    {
                        StringBuilder str = new StringBuilder();
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            str.Append(line);
                            str.Append("|");
                            line = sr.ReadLine();
                        }
                        string[] datas = str.ToString().Split('|');
                        for (int i = 0; i < datas.Length; i++)
                        {
                            if (datas[i].Contains("<DIR>"))
                            {
                                int index = datas[i].IndexOf("<DIR>");
                                string name = datas[i].Substring(index + 5).Trim();
                                if (name == directory)
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="ftpPath">FTP文件路径</param>
        public void UploadFile(String filePath, String ftpPath)
        {

            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, ftpPath));
            webRequest.Method = WebRequestMethods.Ftp.UploadFile;
            webRequest.Credentials = this.Credentials;
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                using (Stream fileStream = File.Open(filePath, FileMode.Open))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while (true)
                    {
                        bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                            break;
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode != FtpStatusCode.ClosingData)
                {
                    throw new FtpException(webResponse.StatusCode, webResponse.StatusDescription);
                }
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="ftpPath">FTP文件路径</param>
        public void DownloadFile(String filePath, String ftpPath)
        {
            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, ftpPath));
            webRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            webRequest.Credentials = this.Credentials;
            using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
            {

                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    using (Stream fileStream = File.Create(filePath))
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        while (true)
                        {
                            bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                                break;
                            fileStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">FTP文件路径</param>
        public void DeleteFile(String path)
        {

            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, path));
            webRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            webRequest.Credentials = this.Credentials;
            using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode != FtpStatusCode.FileActionOK)
                {
                    throw new FtpException(webResponse.StatusCode, webResponse.StatusDescription);
                }
            }
        }
        /// <summary>
        /// 获取FTP文件长度
        /// </summary>
        /// <param name="path">FTP文件路径</param>
        /// <returns>返回文件长度。</returns>
        public Int64 GetFileSize(String path)
        {
            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, path));
            webRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            webRequest.Credentials = this.Credentials;
            using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode != FtpStatusCode.FileStatus)
                {
                    throw new FtpException(webResponse.StatusCode, webResponse.StatusDescription);
                }
                return webResponse.ContentLength;
            }
        }
        /// <summary>
        /// 判断FTP文件是否存在
        /// </summary>
        /// <param name="path">FTP文件路径</param>
        /// <returns>已存在返回True，否则返回False。</returns>
        public Boolean FileExists(String path)
        {
            bool result = false;
            try
            {
                String ftpPath = Path.GetDirectoryName(path);
                String fileName = Path.GetFileName(path);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}", Uri, ftpPath));
                webRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                webRequest.Credentials = this.Credentials;
                //获取FTP服务器的响应
                using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.Default))
                    {
                        StringBuilder str = new StringBuilder();
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            str.Append(line);
                            str.Append("|");
                            line = sr.ReadLine();
                        }
                        string[] datas = str.ToString().Split('|');
                        for (int i = 0; i < datas.Length; i++)
                        {
                            String[] items = datas[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length > 0 && datas[i].Contains("<DIR>") == false)
                            {
                                if (items[items.Length - 1] == fileName)
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }
    }
    /// <summary>
    /// FTP 服务器异常
    /// </summary>
    public class FtpException : Exception
    {
        /// <summary>
        /// 创建Ftp服务器异常对象信息
        /// </summary>
        /// <param name="statusCode">FTP 服务器上发送的最新状态代码</param>
        public FtpException(FtpStatusCode statusCode)
            : base()
        {
            this.StatusCode = statusCode;
        }
        /// <summary>
        /// 创建Ftp服务器异常对象信息
        /// </summary>
        /// <param name="statusCode">FTP 服务器上发送的最新状态代码</param>
        /// <param name="message">FTP 服务器发送的状态代码的文本。</param>
        public FtpException(FtpStatusCode statusCode, String message)
            : base(message)
        {
            this.StatusCode = statusCode;
        }
        /// <summary>
        /// 创建Ftp服务器异常对象信息
        /// </summary>
        /// <param name="statusCode">FTP 服务器上发送的最新状态代码</param>
        /// <param name="message">FTP 服务器发送的状态代码的文本。</param>
        /// <param name="innerException">内部错误信息</param>
        public FtpException(FtpStatusCode statusCode, String message, Exception innerException)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
        }
        /// <summary>
        /// 获取从 FTP 服务器上发送的最新状态代码。
        /// </summary>
        public FtpStatusCode StatusCode { get; private set; }

    }
}
