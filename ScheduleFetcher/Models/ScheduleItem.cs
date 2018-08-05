using System;
using System.Reflection;
// ReSharper disable MemberCanBePrivate.Global

namespace ScheduleFetcher.Models
{
    public class ScheduleItem
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string Title { get; set; }
        public DateTimeOffset NextEpisodeTime { get; set; }

        public ScheduleItem()
        {
        }

        public ScheduleItem(DayOfWeek dayOfWeek, string title, DateTimeOffset nextEpisodeTime)
        {
            DayOfWeek = dayOfWeek;
            Title = title;
            NextEpisodeTime = nextEpisodeTime;
        }
    }
}