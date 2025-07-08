podman run -d --name prometheus --network otel-net -p 0.0.0.0:9090:9090 -v prometheus.yaml:/etc/prometheus/prometheus.yml:Z prom/prometheus
