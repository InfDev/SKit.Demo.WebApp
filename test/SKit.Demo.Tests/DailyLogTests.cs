using System;
using System.Linq;
using Xunit;

using SKit.Demo.WebApp.Services;

namespace SKit.Demo.Tests
{
    public class DailyLogTests
    {
        private static DateTime _dailyLogDate = new DateTime(2020, 11, 13);

        [Fact]
        public async void DailyLogServiceTest()
        {
            var dailyLogService = new DailyLogService();
            var block = await dailyLogService.Get(_dailyLogDate);
            Assert.NotNull(block);
            Assert.NotNull(block.Events);
            Assert.NotEmpty(block.Events);
            var firstEvent = block.Events.First();
            Assert.Equal("---- APP START ----", firstEvent.Message);
        }
    }
}
