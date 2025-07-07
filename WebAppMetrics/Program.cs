using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using System.Diagnostics.Metrics;
using WebAppMetrics.Services;

var builder = WebApplication.CreateBuilder(args);

// Create custom meter for application metrics
var meter = new Meter("WebAppMetrics", "1.0.0");

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebAppMetrics API",
        Version = "v1",
        Description = "API for WebAppMetrics application with OpenTelemetry metrics",
        Contact = new OpenApiContact
        {
            Name = "Development Team",
            Email = "dev@webappmetrics.com"
        }
    });

    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Register services
builder.Services.AddSingleton(meter);
builder.Services.AddSingleton<MetricsService>();

// Get OpenTelemetry configuration
var otlpEndpoint = builder.Configuration.GetValue<string>("OpenTelemetry:OtlpEndpoint") ?? "http://localhost:4317";
var otlpHeaders = builder.Configuration.GetValue<string>("OpenTelemetry:Headers") ?? "";
var serviceName = builder.Configuration.GetValue<string>("OpenTelemetry:ServiceName") ?? "WebAppMetrics";
var serviceVersion = builder.Configuration.GetValue<string>("OpenTelemetry:ServiceVersion") ?? "1.0.0";

// Metric export intervals configuration
var metricExportInterval = builder.Configuration.GetValue<int>("OpenTelemetry:MetricExportSettings:ExportIntervalMs", 30000);
var metricExportTimeout = builder.Configuration.GetValue<int>("OpenTelemetry:MetricExportSettings:ExportTimeoutMs", 10000);

var consoleExportInterval = builder.Configuration.GetValue<int>("OpenTelemetry:ConsoleExportSettings:ExportIntervalMs", 10000);
var consoleExportTimeout = builder.Configuration.GetValue<int>("OpenTelemetry:ConsoleExportSettings:ExportTimeoutMs", 5000);

// Configure OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName, serviceVersion)
        .AddAttributes(new Dictionary<string, object>
        {
            ["environment"] = builder.Environment.EnvironmentName,
            ["application"] = serviceName,
            ["host.name"] = Environment.MachineName
        }))
    .WithMetrics(metrics => metrics
        // Add instrumentation for ASP.NET Core
        .AddAspNetCoreInstrumentation()
        // Add instrumentation for HTTP client calls
        .AddHttpClientInstrumentation()
        // Add runtime instrumentation (GC, memory, etc.)
        .AddRuntimeInstrumentation()
        // Add custom meter
        .AddMeter("WebAppMetrics")
        // Add console exporter for debugging with configurable interval
        .AddConsoleExporter((exporterOptions, metricReaderOptions) =>
        {
            exporterOptions.Targets = OpenTelemetry.Exporter.ConsoleExporterOutputTargets.Console;
            metricReaderOptions.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = consoleExportInterval;
            metricReaderOptions.PeriodicExportingMetricReaderOptions.ExportTimeoutMilliseconds = consoleExportTimeout;
        })
        // Add OTLP exporter for sending to collector with configurable interval
        .AddOtlpExporter((otlpOptions, metricReaderOptions) =>
        {
            // Configure OTLP endpoint
            otlpOptions.Endpoint = new Uri(otlpEndpoint);
            otlpOptions.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
            
            // Add headers if needed for authentication
            if (!string.IsNullOrEmpty(otlpHeaders))
            {
                otlpOptions.Headers = otlpHeaders;
            }
            
            // Configure export intervals
            metricReaderOptions.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = metricExportInterval;
            metricReaderOptions.PeriodicExportingMetricReaderOptions.ExportTimeoutMilliseconds = metricExportTimeout;
        }));

var app = builder.Build();

// Log OpenTelemetry configuration
app.Logger.LogInformation("OpenTelemetry configured:");
app.Logger.LogInformation("- Service: {ServiceName} v{ServiceVersion}", serviceName, serviceVersion);
app.Logger.LogInformation("- OTLP Endpoint: {OtlpEndpoint}", otlpEndpoint);
app.Logger.LogInformation("- Metric Export Interval: {MetricInterval}ms", metricExportInterval);
app.Logger.LogInformation("- Console Export Interval: {ConsoleInterval}ms", consoleExportInterval);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAppMetrics API V1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "WebAppMetrics API Documentation";
        c.DisplayOperationId();
        c.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
