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
    public class MovieStatisticsController : ControllerBase
    {
        private StatisticsService statisticsService = StatisticsService.Instance;

        [HttpGet(Name = "GetMovieStatistics")]
        public async Task<List<MovieStatistics>> GetAsync()
        {

            var statistics = await statisticsService.GetMovieStatistics();

            return statistics;
        }
    }
}
