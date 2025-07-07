# WebAppMetrics - .NET 8 Web API with OpenTelemetry

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue)
![OpenTelemetry](https://img.shields.io/badge/OpenTelemetry-1.8.1-orange)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-green)
![Docker](https://img.shields.io/badge/Docker-Ready-blue)

## ?? Opis Projektu

**WebAppMetrics** to demonstracyjna aplikacja Web API napisana w .NET 8, kt�ra prezentuje implementacj� kompletnego stosu obserwowo�ci aplikacji mikroserwisowych. Projekt zawiera pe�n� integracj� z OpenTelemetry, eksportowaniem metryki do Prometheus oraz wizualizacj� w Grafana.

### ?? G��wne Funkcjonalno�ci

- **RESTful API** - Weather Forecast endpoint z dokumentacj� Swagger/OpenAPI
- **OpenTelemetry Integration** - Kompletne instrumentowanie aplikacji z metrykami
- **Custom Metrics** - Dedykowane metryki biznesowe aplikacji
- **Prometheus Export** - Eksport metryki do systemu monitoringu Prometheus
- **Grafana Dashboards** - Gotowa infrastruktura do wizualizacji metryki
- **Console Logging** - Podgl�d metryki w czasie rzeczywistym w konsoli
- **Docker Support** - Pe�na konteneryzacja aplikacji

## ??? Architektura
???????????????????    ???????????????????    ???????????????????    ???????????????????
?  WebAppMetrics  ?????? OpenTelemetry   ??????   Prometheus    ??????     Grafana     ?
?     (.NET 8)    ?    ?   Collector     ?    ?   (Metrics DB)  ?    ? (Visualization) ?
???????????????????    ???????????????????    ???????????????????    ???????????????????
### ?? Dost�pne Metryki

#### Metryki Niestandardowe (Custom Metrics)
- `weather_forecast_requests_total` - Licznik ��da� do API pogodowego
- `weather_forecast_processing_duration_ms` - Histogram czasu przetwarzania ��da�
- `weather_forecast_active_requests` - Gauge aktywnych ��da�
- `temperature_readings_total` - Licznik odczyt�w temperatury z kategoriami

#### Metryki Systemowe (OpenTelemetry Instrumentation)
- **ASP.NET Core Metrics** - Request duration, response size, error rates
- **HTTP Client Metrics** - Outbound HTTP calls instrumentation
- **Runtime Metrics** - GC collections, memory usage, thread pool

## ?? Szybki Start

### Wymagania
- .NET 8 SDK
- Docker lub Podman
- PowerShell (dla skrypt�w automatyzacji)

### 1. Uruchomienie Aplikacji
cd WebAppMetrics
dotnet run
Aplikacja b�dzie dost�pna pod adresem: `https://localhost:7029`

### 2. Swagger UI
Dost�p do dokumentacji API: `https://localhost:7029/swagger`

### 3. Uruchomienie Stosu Obserwowo�ci

#### Krok 1: Utworzenie sieci Dockerpodman network create otel-net
#### Krok 2: Uruchomienie OpenTelemetry Collector.\Configuration\otel-collector.ps1
#### Krok 3: Uruchomienie Prometheus.\Configuration\Prometheus.ps1
#### Krok 4: Uruchomienie Grafana.\Configuration\grafana.ps1
### 4. Dost�p do Narz�dzi

| Narz�dzie | URL | Credentials |
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
      "ExportIntervalMs": 10000,      // Eksport na konsol� co 10s
      "ExportTimeoutMs": 5000
    }
  }
}
### Parametryzowalne Interwa�y Eksportu

- **Do Kolektora OTLP**: Domy�lnie co 30 sekund
- **Na Konsol�**: Domy�lnie co 10 sekund
- **Mo�liwo�� dostosowania** wszystkich interwa��w poprzez konfiguracj�

