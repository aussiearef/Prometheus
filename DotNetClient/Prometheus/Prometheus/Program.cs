using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Prometheus
{
    class Program
    {
       
        private static readonly Counter counter =
            Metrics.CreateCounter("dot_net_counter", "My Dotnet Counter", new[]{"foo", "bar"});

        private static readonly Gauge gauge =
            Metrics.CreateGauge("dot_net_gauge", "My Gauge", new GaugeConfiguration()
            {
                LabelNames = new []{"foo", "bar"},
                StaticLabels = new Dictionary<string,string>(){{"environment","dev"}}
            });

        private static readonly Summary summary =
            Metrics.CreateSummary("dot_net_summary", "My Summary");

        static void RaiseException()
        {
            throw new NotImplementedException("");
        }
        
        static void Main(string[] args)
        {
            
            Metrics.DefaultRegistry.SetStaticLabels(new Dictionary<string, string>(){{"country","US"}});
            
            var server = new MetricServer(8000);
            server.Start();

            var watcher = new Stopwatch();
            watcher.Start();
            
                Thread.Sleep(TimeSpan.FromSeconds(1));
            
            watcher.Stop();
            
            summary.Observe(watcher.ElapsedMilliseconds);

            try
            {
                counter.WithLabels("1" ,"2").CountExceptions(() => RaiseException());
            }
            catch
            {

            }
            
            while (true)
            {
                // counter.Inc();
                
                gauge.WithLabels(new[]{"1" ,"2"}).Set(100);
                
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            
            Console.WriteLine("Hello World!");
        }
    }
}