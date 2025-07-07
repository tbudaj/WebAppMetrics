# WebAppMetrics - .NET 8 Web API with OpenTelemetry

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue)
![OpenTelemetry](https://img.shields.io/badge/OpenTelemetry-1.8.1-orange)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-green)
![Docker](https://img.shields.io/badge/Docker-Ready-blue)

## 📋 Opis Projektu

**WebAppMetrics** to demonstracyjna aplikacja Web API napisana w .NET 8, która prezentuje implementację kompletnego stosu obserwowości aplikacji mikroserwisowych. Projekt zawiera pełną integrację z OpenTelemetry, eksportowaniem metryk do Prometheus oraz wizualizacją w Grafana.

### 🚀 Główne Funkcjonalności

- **RESTful API** - Weather Forecast endpoint z dokumentacją Swagger/OpenAPI
- **OpenTelemetry Integration** - Kompletne instrumentowanie aplikacji z metrykami
- **Custom Metrics** - Dedykowane metryki biznesowe aplikacji
- **Prometheus Export** - Eksport metryk do systemu monitoringu Prometheus
- **Grafana Dashboards** - Gotowa infrastruktura do wizualizacji metryk
- **Console Logging** - Podgląd metryk w czasie rzeczywistym w konsoli
- **Docker Support** - Pełna konteneryzacja aplikacji

## 🏗️ Architektura
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│  WebAppMetrics  │───▶│ OpenTelemetry   │───▶│   Prometheus    │───▶│     Grafana     │
│     (.NET 8)    │    │   Collector     │    │   (Metrics DB)  │    │ (Visualization) │
└─────────────────┘    └─────────────────┘    └─────────────────┘    └─────────────────┘

### 📊 Dostępne Metryki

#### Metryki Niestandardowe (Custom Metrics)
- `weather_forecast_requests_total` - Licznik żądań do API pogodowego
- `weather_forecast_processing_duration_ms` - Histogram czasu przetwarzania żądań
- `weather_forecast_active_requests` - Gauge aktywnych żądań
- `temperature_readings_total` - Licznik odczytów temperatury z kategoriami

#### Metryki Systemowe (OpenTelemetry Instrumentation)
- **ASP.NET Core Metrics** - Request duration, response size, error rates
- **HTTP Client Metrics** - Outbound HTTP calls instrumentation
- **Runtime Metrics** - GC collections, memory usage, thread pool

## ⚡ Szybki Start

### Wymagania
- .NET 8 SDK
- Docker lub Podman
- PowerShell (dla skryptów automatyzacji)

### 1. Uruchomienie Aplikacji
cd WebAppMetrics
dotnet run
Aplikacja będzie dostępna pod adresem: `https://localhost:7029`

### 2. Swagger UI
Dostęp do dokumentacji API: `https://localhost:7029/swagger`

### 3. Uruchomienie Stosu Obserwowości

#### Krok 1: Utworzenie sieci Docker
podman network create otel-net
#### Krok 2: Uruchomienie OpenTelemetry Collector
.\Configuration\otel-collector.ps1
#### Krok 3: Uruchomienie Prometheus
.\Configuration\Prometheus.ps1
#### Krok 4: Uruchomienie Grafana
.\Configuration\grafana.ps1
### 4. Dostęp do Narzędzi

| Narzędzie | URL | Credentials |
|-----------|-----|-------------|
| WebApp API | http://localhost:7029 | - |
| Swagger UI | http://localhost:7029/swagger | - |
| Prometheus | http://localhost:9090 | - |
| Grafana | http://localhost:3000 | admin/admin |

## ⚙️ Konfiguracja

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
      "ExportIntervalMs": 10000,      // Eksport na konsolę co 10s
      "ExportTimeoutMs": 5000
    }
  }
}
### Parametryzowalne Interwały Eksportu

- **Do Kolektora OTLP**: Domyślnie co 30 sekund
- **Na Konsolę**: Domyślnie co 10 sekund
- **Możliwość dostosowania** wszystkich interwałów poprzez konfigurację

