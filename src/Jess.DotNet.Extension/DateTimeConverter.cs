using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// Time Converter
    /// 时间格式转换器
    /// </summary>
    public sealed class DateTimeConverter
    {

        public static DateTime? ToDateTime(string strDateTime, DateTimeFormat dateTimeFormat)
        {
            switch (dateTimeFormat)
            {
                case DateTimeFormat.YYYYMMDD:
                    return yyyyMMddToDateTime(strDateTime);
                default:
                    break;
            }
            return null;
        }


        public static DateTime? yyyyMMddToDateTime(string date)
        {
            DateTime d;
            string fo = "yyyyMMdd";
            DateTime.TryParseExact(date, fo, null, System.Globalization.DateTimeStyles.None, out d);

            if (string.IsNullOrEmpty(date))
                return null;
            return d;
        }

        public static long ToUnix(DateTime dateTime)
        {
            var time = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;//ms
            //DateTimeOffset.Now.ToUnixTimeMilliseconds();
            
            return time;
        }
    }

    /// <summary>
    /// DateTime String Format
    /// 时间字符串格式
    /// </summary>
    public enum DateTimeFormat
    {
        YYYYMMDD,

    }

}