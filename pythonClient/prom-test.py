
from prometheus_client import start_http_server, Summary, Counter, Gauge
import random
import time

REQUEST_TIME = Summary("request_processing_second", "Time spent prconessing a function")
MY_COUNTER = Counter("my_counter","", ["name" ,"age"])
MY_GAUGE = Gauge("my_gauge","")


def process_request(t):
    MY_COUNTER.labels(name="Joe", age=30).inc(3)
    time.sleep(t)

if __name__ == '__main__':
    start_http_server(8000)
    
    process_request(random.random())

    while True:
        A=1

    print("The end");
