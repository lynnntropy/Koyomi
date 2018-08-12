using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleFetcher;
using ScheduleFetcher.Models;

namespace KoyomiAPI.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    public class ScheduleController : Controller
    {
        private readonly ScheduleService _scheduleService;
        
        private static List<ScheduleItem> _cachedSchedule;
        private static DateTime _cacheTime;

        public ScheduleController()
        {
            _scheduleService = new ScheduleService();
        }

        [HttpGet("schedule")]
        public async Task<List<ScheduleItem>> GetSchedule()
        {
            // Refresh the cache if it's nonexistent or stale (over an hour old)
            if (_cachedSchedule == null || (DateTime.Now - _cacheTime).TotalHours >= 1)
            {
                _cachedSchedule = await _scheduleService.GetScheduleItemsAsync();
                _cacheTime = DateTime.Now;
            }

            return _cachedSchedule;
        }
    }
}