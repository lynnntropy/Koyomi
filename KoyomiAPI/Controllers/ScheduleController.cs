using System;
using System.Collections.Generic;
using System.Linq;
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

        public ScheduleController()
        {
            _scheduleService = new ScheduleService();
        }

        [HttpGet("schedule")]
        public async Task<List<ScheduleItem>> GetSchedule()
        {
            return await _scheduleService.GetScheduleItemsAsync();
        }
    }
}