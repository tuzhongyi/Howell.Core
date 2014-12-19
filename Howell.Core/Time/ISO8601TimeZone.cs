using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Time
{
    /// <summary>
    /// 时区枚举
    /// </summary>
    public enum ISO8601TimeZones
    {
        /// <summary>
        /// Australian Central Daylight  +1030
        /// </summary>
        ACDT = 0,
        /// <summary>
        /// Australian Central Standard +0930
        /// </summary>
        ACST = 1,
        /// <summary>
        /// (US) Atlantic Daylight -0300
        /// </summary>
        ADT = 2,
        /// <summary>
        /// Australian East Daylight  +1100
        /// </summary>
        AEDT = 3,
        /// <summary>
        /// Australian East Standard +1000
        /// </summary>
        AEST = 4,
        /// <summary>
        /// AHDT   -0900
        /// </summary>
        AHDT = 5,
        /// <summary>
        /// AHDT   -1000
        /// </summary>
        AHST = 6,
        /// <summary>
        /// (US) Atlantic Standard -0400
        /// </summary>
        AST = 7,
        /// <summary>
        /// Azores  -0200
        /// </summary>
        AT = 8,
        /// <summary>
        /// Australian West Daylight  +0900
        /// </summary>
        AWDT = 9,
        /// <summary>
        /// Australian West Standard  +0800
        /// </summary>
        AWST = 10,
        /// <summary>
        /// Bhagdad  +0300
        /// </summary>
        BAT = 11,
        /// <summary>
        /// British Double Summer  +0200
        /// </summary>
        BDST = 12,
        /// <summary>
        /// Bering Standard  -1100
        /// </summary>
        BET = 13,
        /// <summary>
        /// Bering Standard  -0300
        /// </summary>
        BST = 14,
        /// <summary>
        /// Baghdad  +0300
        /// </summary>
        BT = 15,
        /// <summary>
        /// Brazil Zone 2  -0300
        /// </summary>
        BZT2 = 16,
        /// <summary>
        /// Central Australian Daylight  +1030
        /// </summary>
        CADT = 17,
        /// <summary>
        /// Central Australian Standard  +0930
        /// </summary>
        CAST = 18,
        /// <summary>
        /// Central Alaska  -1000
        /// </summary>
        CAT = 19,
        /// <summary>
        /// China Coast  +0800
        /// </summary>
        CCT = 20,
        /// <summary>
        /// (US) Central Daylight  -0500
        /// </summary>
        CDT = 21,
        /// <summary>
        /// Central European Daylight  +0200
        /// </summary>
        CED = 22,
        /// <summary>
        /// Central European  +0100
        /// </summary>
        CET = 23,
        /// <summary>
        /// (US) Central Standard -0600
        /// </summary>
        CST = 24,
        /// <summary>
        /// (US) Central Standard -0600
        /// </summary>
        CENTRAL = 25,
        /// <summary>
        /// Eastern Australian Standard +1000
        /// </summary>
        EAST = 26,
        /// <summary>
        /// (US) Eastern Daylight -0400
        /// </summary>
        EDT = 27,
        /// <summary>
        /// Eastern European Daylight +0300
        /// </summary>
        EED = 28,
        /// <summary>
        /// Eastern Europe +0200
        /// </summary>
        EET = 29,
        /// <summary>
        /// Eastern Europe Summer +0300
        /// </summary>
        EEST = 30,
        /// <summary>
        /// (US) Eastern Standard -0500
        /// </summary>
        EST = 31,
        /// <summary>
        /// (US) Eastern Standard -0500
        /// </summary>
        EASTERN = 32,

        //new string[] {"EASTERN", "-0500", "(US) Eastern Standard"},
        //new string[] {"FST", "+0200", "French Summer"},
        //new string[] {"FWT", "+0100", "French Winter"},
        //new string[] {"GMT", "-0000", "Greenwich Mean"},
        //new string[] {"GST", "+1000", "Guam Standard"},
        //new string[] {"HDT", "-0900", "Hawaii Daylight"},
        //new string[] {"HST", "-1000", "Hawaii Standard"},
        //new string[] {"IDLE", "+1200", "Internation Date Line East"},
        //new string[] {"IDLW", "-1200", "Internation Date Line West"},
        //new string[] {"IST", "+0530", "Indian Standard"},
        //new string[] {"IT", "+0330", "Iran"},
        //new string[] {"JST", "+0900", "Japan Standard"},
        //new string[] {"JT", "+0700", "Java"},
    }

    /// <summary>
    /// ISO8601时区
    /// </summary>
    public class ISO8601TimeZone
    {
        //private TimeSpan m_Offset;
        private static string[][] TimeZones = new string[][] {
        new string[] {"ACDT", "+1030", "Australian Central Daylight"},
        new string[] {"ACST", "+0930", "Australian Central Standard"},
        new string[] {"ADT", "-0300", "(US) Atlantic Daylight"},
        new string[] {"AEDT", "+1100", "Australian East Daylight"},
        new string[] {"AEST", "+1000", "Australian East Standard"},
        new string[] {"AHDT", "-0900", ""},
        new string[] {"AHST", "-1000", ""},
        new string[] {"AST", "-0400", "(US) Atlantic Standard"},
        new string[] {"AT", "-0200", "Azores"},
        new string[] {"AWDT", "+0900", "Australian West Daylight"},
        new string[] {"AWST", "+0800", "Australian West Standard"},
        new string[] {"BAT", "+0300", "Bhagdad"},
        new string[] {"BDST", "+0200", "British Double Summer"},
        new string[] {"BET", "-1100", "Bering Standard"},
        new string[] {"BST", "-0300", "Brazil Standard"},
        new string[] {"BT", "+0300", "Baghdad"},
        new string[] {"BZT2", "-0300", "Brazil Zone 2"},
        new string[] {"CADT", "+1030", "Central Australian Daylight"},
        new string[] {"CAST", "+0930", "Central Australian Standard"},
        new string[] {"CAT", "-1000", "Central Alaska"},
        new string[] {"CCT", "+0800", "China Coast"},
        new string[] {"CDT", "-0500", "(US) Central Daylight"},
        new string[] {"CED", "+0200", "Central European Daylight"},
        new string[] {"CET", "+0100", "Central European"},
        new string[] {"CST", "-0600", "(US) Central Standard"},
        new string[] {"CENTRAL", "-0600", "(US) Central Standard"},
        new string[] {"EAST", "+1000", "Eastern Australian Standard"},
        new string[] {"EDT", "-0400", "(US) Eastern Daylight"},
        new string[] {"EED", "+0300", "Eastern European Daylight"},
        new string[] {"EET", "+0200", "Eastern Europe"},
        new string[] {"EEST", "+0300", "Eastern Europe Summer"},
        new string[] {"EST", "-0500", "(US) Eastern Standard"},
        new string[] {"EASTERN", "-0500", "(US) Eastern Standard"},
        new string[] {"FST", "+0200", "French Summer"},
        new string[] {"FWT", "+0100", "French Winter"},
        new string[] {"GMT", "-0000", "Greenwich Mean"},
        new string[] {"GST", "+1000", "Guam Standard"},
        new string[] {"HDT", "-0900", "Hawaii Daylight"},
        new string[] {"HST", "-1000", "Hawaii Standard"},
        new string[] {"IDLE", "+1200", "Internation Date Line East"},
        new string[] {"IDLW", "-1200", "Internation Date Line West"},
        new string[] {"IST", "+0530", "Indian Standard"},
        new string[] {"IT", "+0330", "Iran"},
        new string[] {"JST", "+0900", "Japan Standard"},
        new string[] {"JT", "+0700", "Java"},
        new string[] {"MDT", "-0600", "(US) Mountain Daylight"},
        new string[] {"MED", "+0200", "Middle European Daylight"},
        new string[] {"MET", "+0100", "Middle European"},
        new string[] {"MEST", "+0200", "Middle European Summer"},
        new string[] {"MEWT", "+0100", "Middle European Winter"},
        new string[] {"MST", "-0700", "(US) Mountain Standard"},
        new string[] {"MOUNTAIN", "-0700", "(US) Mountain Standard"},
        new string[] {"MT", "+0800", "Moluccas"},
        new string[] {"NDT", "-0230", "Newfoundland Daylight"},
        new string[] {"NFT", "-0330", "Newfoundland"},
        new string[] {"NT", "-1100", "Nome"},
        new string[] {"NST", "+0630", "North Sumatra"},
        new string[] {"NZ", "+1100", "New Zealand "},
        new string[] {"NZST", "+1200", "New Zealand Standard"},
        new string[] {"NZDT", "+1300", "New Zealand Daylight "},
        new string[] {"NZT", "+1200", "New Zealand"},
        new string[] {"PDT", "-0700", "(US) Pacific Daylight"},
        new string[] {"PST", "-0800", "(US) Pacific Standard"},
        new string[] {"PACIFIC", "-0800", "(US) Pacific Standard"},
        new string[] {"ROK", "+0900", "Republic of Korea"},
        new string[] {"SAD", "+1000", "South Australia Daylight"},
        new string[] {"SAST", "+0900", "South Australia Standard"},
        new string[] {"SAT", "+0900", "South Australia Standard"},
        new string[] {"SDT", "+1000", "South Australia Daylight"},
        new string[] {"SST", "+0200", "Swedish Summer"},
        new string[] {"SWT", "+0100", "Swedish Winter"},
        new string[] {"USZ3", "+0400", "USSR Zone 3"},
        new string[] {"USZ4", "+0500", "USSR Zone 4"},
        new string[] {"USZ5", "+0600", "USSR Zone 5"},
        new string[] {"USZ6", "+0700", "USSR Zone 6"},
        new string[] {"UT", "-0000", "Universal Coordinated"},
        new string[] {"UTC", "-0000", "Universal Coordinated"},
        new string[] {"UZ10", "+1100", "USSR Zone 10"},
        new string[] {"WAT", "-0100", "West Africa"},
        new string[] {"WET", "-0000", "West European"},
        new string[] {"WST", "+0800", "West Australian Standard"},
        new string[] {"YDT", "-0800", "Yukon Daylight"},
        new string[] {"YST", "-0900", "Yukon Standard"},
        new string[] {"ZP4", "+0400", "USSR Zone 3"},
        new string[] {"ZP5", "+0500", "USSR Zone 4"},
        new string[] {"ZP6", "+0600", "USSR Zone 5"},
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        public ISO8601TimeZone(TimeSpan ts)
        {
            
        }

        /// <summary>
        /// 转换为字符串 默认格式+0800
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return "";
        }

    }
}
