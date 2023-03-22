using BetPlaceConsole1.Data;
using Confluent.Kafka;
using System.Threading;

namespace BetPlaceConsole1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "127.0.0.1:29092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };


            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("bets");

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine(consumeResult);

                }

                consumer.Close();
            }
        }
    }
}