## ?? Struktura Projektu
WebAppMetrics/
??? Controllers/
?   ??? WeatherForecastController.cs    # API Controller z instrumentacj�
??? Services/
?   ??? MetricsService.cs               # Serwis zarz�dzania metrykami
??? Configuration/                      # Skrypty i konfiguracje
?   ??? otel-collector.yaml            # Konfiguracja OpenTelemetry Collector
?   ??? otel-collector.ps1             # Skrypt uruchomienia Collector
?   ??? Prometheus.yaml                # Konfiguracja Prometheus
?   ??? Prometheus.ps1                 # Skrypt uruchomienia Prometheus
?   ??? grafana.ps1                    # Skrypt uruchomienia Grafana
??? Program.cs                         # Konfiguracja aplikacji i OpenTelemetry
??? appsettings.json                   # Konfiguracja aplikacji
??? WebAppMetrics.csproj              # Plik projektu z pakietami NuGet
## ?? Funkcjonalno�ci Techniczne

### Instrumentacja OpenTelemetry
- **Automatyczna instrumentacja** ASP.NET Core requests
- **Custom metrics** dla logiki biznesowej
- **Runtime instrumentation** dla monitorowania wydajno�ci .NET
- **HTTP client instrumentation** dla wywo�a� zewn�trznych

### Eksportery Metryki
- **OTLP Exporter** - Push metryki do OpenTelemetry Collector
- **Console Exporter** - Real-time monitoring w konsoli aplikacji
- **Prometheus Format** - Kompatybilno�� z ekosystemem Prometheus

### API Features
- **RESTful endpoints** z pe�n� dokumentacj�
- **Swagger/OpenAPI** z automatyczn� generacj� dokumentacji
- **XML Comments** dla lepszej dokumentacji API
- **Response types** i validation attributes

## ?? Analiza Wydajno�ci Aplikacji Mikroserwisowych

Ten projekt demonstruje kluczowe aspekty monitorowania aplikacji mikroserwisowych:

### ?? Observability Pillars
1. **Metrics** - Agregowane dane liczbowe o wydajno�ci
2. **Logs** - Strukturalne logowanie zdarze�
3. **Traces** - �ledzenie �cie�ek wykonania (przygotowane do rozszerzenia)

### ?? Key Performance Indicators (KPIs)
- **Request Rate** - Liczba ��da� na sekund�
- **Response Time** - Czas odpowiedzi API
- **Error Rate** - Procent b��dnych odpowiedzi
- **Resource Utilization** - Wykorzystanie CPU, pami�ci, GC

### ?? Production Readiness
- **Configurable export intervals** dla optymalizacji wydajno�ci
- **Resource-aware configuration** dla r�nych �rodowisk
- **Scalable architecture** gotowa na rozproszenie

## ?? Docker Support

Projekt zawiera pe�ne wsparcie dla konteneryzacji:

- `Dockerfile` dla aplikacji WebAppMetrics
- Skrypty PowerShell dla infrastruktury obserwowo�ci
- Preconfigured networking dla komunikacji mi�dzy kontenerami

## ?? Przyk�ady U�ycia

### 1. Testowanie APIcurl -X GET "https://localhost:7029/api/WeatherForecast" -H "accept: application/json"
### 2. Monitorowanie Metryki w Konsoli
Metryki s� automatycznie wy�wietlane w konsoli aplikacji co 10 sekund.

### 3. Zapytania Prometheus# Request rate
rate(weather_forecast_requests_total[5m])

# 95th percentile response time
histogram_quantile(0.95, weather_forecast_processing_duration_ms_bucket)

# Active requests
weather_forecast_active_requests
## ?? Cele Edukacyjne

Ten projekt jest idealny do nauki:

- **Implementacji OpenTelemetry** w aplikacjach .NET 8
- **Patterns obserwowo�ci** w architekturze mikroserwisowej
- **Integracji z narz�dziami** Prometheus i Grafana
- **Best practices** w monitorowaniu aplikacji
- **Performance tuning** aplikacji webowych

## ?? Wk�ad w Projekt

Projekt jest otwarty na wsp�prac�. Mo�na rozszerzy� o:
- Dodatkowe metryki biznesowe
- Tracing instrumentation
- Alerting rules dla Prometheus
- Gotowe dashboardy Grafana
- Health checks endpoints

## ?? Licencja

Ten projekt jest dost�pny na licencji MIT.

---

**WebAppMetrics** - Twoja brama do �wiata obserwowo�ci aplikacji mikroserwisowych! ????