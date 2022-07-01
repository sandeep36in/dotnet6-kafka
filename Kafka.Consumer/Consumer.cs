using Confluent.Kafka;
using Core;
using System.Text.Json;

namespace Kafka.Subscriber
{
    public class Consumer<T> : ISubscriber<T> where T : class
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;
        private IConsumer<Ignore, string> _consumerBuilder;
        public Consumer(string bootstrapServers, string topic, string groupId)
        {
            _bootstrapServers = bootstrapServers;
            _topic = topic;

            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build();

            _consumerBuilder.Subscribe(_topic);
        }

        public T Consume(CancellationToken cancellationToken)
        {
            try
            {
                var consumer = _consumerBuilder.Consume(cancellationToken);
                return JsonSerializer.Deserialize<T>(consumer.Message.Value);
            }
            catch (OperationCanceledException)
            {
                _consumerBuilder.Close();
            }
            return null;
        }

        public void Dispose()
        {
            _consumerBuilder.Dispose();
        }
    }
}