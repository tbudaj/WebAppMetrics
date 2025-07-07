using System.Diagnostics.Metrics;

namespace WebAppMetrics.Services
{
    /// <summary>
    /// Service for managing custom application metrics
    /// </summary>
    public class MetricsService
    {
        private readonly Meter _meter;
        private readonly Counter<long> _weatherForecastRequestCounter;
        private readonly Histogram<double> _weatherForecastProcessingTime;
        private readonly ObservableGauge<int> _activeRequestsGauge;
        private readonly Counter<long> _temperatureReadingsCounter;
        
        private int _activeRequests = 0;

        public MetricsService(Meter meter)
        {
            _meter = meter;
            
            // Initialize metrics
            _weatherForecastRequestCounter = _meter.CreateCounter<long>(
                "weather_forecast_requests_total",
                "request",
                "Total number of weather forecast requests");

            _weatherForecastProcessingTime = _meter.CreateHistogram<double>(
                "weather_forecast_processing_duration_ms",
                "ms",
                "Time taken to process weather forecast requests");

            _temperatureReadingsCounter = _meter.CreateCounter<long>(
                "temperature_readings_total",
                "reading",
                "Total number of temperature readings generated");

            _activeRequestsGauge = _meter.CreateObservableGauge<int>(
                "weather_forecast_active_requests",
                () => _activeRequests,
                "request",
                "Number of active weather forecast requests");
        }

        /// <summary>
        /// Records a weather forecast request
        /// </summary>
        public void RecordWeatherForecastRequest(string endpoint = "GetWeatherForecast")
        {
            _weatherForecastRequestCounter.Add(1, new KeyValuePair<string, object?>("endpoint", endpoint));
        }

        /// <summary>
        /// Records processing time for weather forecast
        /// </summary>
        public void RecordProcessingTime(double processingTimeMs, string endpoint = "GetWeatherForecast")
        {
            _weatherForecastProcessingTime.Record(processingTimeMs, 
                new KeyValuePair<string, object?>("endpoint", endpoint));
        }

        /// <summary>
        /// Records temperature readings generated
        /// </summary>
        public void RecordTemperatureReading(int temperatureC, string summary)
        {
            _temperatureReadingsCounter.Add(1, 
                new KeyValuePair<string, object?>("temperature_range", GetTemperatureRange(temperatureC)),
                new KeyValuePair<string, object?>("summary", summary));
        }

        /// <summary>
        /// Increments active requests counter
        /// </summary>
        public void IncrementActiveRequests()
        {
            Interlocked.Increment(ref _activeRequests);
        }

        /// <summary>
        /// Decrements active requests counter
        /// </summary>
        public void DecrementActiveRequests()
        {
            Interlocked.Decrement(ref _activeRequests);
        }

        private static string GetTemperatureRange(int temperature)
        {
            return temperature switch
            {
                < 0 => "freezing",
                >= 0 and < 10 => "cold",
                >= 10 and < 20 => "cool",
                >= 20 and < 30 => "warm",
                >= 30 => "hot"
            };
        }
    }
}