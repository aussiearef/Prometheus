using System;
using System.Threading;
using Prometheus;

namespace pushgatewaydotnet
{
    class Program
    {
        static void Main(string[] args)
        {

            var register = Metrics.NewCustomRegistry();
            var factory = Metrics.WithCustomRegistry(register);

            var pusher = new MetricPusher(new MetricPusherOptions
            {
                Endpoint = "http://localhost:9091/metrics",
                Job = "Dotnet PushGateway Sample",
                Instance = "Instance One",
                Registry = register
            });

            pusher.Start();

            while (true)
            {
                var gauge = factory.CreateGauge("dot_net_pushgateway", "HELP");
                gauge.Set(new Random(DateTime.Now.Millisecond).Next() * 100);
                Thread.Sleep(100);
            }
            

            pusher.Stop();
            
            Console.WriteLine("Hello World!");
        }
    }
}