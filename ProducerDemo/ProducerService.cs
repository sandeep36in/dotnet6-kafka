using Core;
using Microsoft.Extensions.Hosting;

namespace ProducerDemo
{
    internal class ProducerService : IHostedService
    {
        private readonly IPublisher<Order> _publisher;

        public ProducerService(IPublisher<Order> publisher)
        {
            _publisher = publisher;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                var order = Order.GetNewOrder();
                var result =  await _publisher.PublishAsync(order, cancellationToken);
                if (result)
                {
                    Console.WriteLine($"Published -{result} {order.GetDetails()}");
                }
                Thread.Sleep(1000);
            }
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
