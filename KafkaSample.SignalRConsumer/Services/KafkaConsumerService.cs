using Confluent.Kafka;
using KafkaSample.SignalRConsumer.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaSample.SignalRConsumer.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IHubContext<KafkaHub> _KafkaHubContext;
        private readonly IConsumer<Ignore, string> _KafkaConsumer;

        public KafkaConsumerService(IHubContext<KafkaHub> kafkaHubContext)
        {
            _KafkaHubContext = kafkaHubContext;

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _KafkaConsumer = new ConsumerBuilder<Ignore, string>(conf).Build();

            _KafkaConsumer.Subscribe("test-topic");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _KafkaConsumer.Consume(stoppingToken);

                    await _KafkaHubContext.Clients.All.SendAsync("ReceiveMessage", $"{DateTime.Now.ToString("HH:mm:ss.ffff")} - {cr.Message.Value}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }

                await Task.Delay(100, stoppingToken);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _KafkaConsumer.Close();
            _KafkaConsumer.Dispose();
        }
    }
}
