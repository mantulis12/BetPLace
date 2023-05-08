using BetPlace.Data;
using BetPlace.Models;
using Confluent.Kafka;
using Serilog;

namespace BetPlace.Services
{
    public class EventService
    {

        private BetPlaceContext _context;
        public EventService(BetPlaceContext context)
        {
            _context = context;
        }

        public void ResultEventKafka(int EventId, EventResult eventResult)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "127.0.0.1:29092",
            };

            using (var producer = new ProducerBuilder<int, string>(config).Build())
            {
                producer.Produce("bets", new Message<int, string> { Key=EventId, Value=eventResult.WiningTeam });
                Log.Information("Events had been produced and sent to Kafka");
            }
        }
    }
}
