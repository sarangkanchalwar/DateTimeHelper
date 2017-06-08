using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeHelper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Converts the Unix time stamp value to C# DateTime object.
        /// </summary>
        /// <param name="unixTimeStamp">The Unix time stamp.</param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimeStampInTicks = (long)(unixTimeStamp * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks);
        }

        /// <summary>
        /// Converts the C# DateTime object to Unix time stamp.
        /// </summary>
        /// <param name="dateTime">The DateTime object.</param>
        /// <returns></returns>
        public static string DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimeStampInTicks = (dateTime - unixStart).Ticks;
            var ticks = unixTimeStampInTicks / TimeSpan.TicksPerSecond;
            return ticks.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the difference between two dates.
        /// </summary>
        /// <param name="date1">The date1.</param>
        /// <param name="date2">The date2.</param>
        /// <returns></returns>
        public static string GetDifferenceBetweenDates(DateTime date1, DateTime date2)
        {
            if (DateTime.Compare(date1, date2) >= 0)
            {
                TimeSpan ts = date1.Subtract(date2);
                if (ts.Days == 0)
                {
                    if (ts.Hours == 0)
                        return string.Format("{0}  minutes", ts.Minutes);
                    return string.Format("{0} hours, {1} minutes",
                        ts.Hours, ts.Minutes);
                }
                if (ts.Hours == 0)
                    return string.Format("{0}  minutes", ts.Minutes);
                return string.Format("{0} days", ts.Days);
            }
            return "Unknown";
        }

        /// <summary>
        /// Gets the dates between date range.
        /// </summary>
        /// <param name="startDate">The startDate.</param>
        /// <param name="endDate">The endDate.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachDay(DateTime startDate, DateTime endDate)
        {
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
                yield return day;
        }

        /// <summary>
        /// Gets the formated date time string.
        /// </summary>
        /// <param name="dateTime">The DateTime object.</param>
        /// <returns></returns>
        public static string GetFormatedDateTimeString(DateTime dateTime)
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
        /// Gets the formated date time string.
        /// </summary>
        /// <param name="unixTimestamp">The Unix time stamp.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Null or empty string not allowed for time stamp.</exception>
        public static string GetFormatedDateTimeString(string unixTimestamp)
        {
            if(string.IsNullOrEmpty(unixTimestamp))
                throw new ArgumentException("Null or empty string not allowed for time stamp.");
            double timeStampInDouble = double.Parse(unixTimestamp);
            DateTime dateTime = UnixTimeStampToDateTime(timeStampInDouble);
            return GetFormatedDateTimeString(dateTime);
        }

        /// <summary>
        /// Converts the UTC date time in time zone date time.
        /// </summary>
        /// <param name="utcDateTime">The UTC date time.</param>
        /// <param name="timeZoneInfo">The time zone information.</param>
        /// <returns></returns>
        public static DateTime ConvertDateTimeInTimeZone(DateTime utcDateTime, TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZoneInfo);
        }

        /// <summary>
        /// Converts the UTC date time in time zone date time.
        /// TimeZoneId can be get from ReadOnlyCollection - TimeZoneInfo.GetSystemTimeZones()
        /// </summary>
        /// <param name="utcDateTime">The UTC date time.</param>
        /// <param name="timeZoneId">The time zone identifier string.</param>
        /// <returns></returns>
        public static DateTime ConvertDateTimeInTimeZone(DateTime utcDateTime, string timeZoneId)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return ConvertDateTimeInTimeZone(utcDateTime, timeZoneInfo);
        }
    }
}
