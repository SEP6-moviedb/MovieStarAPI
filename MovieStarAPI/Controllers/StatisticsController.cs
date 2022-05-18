using MovieStarAPI.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using MovieStarAPI.Persistence;

namespace MovieStarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        [HttpGet(Name = "GetStatistics")]
        public async Task<List<ActorStatistics>> GetAsync()
        {
            var statistics = await StatisticsService.GetStatistics();

            return statistics;
        }
    }
}
