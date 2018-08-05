using System;
using Xunit;

namespace ScheduleFetcher.Tests
{
    public class ScheduleServiceTest
    {
        [Fact]
        public void ServiceReturnsItems()
        {
            var scheduleService = new ScheduleService();
            var scheduleItems = scheduleService.GetScheduleItemsAsync().Result;
            
            Assert.NotEmpty(scheduleItems);
        }
    }
}