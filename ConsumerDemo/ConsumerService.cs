using Core;
using Microsoft.Extensions.Hosting;

namespace ConsumerDemo
{
    internal class ConsumerService : IHostedService
    {
        private readonly ISubscriber<OrderProcessingRequest> _subscriber;

        public ConsumerService(ISubscriber<OrderProcessingRequest> subscriber)
        {
            _subscriber = subscriber;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    var obj = _subscriber.Consume(cancellationToken);
                    obj.Display();
                }
                catch (Exception ex) { 
                    Console.WriteLine(ex.ToString());
                }
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Dispose();
            return Task.CompletedTask;
        }
    }
}
