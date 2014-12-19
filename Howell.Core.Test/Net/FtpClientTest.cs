
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Howell.Net
{
    class FtpClientTest
    {
        public static void Test()
        {
            FtpClient client = new FtpClient("ftp://127.0.0.1/");
            client.Credentials = new NetworkCredential("TZY", "spyder1983");

            if (client.DirectoryExists("MySqlDb") == false)
            {
                client.CreateDirectory("MySqlDb");
            }
            client.UploadFile("FTPClientDemo.exe.config", @"MySqlDb\config.txt");
            client.DownloadFile("config.txt", @"MySqlDb\config.txt");
            client.GetFileSize(@"MySqlDb\config.txt");
            client.RenameDirectory("MySqlDb", "MySqlDb2");
            if (client.FileExists(@"MySqlDb2\config.txt") == true)
            {
                client.DeleteFile(@"MySqlDb2\config.txt");
            }
            if (client.DirectoryExists("MySqlDb2") == true)
            {
                client.DeleteDirectory("MySqlDb2");
            }
        }
    }
}
