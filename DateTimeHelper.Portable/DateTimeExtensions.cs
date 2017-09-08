using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeHelper.Portable
{
    public static class DateTimeExtensions
    {
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
        /// Converts the DateTime into string in ISO format.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string ToStringIso(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Get first the day of the month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Get Last the day of the month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// Gets the date of start day of week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Gets the date of last day of week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            return date.GetFirstDayOfWeek(startOfWeek).AddDays(6).Date;
        }

        /// <summary>
        /// Converts to the formated date time string e.g. 21 hours 2 miuntes ago | 3 months ago.
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
            return TimeZoneInfo.ConvertTime(dateTime.ToUniversalTime(), timeZoneInfo);
        }        
    }
}
