using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateTimeHelper
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Parses the specified UNIX time stamp.
        /// </summary>
        /// <param name="dt">The date time.</param>
        /// <param name="unixTimeStamp">The UNIX time stamp.</param>
        /// <returns></returns>
        public static DateTime Parse(this DateTime dt, double unixTimeStamp)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimeStampInTicks = (long)(unixTimeStamp * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks);
        }

        /// <summary>
        /// Parses the specified UNIX time stamp.
        /// </summary>
        /// <param name="dt">The date time.</param>
        /// <param name="unixTimeStamp">The UNIX time stamp.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Invalid UNIX time stamp.</exception>
        public static DateTime Parse(this DateTime dt, string unixTimeStamp)
        {
            if (string.IsNullOrEmpty(unixTimeStamp))
                throw new ArgumentException("Invalid UNIX time stamp.");
            var timeStampinDouble = double.Parse(unixTimeStamp);
            return dt.Parse(timeStampinDouble);
        }

        /// <summary>
        /// Converts to the UNIX time stamp.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToUnixTimeStamp(this DateTime dateTime)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimeStampInTicks = (dateTime - unixStart).Ticks;
            var ticks = unixTimeStampInTicks / TimeSpan.TicksPerSecond;
            return ticks.ToString();
        }

        /// <summary>
        /// Converts to the formated date time string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToFormatedDateTimeString(this DateTime dateTime)
        {
            try
            {
                DateTime dt1 = DateTime.UtcNow;
                TimeSpan ts;
                try
                {
                    ts = dt1 - dateTime;
                }
                catch (Exception)
                {
                    ts = dateTime - dt1;
                }
                string days = "";
                string hour = "";
                if (ts.TotalDays <= 1)
                {
                    if (ts.TotalHours > 1 && ts.TotalHours < 2)
                    {
                        days = Convert.ToInt32(ts.Hours) + " hr ";
                    }
                    else if (ts.TotalHours > 1)
                    {
                        days = Convert.ToInt32(ts.Hours) + " hrs ";
                    }
                    if (ts.Minutes <= 1)
                    {
                        hour = Convert.ToInt32(ts.Minutes) + " min ";
                    }
                    else if (ts.TotalMinutes > 1 && ts.TotalMinutes < 2)
                    {
                        hour = Convert.ToInt32(ts.Minutes) + " min ";
                    }
                    else
                    {
                        hour = Convert.ToInt32(ts.Minutes) + " mins ";
                    }
                }
                if (ts.TotalDays > 1)
                {
                    if (ts.TotalDays > 7)
                    {
                        int value = (dt1.Year - dateTime.Year) * 12 + dt1.Month - dateTime.Month;
                        if (value < 12)
                        {
                            days = dateTime.ToString("MMMM dd") + " ";
                        }
                        else
                        {
                            days = dateTime.ToString("MMMM dd yyyy") + "";
                        }
                    }
                    else
                    {
                        if (ts.Days > 1)
                        {
                            days = Convert.ToInt32(ts.Days) + " Days ";
                        }
                        else if (ts.Days <= 1)
                        {
                            days = Convert.ToInt32(ts.Days) + " Day ";
                        }
                        if (ts.Hours > 1)
                        {
                            hour = Convert.ToInt32(ts.Hours) + " hrs ";
                        }
                        else if (ts.TotalMinutes <= 1)
                        {
                            hour = Convert.ToInt32(ts.Minutes) + " min ";
                        }
                        else
                        {
                            hour = Convert.ToInt32(ts.Minutes) + " mins ";
                        }
                    }
                }
                string response;
                if (days.Contains("Day") || days.Contains("hr") || hour.Contains("Day") || hour.Contains("hr"))
                    response = days + hour + "ago";
                else if (!string.IsNullOrEmpty(days))
                    response = days;
                else
                    response = hour + "ago";
                return response;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// To the time zone.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="timeZoneInfo">The time zone information.</param>
        /// <returns></returns>
        public static DateTime ToTimeZone(this DateTime dateTime, TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), timeZoneInfo);
        }

        /// <summary>
        /// To the time zone.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="timeZoneId">The time zone identifier.</param>
        /// <returns></returns>
        public static DateTime ToTimeZone(this DateTime dateTime, string timeZoneId)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return dateTime.ToUniversalTime().ToTimeZone(timeZoneInfo);
        }
    }
}