## 📁 Struktura Projektu
WebAppMetrics/
├── Controllers/
│   └── WeatherForecastController.cs    # API Controller z instrumentacją
├── Services/
│   └── MetricsService.cs               # Serwis zarządzania metrykami
├── Configuration/                      # Skrypty i konfiguracje
│   ├── otel-collector.yaml            # Konfiguracja OpenTelemetry Collector
│   ├── otel-collector.ps1             # Skrypt uruchomienia Collector
│   ├── Prometheus.yaml                # Konfiguracja Prometheus
│   ├── Prometheus.ps1                 # Skrypt uruchomienia Prometheus
│   └── grafana.ps1                    # Skrypt uruchomienia Grafana
├── Program.cs                         # Konfiguracja aplikacji i OpenTelemetry
├── appsettings.json                   # Konfiguracja aplikacji
└── WebAppMetrics.csproj              # Plik projektu z pakietami NuGet
## 🔧 Funkcjonalności Techniczne

### Instrumentacja OpenTelemetry
- **Automatyczna instrumentacja** ASP.NET Core requests
- **Custom metrics** dla logiki biznesowej
- **Runtime instrumentation** dla monitorowania wydajności .NET
- **HTTP client instrumentation** dla wywołań zewnętrznych

### Eksportery Metryk
- **OTLP Exporter** - Push metryk do OpenTelemetry Collector
- **Console Exporter** - Real-time monitoring w konsoli aplikacji
- **Prometheus Format** - Kompatybilność z ekosystemem Prometheus

### API Features
- **RESTful endpoints** z pełną dokumentacją
- **Swagger/OpenAPI** z automatyczną generacją dokumentacji
- **XML Comments** dla lepszej dokumentacji API
- **Response types** i validation attributes

## 📈 Analiza Wydajności Aplikacji Mikroserwisowych

Ten projekt demonstruje kluczowe aspekty monitorowania aplikacji mikroserwisowych:

### 🏛️ Observability Pillars
1. **Metrics** - Agregowane dane liczbowe o wydajności
2. **Logs** - Strukturalne logowanie zdarzeń
3. **Traces** - Śledzenie ścieżek wykonania (przygotowane do rozszerzenia)

### 📊 Key Performance Indicators (KPIs)
- **Request Rate** - Liczba żądań na sekundę
- **Response Time** - Czas odpowiedzi API
- **Error Rate** - Procent błędnych odpowiedzi
- **Resource Utilization** - Wykorzystanie CPU, pamięci, GC

### 🚀 Production Readiness
- **Configurable export intervals** dla optymalizacji wydajności
- **Resource-aware configuration** dla różnych środowisk
- **Scalable architecture** gotowa na rozproszenie

## 🐳 Docker Support

Projekt zawiera pełne wsparcie dla konteneryzacji:

- `Dockerfile` dla aplikacji WebAppMetrics
- Skrypty PowerShell dla infrastruktury obserwowości
- Preconfigured networking dla komunikacji między kontenerami

## 💡 Przykłady Użycia

### 1. Testowanie API
curl -X GET "https://localhost:7029/api/WeatherForecast" -H "accept: application/json"
### 2. Monitorowanie Metryk w Konsoli
Metryki są automatycznie wyświetlane w konsoli aplikacji co 10 sekund.

### 3. Zapytania Prometheus
# Request rate
rate(weather_forecast_requests_total[5m])

# 95th percentile response time
histogram_quantile(0.95, weather_forecast_processing_duration_ms_bucket)

# Active requests
weather_forecast_active_requests
## 🎓 Cele Edukacyjne

Ten projekt jest idealny do nauki:

- **Implementacji OpenTelemetry** w aplikacjach .NET 8
- **Patterns obserwowości** w architekturze mikroserwisowej
- **Integracji z narzędziami** Prometheus i Grafana
- **Best practices** w monitorowaniu aplikacji
- **Performance tuning** aplikacji webowych

## 🤝 Wkład w Projekt

Projekt jest otwarty na współpracę. Można rozszerzyć o:
- Dodatkowe metryki biznesowe
- Tracing instrumentation
- Alerting rules dla Prometheus
- Gotowe dashboardy Grafana
- Health checks endpoints

## 📄 Licencja

Ten projekt jest dostępny na licencji MIT.

---

**WebAppMetrics** - Twoja brama do świata obserwowości aplikacji mikroserwisowych! 🚀📊