using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;

namespace Howell.OS
{
    using TimeZoneCollection = System.Collections.Generic.Dictionary<string, TimeZone.TimeZoneInformation>;
    /// <summary>
    /// 系统时区
    /// </summary>
    public class TimeZone
    {

        #region DLL Imports

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetTimeZoneInformation(out TimeZoneInformation lpTimeZoneInformation);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]

        private static extern bool SetTimeZoneInformation(ref TimeZoneInformation lpTimeZoneInformation);


        /// <summary>
        /// 系统时间
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            /// <summary>
            /// 年
            /// </summary>
            public short wYear;
            /// <summary>
            /// 月
            /// </summary>
            public short wMonth;
            /// <summary>
            /// 星期几
            /// </summary>
            public short wDayOfWeek;
            /// <summary>
            /// 天
            /// </summary>
            public short wDay;
            /// <summary>
            /// 时
            /// </summary>
            public short wHour;
            /// <summary>
            /// 分
            /// </summary>
            public short wMinute;
            /// <summary>
            /// 秒
            /// </summary>
            public short wSecond;
            /// <summary>
            /// 毫秒
            /// </summary>
            public short wMilliseconds;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="buf"></param>
            /// <param name="index"></param>
            public SYSTEMTIME(byte[] buf, int index)
            {
                wYear = BitConverter.ToInt16(buf, index);
                index += 2;

                wMonth = BitConverter.ToInt16(buf, index);
                index += 2;

                wDayOfWeek = BitConverter.ToInt16(buf, index);
                index += 2;

                wDay = BitConverter.ToInt16(buf, index);
                index += 2;

                wHour = BitConverter.ToInt16(buf, index);
                index += 2;

                wMinute = BitConverter.ToInt16(buf, index);
                index += 2;

                wSecond = BitConverter.ToInt16(buf, index);
                index += 2;

                wMilliseconds = BitConverter.ToInt16(buf, index);
            }

        }
        /// <summary>
        /// 时区信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TimeZoneInformation
        {
            /// <summary>
            /// 
            /// </summary>
            public int bias;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string standardName;
            /// <summary>
            /// 
            /// </summary>
            public SYSTEMTIME standardDate;
            /// <summary>
            /// 
            /// </summary>
            public int standardBias;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string daylightName;
            /// <summary>
            /// 
            /// </summary>
            public SYSTEMTIME daylightDate;
            /// <summary>
            /// 
            /// </summary>
            public int daylightBias;

        }

        #endregion


        static object _SyncRoot = new object();
        static TimeZoneCollection _Zones = null;
        /// <summary>
        /// Get all time zone informations
        /// </summary>
        /// <returns></returns>
        private static TimeZoneCollection GetTimeZones()
        {
            lock (_SyncRoot)
            {
                if (_Zones != null)
                {
                    return _Zones;
                }

                //open key where all time zones are located in the registry

                RegistryKey timeZoneKeys = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones");

                //create a new hashtable which will store the name

                //of the timezone and the associate time zone information struct

                _Zones = new TimeZoneCollection();

                //iterate through each time zone in the registry and add it to the hash table

                foreach (string zonekey in timeZoneKeys.GetSubKeyNames())
                {

                    //get current time zone key

                    RegistryKey individualZone = timeZoneKeys.OpenSubKey(zonekey);

                    //create new TZI struct and populate it with values from key

                    TimeZoneInformation TZI = new TimeZoneInformation();

                    TZI.standardName = individualZone.GetValue("Std").ToString();

                    string displayName = individualZone.GetValue("Display").ToString();

                    TZI.daylightName = individualZone.GetValue("Dlt").ToString();

                    //read binary TZI data, convert to byte array

                    byte[] b = (byte[])individualZone.GetValue("TZI");

                    TZI.bias = BitConverter.ToInt32(b, 0);
                    TZI.standardBias = BitConverter.ToInt32(b, 4);
                    TZI.daylightBias = BitConverter.ToInt32(b, 8);

                    TZI.standardDate = new SYSTEMTIME(b, 12);
                    TZI.daylightDate = new SYSTEMTIME(b, 28);

                    //Marshal.PtrToStructure(

                    //add the name and TZI struct to hash table

                    _Zones.Add(displayName, TZI);

                }

                return _Zones;
            }
        }
        /// <summary>
        /// Get the Time zone by display name 
        /// </summary>
        /// <param name="displayName">part of display name</param>
        /// <param name="wholeDisplayName">whole display name</param>
        /// <returns>time zone</returns>
        public static TimeZoneInformation GetTimeZone(string displayName, out string wholeDisplayName)
        {
            TimeZoneCollection tzc = TimeZone.GetTimeZones();

            foreach (string key in tzc.Keys)
            {
                if (key.IndexOf(displayName, 0, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    wholeDisplayName = key;
                    return tzc[key];
                }
            }

            throw new Exception(string.Format("Can't find the display name : {0}", displayName));
        }
        /// <summary>
        /// Get current time zone
        /// </summary>
        /// <returns></returns>
        public static TimeZoneInformation GetCurrentTimeZone()
        {
            TimeZoneInformation tzi;
            GetTimeZoneInformation(out tzi);
            return tzi;
        }
        /// <summary>
        /// Get current time zone display name
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTimeZoneDisplayName()
        {
            TimeZoneInformation tzi;
            GetTimeZoneInformation(out tzi);
            TimeZoneCollection tzc = TimeZone.GetTimeZones();
            foreach (string displayName in tzc.Keys)
            {
                TimeZoneInformation tz = tzc[displayName];
                if (tz.standardName.Equals(tzi.standardName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return displayName;
                }
            }

            throw new Exception(string.Format("Can't find the display name of {0}", tzi.standardName));
        }
        /// <summary>
        /// Set time zone
        /// </summary>
        /// <param name="tzi"></param>
        /// <returns></returns>
        public static bool SetTimeZone(TimeZoneInformation tzi)
        {

            //ComputerManager.EnableToken("SeTimeZonePrivilege", Process.GetCurrentProcess().Handle);

            // set local system timezone

            return SetTimeZoneInformation(ref tzi);

        }
        /// <summary>
        /// set time zone by display name
        /// </summary>
        /// <param name="displayName">part of display name</param>
        /// <param name="wholeDisplayName">whole display name</param>
        /// <returns></returns>
        public static bool SetTimeZone(string displayName, out string wholeDisplayName)
        {

            //ComputerManager.EnableToken("SeTimeZonePrivilege", Process.GetCurrentProcess().Handle);

            // set local system timezone
            TimeZoneInformation tzi = GetTimeZone(displayName, out wholeDisplayName);

            return SetTimeZoneInformation(ref tzi);

        }
    }
}
