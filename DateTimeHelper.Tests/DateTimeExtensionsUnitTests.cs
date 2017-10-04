using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateTimeHelper.Tests
{
    [TestClass]
    public class DateTimeExtensionsUnitTests
    {
        readonly DateTime dateTime = new DateTime(2017, 10, 12, 12, 10, 01);

        [TestMethod]
        public void ToUnixTimeStampTest1()
        {
            var result = dateTime.ToUnixTimeStamp();
            var expectedDate = DTHelper.UnixTimeStampToDateTime(result);
            Assert.AreEqual(0, dateTime.CompareTo(expectedDate));
        }

        [TestMethod]
        public void ToStringIsoTest()
        {
            var result = dateTime.ToStringIso();
            Assert.AreEqual("2017-10-12 12:10:01", result);
        }

        [TestMethod]
        public void GetFirstDayOfTheMonthTest()
        {
            var result = dateTime.GetFirstDayOfTheMonth();
            var expectedDate = new DateTime(2017, 10, 01, 00, 00, 00);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }

        [TestMethod]
        public void GetLastDayOfTheMonthTest()
        {
            var result = dateTime.GetLastDayOfTheMonth();
            var expectedDate = new DateTime(2017, 10, 31, 00, 00, 00);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }

        [TestMethod]
        public void GetStartDayOfWeekTest1()
        {
            var result = dateTime.GetStartDayOfWeek(DayOfWeek.Monday);
            var expectedDate = new DateTime(2017, 10, 09, 00, 00, 00);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }

        [TestMethod]
        public void GetStartDayOfWeekTest2()
        {
            var result = dateTime.GetStartDayOfWeek(DayOfWeek.Sunday);
            var expectedDate = new DateTime(2017, 10, 08, 00, 00, 00);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }

        [TestMethod]
        public void GetLastDayOfWeekTest1()
        {
            var result = dateTime.GetLastDayOfWeek(DayOfWeek.Monday);
            var expectedDate = new DateTime(2017, 10, 15, 00, 00, 00);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }

        [TestMethod]
        public void GetLastDayOfWeekTest2()
        {
            var result = dateTime.GetLastDayOfWeek(DayOfWeek.Sunday);
            var expectedDate = new DateTime(2017, 10, 14, 00, 00, 00);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }

        [TestMethod]
        public void ToTimeZoneTest1()
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"); // (UTC+08:00) Kuala Lumpur, Singapore
            var result = dateTime.ToTimeZone(timeZoneInfo);
            var expectedDate = dateTime.ToUniversalTime().AddHours(8);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }

        [TestMethod]
        public void ToTimeZoneTest2()
        { 
            var result = dateTime.ToTimeZone("Singapore Standard Time"); // (UTC+08:00) Kuala Lumpur, Singapore
            var expectedDate = dateTime.ToUniversalTime().AddHours(8);
            Assert.AreEqual(0, expectedDate.CompareTo(result));
        }
    }
}
