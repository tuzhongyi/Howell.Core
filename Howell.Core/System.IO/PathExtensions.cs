using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace System.IO
{
    /// <summary>
    /// System.IO.Path的扩展函数
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// 获取指定路径的上一层路径
        /// </summary>
        /// <param name="path">当前路径</param>
        /// <returns>返回上一层路径</returns>
        /// <exception cref="System.ArgumentNullException">path can not be null.</exception>
        /// <exception cref="System.ArgumentException">path is illegal.</exception>
        public static String GetPathUpper(String  path)
        {
            if (String.IsNullOrEmpty(path) == true)
                throw new ArgumentNullException("path");
            String value = Path.GetDirectoryName(path);
            String[] splitPaths = value.Split(new String[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            if (splitPaths.Length <= 0) throw new ArgumentException("Path is illegal.", "path");
            String result = "";
            for(int i =0;i < splitPaths.Length -1;++i)
            {
                result += String.Format("{0}\\", splitPaths[i]);
            }
            return result;
        }
        /// <summary>
        /// 获取指定路径的页目录信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>返回页目录名称</returns>
        /// <exception cref="System.ArgumentNullException">path can not be null.</exception>
        public static String GetPathLeaf(String path)
        {            
            if (String.IsNullOrEmpty(path) == true)
                throw new ArgumentNullException("path");
            String value = Path.GetDirectoryName(path);
            String[] splitPaths = value.Split(new String[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            if (splitPaths.Length <= 0) return "";
            else
                return splitPaths[splitPaths.Length - 1];
        }
        /// <summary>
        /// 判断指定文件名是否合法
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>如果合法返回true,否则返回false.</returns>
        public static Boolean IsFileNameValid(String fileName)
        {
            foreach (var item in Path.GetInvalidFileNameChars())
            {
                if (fileName.Contains(item) == true) return false;
            }
            return true;
        }
        /// <summary>
        /// 将指定的文件名转换为有效的文件名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回有效的文件名</returns>
        public static String GetValidFileName(String fileName)
        {
            if (String.IsNullOrEmpty(fileName) == true) throw new ArgumentNullException("fileName", "FileName can't be null or empty.");
            String result = fileName;
            foreach (var item in Path.GetInvalidFileNameChars())
            {
                result = result.Replace(item.ToString(), String.Empty);
            }
            if (String.IsNullOrEmpty(result) == true) throw new ArgumentException("All chars of fileName is illegal.", "fileName");
            return result;
        }
    }
}
