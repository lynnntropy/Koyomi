using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScheduleFetcher.Models;

namespace ScheduleFetcher
{
    public class ScheduleService
    {
        private const string ScheduleUrl = "https://horriblesubs.info/release-schedule/";
        private static HttpClient _httpClient;

        public ScheduleService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<ScheduleItem>> GetScheduleItemsAsync()
        {
            var scheduleItems = new List<ScheduleItem>();
            
            var html = await _httpClient.GetStringAsync(ScheduleUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var dayCounter = 1;

            var tables = doc.DocumentNode.SelectNodes("//table");
            foreach (var table in tables)
            {
                var title = table.SelectSingleNode(".//td[@class='schedule-page-show']").InnerText;

                if (dayCounter == 7) dayCounter = 0;
                var dayOfWeek = (DayOfWeek) dayCounter;

                var rows = table.SelectNodes(".//tr");
                foreach (var row in rows)
                {
                    var timeString = table.SelectSingleNode(".//td[@class='schedule-time']").InnerText;
                    var scheduleTime = DateTimeOffset.Parse($"{timeString} {GetHorribleSubsTimeZoneOffsetString()}");
                    var nextEpisodeDay = GetNextWeekday(dayOfWeek, DateTimeOffset.Now <= scheduleTime);
                
                    var nextEpisodeTime = new DateTimeOffset(
                        nextEpisodeDay.Year, 
                        nextEpisodeDay.Month, 
                        nextEpisodeDay.Day, 
                        scheduleTime.Hour, 
                        scheduleTime.Minute, 
                        0, 
                        scheduleTime.Offset);
                
                    scheduleItems.Add(new ScheduleItem(dayOfWeek, title, nextEpisodeTime));                    
                }

                dayCounter++;
            }

            return scheduleItems;
        }

        private static string GetHorribleSubsTimeZoneOffsetString()
        {
            // Return PDT offset if during daylight savings time
            
            if (DateTime.Now >= new DateTime(DateTime.Now.Year, 3, 11) &&
                DateTime.Now <= new DateTime(DateTime.Now.Year, 11, 4))
            {
                return "-7:00";
            }
            
            // Return PST offset otherwise

            return "-8:00";
        }
        
        private static DateTime GetNextWeekday(DayOfWeek day, bool startFromToday)
        {
            var result = startFromToday ? DateTime.Now : DateTime.Now.AddDays(1);
            while (result.DayOfWeek != day) result = result.AddDays(1);
            return result;
        }
    }
}
