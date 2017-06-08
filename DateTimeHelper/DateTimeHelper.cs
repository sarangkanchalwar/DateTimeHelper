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
    }
}
