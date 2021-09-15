from prometheus_client import push_to_gateway, CollectorRegistry, Gauge, registry

registry = CollectorRegistry()

gauge = Gauge("python_push_to_gateway", "python_push_to_gateway", registry= registry)

while True:
    gauge.set_to_current_time()
    push_to_gateway("localhost:9091", job="Job A", registry = registry)