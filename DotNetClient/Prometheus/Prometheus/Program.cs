using System;
using System.Diagnostics;
using System.Threading;

namespace Prometheus
{
    class Program
    {
        private static readonly Counter counter =
            Metrics.CreateCounter("dot_net_counter", "My Dotnet Counter");

        private static readonly Gauge gauge =
            Metrics.CreateGauge("dot_net_gauge", "My Gauge");

        private static readonly Summary summary =
            Metrics.CreateSummary("dot_net_summary", "My Summary");

        static void RaiseException()
        {
            throw new NotImplementedException("");
        }
        
        static void Main(string[] args)
        {
            var server = new MetricServer(8000);
            server.Start();

            var watcher = new Stopwatch();
            watcher.Start();
            
                Thread.Sleep(TimeSpan.FromSeconds(1));
            
            watcher.Stop();
            
            summary.Observe(watcher.ElapsedMilliseconds);

            try
            {
                counter.CountExceptions(() => RaiseException());
            }
            catch
            {

            }
            
            while (true)
            {
                // counter.Inc();
                
                gauge.Set(100);
                gauge.Dec(10);
                
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            
            Console.WriteLine("Hello World!");
        }
    }
}