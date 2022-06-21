using ConsumerDemo;
using Core;
using Kafka.Subscriber;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<ISubscriber<OrderProcessingRequest>>(x => new Consumer<OrderProcessingRequest>("localhost:9092", "quickstart-events","test_group"));
        services.AddSingleton<IHostedService, ConsumerService>();
    })
    .Build();


await host.RunAsync();