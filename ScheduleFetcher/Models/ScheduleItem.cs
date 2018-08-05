using System;
using System.Reflection;

namespace ScheduleFetcher.Models
{
    public class ScheduleItem
    {
        private DayOfWeek DayOfWeek { get; set; }
        private string Title { get; set; }
        private DateTimeOffset NextEpisodeTime { get; set; }

        public ScheduleItem(DayOfWeek dayOfWeek, string title, DateTimeOffset nextEpisodeTime)
        {
            DayOfWeek = dayOfWeek;
            Title = title;
            NextEpisodeTime = nextEpisodeTime;
        }
    }
}