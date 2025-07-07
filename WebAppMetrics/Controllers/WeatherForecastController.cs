using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMetrics.Services;

namespace WebAppMetrics.Controllers
{
    /// <summary>
    /// Controller for weather forecast operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly MetricsService _metricsService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MetricsService metricsService)
        {
            _logger = logger;
            _metricsService = metricsService;
        }

        /// <summary>
        /// Gets a collection of weather forecasts
        /// </summary>
        /// <returns>A collection of weather forecast data</returns>
        /// <response code="200">Returns the weather forecast data</response>
        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), 200)]
        public IEnumerable<WeatherForecast> Get()
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _metricsService.IncrementActiveRequests();
                _metricsService.RecordWeatherForecastRequest();
                
                _logger.LogInformation("Getting weather forecast data");
                
                var forecasts = Enumerable.Range(1, 5).Select(index =>
                {
                    var temperatureC = Random.Shared.Next(-20, 55);
                    var summary = Summaries[Random.Shared.Next(Summaries.Length)];
                    
                    // Record metrics for each temperature reading
                    _metricsService.RecordTemperatureReading(temperatureC, summary);
                    
                    return new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = temperatureC,
                        Summary = summary
                    };
                }).ToArray();

                _logger.LogInformation("Generated {Count} weather forecasts", forecasts.Length);
                
                return forecasts;
            }
            finally
            {
                stopwatch.Stop();
                _metricsService.RecordProcessingTime(stopwatch.Elapsed.TotalMilliseconds);
                _metricsService.DecrementActiveRequests();
                
                _logger.LogInformation("Weather forecast request completed in {ElapsedMs}ms", 
                    stopwatch.Elapsed.TotalMilliseconds);
            }
        }
    }
}
