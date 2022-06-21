
using Core;
using Kafka.Publisher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProducerDemo;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IPublisher<Order>>(x => new Producer<Order>("localhost:9092", "quickstart-events","OrderClient"));
        services.AddSingleton<IHostedService, ProducerService>();
    })
    .Build();


await host.RunAsync();