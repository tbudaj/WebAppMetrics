podman run -d --name grafana --network otel-net -p 0.0.0.0:3000:3000 -v grafana-storage:/var/lib/grafana grafana/grafana
