using Confluent.Kafka;
using Core;
using System.Text.Json;

namespace Kafka.Publisher
{
    public class Producer<T> : IPublisher<T> where T : class
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;
        private readonly string _clientIdentifier;
        private readonly IProducer<Null, string> _producer;

        public Producer(string bootstrapServers, string topic, string clientIdentifier)
        {
            _bootstrapServers = bootstrapServers;
            _topic = topic;
            _clientIdentifier = clientIdentifier;

            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                ClientId = _clientIdentifier
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task<bool> PublishAsync(T message, CancellationToken cancellationToken)
        {
            try
            {
                var msg = JsonSerializer.Serialize<T>(message);
                var result = await _producer.ProduceAsync
                (_topic, new Message<Null, string>
                {
                    Value = msg
                }, cancellationToken);

                return await Task.FromResult(true);
            }
            catch (OperationCanceledException)
            {
                _producer.Dispose();
                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                //TODO log the exception
                return await Task.FromResult(false);
            }
        }
    }
}