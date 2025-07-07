# WebAppMetrics - .NET 8 Web API with OpenTelemetry

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue)
![OpenTelemetry](https://img.shields.io/badge/OpenTelemetry-1.8.1-orange)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-green)
![Docker](https://img.shields.io/badge/Docker-Ready-blue)

## 📋 Project Description

**WebAppMetrics** is a demonstration Web API application written in .NET 8 that showcases the implementation of a complete observability stack for microservice applications. The project includes full integration with OpenTelemetry, metric export to Prometheus, and visualization in Grafana.

### 🚀 Key Features

- **RESTful API** - Weather Forecast endpoint with Swagger/OpenAPI documentation
- **OpenTelemetry Integration** - Complete application instrumentation with metrics
- **Custom Metrics** - Dedicated business metrics for the application
- **Prometheus Export** - Metric export to Prometheus monitoring system
- **Grafana Dashboards** - Ready infrastructure for metric visualization
- **Console Logging** - Real-time metric preview in console
- **Docker Support** - Full application containerization

## 🏗️ Architecture
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│  WebAppMetrics  │───▶│ OpenTelemetry   │───▶│   Prometheus    │───▶│     Grafana     │
│     (.NET 8)    │    │   Collector     │    │   (Metrics DB)  │    │ (Visualization) │
└─────────────────┘    └─────────────────┘    └─────────────────┘    └─────────────────┘

### 📊 Available Metrics

#### Custom Metrics
- `weather_forecast_requests_total` - Counter for weather API requests
- `weather_forecast_processing_duration_ms` - Histogram of request processing time
- `weather_forecast_active_requests` - Gauge for active requests
- `temperature_readings_total` - Counter for temperature readings with categories

#### System Metrics (OpenTelemetry Instrumentation)
- **ASP.NET Core Metrics** - Request duration, response size, error rates
- **HTTP Client Metrics** - Outbound HTTP calls instrumentation
- **Runtime Metrics** - GC collections, memory usage, thread pool

## ⚡ Quick Start

### Requirements
- .NET 8 SDK
- Docker or Podman
- PowerShell (for automation scripts)

### 1. Running the Application
cd WebAppMetrics
dotnet run
The application will be available at: `https://localhost:7029`

### 2. Swagger UI
Access API documentation at: `https://localhost:7029/swagger`

### 3. Running the Observability Stack

#### Step 1: Create Docker Network
podman network create otel-net
#### Step 2: Start OpenTelemetry Collector
.\Configuration\otel-collector.ps1
#### Step 3: Start Prometheus
.\Configuration\Prometheus.ps1
#### Step 4: Start Grafana
.\Configuration\grafana.ps1
### 4. Tool Access

| Tool | URL | Credentials |
|------|-----|-------------|
| WebApp API | http://localhost:7029 | - |
| Swagger UI | http://localhost:7029/swagger | - |
| Prometheus | http://localhost:9090 | - |
| Grafana | http://localhost:3000 | admin/admin |

## ⚙️ Configuration

### OpenTelemetry Settings (appsettings.json)
{
  "OpenTelemetry": {
    "ServiceName": "WebAppMetrics",
    "ServiceVersion": "1.0.0",
    "OtlpEndpoint": "http://localhost:4317",
    "MetricExportSettings": {
      "ExportIntervalMs": 30000,      // Export to collector every 30s
      "ExportTimeoutMs": 10000,
      "MaxExportBatchSize": 512
    },
    "ConsoleExportSettings": {
      "ExportIntervalMs": 10000,      // Export to console every 10s
      "ExportTimeoutMs": 5000
    }
  }
}
### Configurable Export Intervals

- **To OTLP Collector**: Default every 30 seconds
- **To Console**: Default every 10 seconds
- **Customizable** - All intervals can be adjusted via configuration

## 📁 Project Structure
WebAppMetrics/
├── Controllers/
│   └── WeatherForecastController.cs    # API Controller with instrumentation
├── Services/
│   └── MetricsService.cs               # Metrics management service
├── Configuration/                      # Scripts and configurations
│   ├── otel-collector.yaml            # OpenTelemetry Collector configuration
│   ├── otel-collector.ps1             # Collector startup script
│   ├── Prometheus.yaml                # Prometheus configuration
│   ├── Prometheus.ps1                 # Prometheus startup script
│   └── grafana.ps1                    # Grafana startup script
├── Program.cs                         # Application and OpenTelemetry configuration
├── appsettings.json                   # Application configuration
└── WebAppMetrics.csproj              # Project file with NuGet packages
## 🔧 Technical Features

### OpenTelemetry Instrumentation
- **Automatic instrumentation** for ASP.NET Core requests
- **Custom metrics** for business logic
- **Runtime instrumentation** for .NET performance monitoring
- **HTTP client instrumentation** for external calls

### Metric Exporters
- **OTLP Exporter** - Push metrics to OpenTelemetry Collector
- **Console Exporter** - Real-time monitoring in application console
- **Prometheus Format** - Compatibility with Prometheus ecosystem

### API Features
- **RESTful endpoints** with complete documentation
- **Swagger/OpenAPI** with automatic documentation generation
- **XML Comments** for enhanced API documentation
- **Response types** and validation attributes

## 📈 Microservice Application Performance Analysis

This project demonstrates key aspects of microservice application monitoring:

### 🏛️ Observability Pillars
1. **Metrics** - Aggregated numerical performance data
2. **Logs** - Structured event logging
3. **Traces** - Execution path tracking (prepared for extension)

### 📊 Key Performance Indicators (KPIs)
- **Request Rate** - Number of requests per second
- **Response Time** - API response time
- **Error Rate** - Percentage of error responses
- **Resource Utilization** - CPU, memory, GC usage

### 🚀 Production Readiness
- **Configurable export intervals** for performance optimization
- **Resource-aware configuration** for different environments
- **Scalable architecture** ready for distribution

## 🐳 Docker Support

The project includes full containerization support:

- `Dockerfile` for WebAppMetrics application
- PowerShell scripts for observability infrastructure
- Preconfigured networking for inter-container communication

## 💡 Usage Examples

### 1. API Testing
curl -X GET "https://localhost:7029/api/WeatherForecast" -H "accept: application/json"
### 2. Console Metric Monitoring
Metrics are automatically displayed in the application console every 10 seconds.

### 3. Prometheus Queries
# Request rate
rate(weather_forecast_requests_total[5m])

# 95th percentile response time
histogram_quantile(0.95, weather_forecast_processing_duration_ms_bucket)

# Active requests
weather_forecast_active_requests
## 🎓 Educational Goals

This project is ideal for learning:

- **OpenTelemetry implementation** in .NET 8 applications
- **Observability patterns** in microservice architecture
- **Integration with tools** like Prometheus and Grafana
- **Best practices** in application monitoring
- **Performance tuning** of web applications

## 🤝 Contributing

The project is open for collaboration. It can be extended with:
- Additional business metrics
- Tracing instrumentation
- Alerting rules for Prometheus
- Ready-made Grafana dashboards
- Health check endpoints

## 📄 License

This project is available under the MIT license.

---

**WebAppMetrics** - Your gateway to the world of microservice application observability! 🚀📊