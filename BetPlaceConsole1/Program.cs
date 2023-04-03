using BetPlace.Models;
using BetPlaceConsole1.Data;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace BetPlaceConsole1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _context = new BetPlaceContext();

            var config = new ConsumerConfig
            {
                BootstrapServers = "127.0.0.1:29092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };


            using (var consumer = new ConsumerBuilder<int, string>(config).Build())
            {
                consumer.Subscribe("bets");

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    var WinningTeam = consumeResult.Value;
                    int EventId = consumeResult.Key;


                    List<Bet> bets = _context.Bet.Where(m => m.BetEventId == EventId).ToList();

                    var BetEvent = _context.BetEvent.Where(m => m.Id == EventId).FirstOrDefault();

                    if (BetEvent != null)
                    {
                        BetEvent.IsActive = false;
                        _context.Update(BetEvent);
                    }

                    foreach (var bet in bets)
                    {
                        User? user = _context.User
                        .FirstOrDefault(m => m.Id == bet.UserId);

                        if (bet.status != 0)
                        {
                            continue;
                        }

                        if (WinningTeam == bet.WiningTeam)
                        {

                            bet.status = 1;
                            _context.Update(bet);
                            if (user == null)
                            {
                                continue;
                            }

                            decimal amount = Decimal.Multiply(bet.balance, bet.coef);


                            BalanceLog balanceLog = new BalanceLog
                            {
                                UserId = bet.UserId,
                                Change = amount,
                                CurrentBalance = user.Balance,
                                OperationType = 2
                            };

                            _context.Add(balanceLog);

                            user.Balance += amount;

                            _context.Update(user);
                        }
                        else
                        {
                            bet.status = 2;
                            _context.Update(bet);
                        }
                    }

                    _context.SaveChanges();
                    Console.WriteLine("Event "+EventId+" resulted");

                }

                consumer.Close();
            }
        }
    }
}