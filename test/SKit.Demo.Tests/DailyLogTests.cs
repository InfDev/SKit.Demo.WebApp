using System;
using Xunit;

using SKit.Demo.WebApp.Services;
using System.Linq;

namespace SKit.Demo.Tests
{
    public class DailyLogTests
    {
        private static DateTime _dailyLogDate = new DateTime(2020, 05, 16);

        [Fact]
        public void DailyLogServiceTest()
        {
            var dailyLogService = new DailyLogService();
            var block = dailyLogService.Get(_dailyLogDate);
            Assert.NotNull(block);
            Assert.NotNull(block.Events);
            Assert.NotEmpty(block.Events);
            var firstEvent = block.Events.First();
            Assert.Equal("---- APP START ----", firstEvent.Message);
        }
    }
}
