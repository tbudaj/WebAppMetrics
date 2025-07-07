# WebAppMetrics - .NET 8 Web API with OpenTelemetry

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue)
![OpenTelemetry](https://img.shields.io/badge/OpenTelemetry-1.8.1-orange)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-green)
![Docker](https://img.shields.io/badge/Docker-Ready-blue)

## ?? Opis Projektu

**WebAppMetrics** to demonstracyjna aplikacja Web API napisana w .NET 8, która prezentuje implementacjê kompletnego stosu obserwowoœci aplikacji mikroserwisowych. Projekt zawiera pe³n¹ integracjê z OpenTelemetry, eksportowaniem metryki do Prometheus oraz wizualizacj¹ w Grafana.

### ?? G³ówne Funkcjonalnoœci

- **RESTful API** - Weather Forecast endpoint z dokumentacj¹ Swagger/OpenAPI
- **OpenTelemetry Integration** - Kompletne instrumentowanie aplikacji z metrykami
- **Custom Metrics** - Dedykowane metryki biznesowe aplikacji
- **Prometheus Export** - Eksport metryki do systemu monitoringu Prometheus
- **Grafana Dashboards** - Gotowa infrastruktura do wizualizacji metryki
- **Console Logging** - Podgl¹d metryki w czasie rzeczywistym w konsoli
- **Docker Support** - Pe³na konteneryzacja aplikacji

## ??? Architektura
???????????????????    ???????????????????    ???????????????????    ???????????????????
?  WebAppMetrics  ?????? OpenTelemetry   ??????   Prometheus    ??????     Grafana     ?
?     (.NET 8)    ?    ?   Collector     ?    ?   (Metrics DB)  ?    ? (Visualization) ?
???????????????????    ???????????????????    ???????????????????    ???????????????????
### ?? Dostêpne Metryki

#### Metryki Niestandardowe (Custom Metrics)
- `weather_forecast_requests_total` - Licznik ¿¹dañ do API pogodowego
- `weather_forecast_processing_duration_ms` - Histogram czasu przetwarzania ¿¹dañ
- `weather_forecast_active_requests` - Gauge aktywnych ¿¹dañ
- `temperature_readings_total` - Licznik odczytów temperatury z kategoriami

#### Metryki Systemowe (OpenTelemetry Instrumentation)
- **ASP.NET Core Metrics** - Request duration, response size, error rates
- **HTTP Client Metrics** - Outbound HTTP calls instrumentation
- **Runtime Metrics** - GC collections, memory usage, thread pool

## ?? Szybki Start

### Wymagania
- .NET 8 SDK
- Docker lub Podman
- PowerShell (dla skryptów automatyzacji)

### 1. Uruchomienie Aplikacji
cd WebAppMetrics
dotnet run
Aplikacja bêdzie dostêpna pod adresem: `https://localhost:7029`

### 2. Swagger UI
Dostêp do dokumentacji API: `https://localhost:7029/swagger`

### 3. Uruchomienie Stosu Obserwowoœci

#### Krok 1: Utworzenie sieci Dockerpodman network create otel-net
#### Krok 2: Uruchomienie OpenTelemetry Collector.\Configuration\otel-collector.ps1
#### Krok 3: Uruchomienie Prometheus.\Configuration\Prometheus.ps1
#### Krok 4: Uruchomienie Grafana.\Configuration\grafana.ps1
### 4. Dostêp do Narzêdzi

| Narzêdzie | URL | Credentials |
|-----------|-----|-------------|
| WebApp API | http://localhost:7029 | - |
| Swagger UI | http://localhost:7029/swagger | - |
| Prometheus | http://localhost:9090 | - |
| Grafana | http://localhost:3000 | admin/admin |

## ?? Konfiguracja

### OpenTelemetry Settings (appsettings.json)
{
  "OpenTelemetry": {
    "ServiceName": "WebAppMetrics",
    "ServiceVersion": "1.0.0",
    "OtlpEndpoint": "http://localhost:4317",
    "MetricExportSettings": {
      "ExportIntervalMs": 30000,      // Eksport do kolektora co 30s
      "ExportTimeoutMs": 10000,
      "MaxExportBatchSize": 512
    },
    "ConsoleExportSettings": {
      "ExportIntervalMs": 10000,      // Eksport na konsolê co 10s
      "ExportTimeoutMs": 5000
    }
  }
}
### Parametryzowalne Interwa³y Eksportu

- **Do Kolektora OTLP**: Domyœlnie co 30 sekund
- **Na Konsolê**: Domyœlnie co 10 sekund
- **Mo¿liwoœæ dostosowania** wszystkich interwa³ów poprzez konfiguracjê

## ?? Struktura Projektu
WebAppMetrics/
??? Controllers/
?   ??? WeatherForecastController.cs    # API Controller z instrumentacj¹
??? Services/
?   ??? MetricsService.cs               # Serwis zarz¹dzania metrykami
??? Configuration/                      # Skrypty i konfiguracje
?   ??? otel-collector.yaml            # Konfiguracja OpenTelemetry Collector
?   ??? otel-collector.ps1             # Skrypt uruchomienia Collector
?   ??? Prometheus.yaml                # Konfiguracja Prometheus
?   ??? Prometheus.ps1                 # Skrypt uruchomienia Prometheus
?   ??? grafana.ps1                    # Skrypt uruchomienia Grafana
??? Program.cs                         # Konfiguracja aplikacji i OpenTelemetry
??? appsettings.json                   # Konfiguracja aplikacji
??? WebAppMetrics.csproj              # Plik projektu z pakietami NuGet
## ?? Funkcjonalnoœci Techniczne

### Instrumentacja OpenTelemetry
- **Automatyczna instrumentacja** ASP.NET Core requests
- **Custom metrics** dla logiki biznesowej
- **Runtime instrumentation** dla monitorowania wydajnoœci .NET
- **HTTP client instrumentation** dla wywo³añ zewnêtrznych

### Eksportery Metryki
- **OTLP Exporter** - Push metryki do OpenTelemetry Collector
- **Console Exporter** - Real-time monitoring w konsoli aplikacji
- **Prometheus Format** - Kompatybilnoœæ z ekosystemem Prometheus

### API Features
- **RESTful endpoints** z pe³n¹ dokumentacj¹
- **Swagger/OpenAPI** z automatyczn¹ generacj¹ dokumentacji
- **XML Comments** dla lepszej dokumentacji API
- **Response types** i validation attributes

## ?? Analiza Wydajnoœci Aplikacji Mikroserwisowych

Ten projekt demonstruje kluczowe aspekty monitorowania aplikacji mikroserwisowych:

### ?? Observability Pillars
1. **Metrics** - Agregowane dane liczbowe o wydajnoœci
2. **Logs** - Strukturalne logowanie zdarzeñ
3. **Traces** - Œledzenie œcie¿ek wykonania (przygotowane do rozszerzenia)

### ?? Key Performance Indicators (KPIs)
- **Request Rate** - Liczba ¿¹dañ na sekundê
- **Response Time** - Czas odpowiedzi API
- **Error Rate** - Procent b³êdnych odpowiedzi
- **Resource Utilization** - Wykorzystanie CPU, pamiêci, GC

### ?? Production Readiness
- **Configurable export intervals** dla optymalizacji wydajnoœci
- **Resource-aware configuration** dla ró¿nych œrodowisk
- **Scalable architecture** gotowa na rozproszenie

## ?? Docker Support

Projekt zawiera pe³ne wsparcie dla konteneryzacji:

- `Dockerfile` dla aplikacji WebAppMetrics
- Skrypty PowerShell dla infrastruktury obserwowoœci
- Preconfigured networking dla komunikacji miêdzy kontenerami

## ?? Przyk³ady U¿ycia

### 1. Testowanie APIcurl -X GET "https://localhost:7029/api/WeatherForecast" -H "accept: application/json"
### 2. Monitorowanie Metryki w Konsoli
Metryki s¹ automatycznie wyœwietlane w konsoli aplikacji co 10 sekund.

### 3. Zapytania Prometheus# Request rate
rate(weather_forecast_requests_total[5m])

# 95th percentile response time
histogram_quantile(0.95, weather_forecast_processing_duration_ms_bucket)

# Active requests
weather_forecast_active_requests
## ?? Cele Edukacyjne

Ten projekt jest idealny do nauki:

- **Implementacji OpenTelemetry** w aplikacjach .NET 8
- **Patterns obserwowoœci** w architekturze mikroserwisowej
- **Integracji z narzêdziami** Prometheus i Grafana
- **Best practices** w monitorowaniu aplikacji
- **Performance tuning** aplikacji webowych

## ?? Wk³ad w Projekt

Projekt jest otwarty na wspó³pracê. Mo¿na rozszerzyæ o:
- Dodatkowe metryki biznesowe
- Tracing instrumentation
- Alerting rules dla Prometheus
- Gotowe dashboardy Grafana
- Health checks endpoints

## ?? Licencja

Ten projekt jest dostêpny na licencji MIT.

---

**WebAppMetrics** - Twoja brama do œwiata obserwowoœci aplikacji mikroserwisowych! ????