from prometheus_client import push_to_gateway, CollectorRegistry, Gauge, registry, utils
from prometheus_client import exposition
from prometheus_client.exposition import basic_auth_handler

def auth_handler(url, method, timeout, headers, data):
   return basic_auth_handler(url, method, timeout, headers, data, "admin", "password")

registry = CollectorRegistry()

gauge = Gauge("python_push_to_gateway", "python_push_to_gateway", registry= registry)

while True:
    gauge.set_to_current_time()
    push_to_gateway("localhost:9091", job="Job A", registry = registry, handler=auth_handler)