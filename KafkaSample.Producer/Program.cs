using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaSample.Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting");

            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                while (true)
                {
                    try
                    {
                        var dr = await p.ProduceAsync("test-topic", new Message<Null, string> { Value = "test" });

                        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    }

                    await Task.Delay(100);
                }
            }
        }
    }
}
