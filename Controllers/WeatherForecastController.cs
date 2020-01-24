using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using weatherapp.Configuration;

namespace weatherapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRedisConnectionFactory _fact;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRedisConnectionFactory fact)
        {
            _logger = logger;
            _fact = fact;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var db =_fact.GetDatabase();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Sandy = db.StringGet("CacheTest")
            })
            .ToArray();
        }
    }
}
