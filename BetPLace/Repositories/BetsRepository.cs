using BetPlace.Data;
using BetPlace.Migrations;
using BetPlace.Models;
using Microsoft.EntityFrameworkCore;

namespace BetPlace.Repositories
{
    public class BetsRepository
    {
        BetPlaceContext _context;
        public BetsRepository(BetPlaceContext context)
        {
            _context = context;
        }

        public async Task<List<Bet>> GetBetsAsync()
        {
            var Bets = _context.Bet.Include(b => b.BetEvent).Include(b => b.User);
            return await Bets.ToListAsync();
        }

        public List<Bet> GetBets(int UserId)
        {
            return _context.Bet.Where(m => m.UserId == UserId).Include(b => b.BetEvent).ToList();
        }

        public async Task<Bet?> GetBetById(int? Id)
        {
            var bet = await _context.Bet
                .Include(b => b.BetEvent)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == Id);

            return bet;
        }

        public void AddBet(Bet bet)
        {
            _context.Add(bet);
        }

        public void UpdateBet(Bet bet)
        {
            _context.Update(bet);
        }

    }
}
