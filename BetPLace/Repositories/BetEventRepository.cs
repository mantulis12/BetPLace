using BetPlace.Data;
using BetPlace.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BetPlace.Repositories
{
    public class BetEventRepository
    {
        private BetPlaceContext _context;
        public BetEventRepository(BetPlaceContext context) 
        {
            _context = context;
        }

        public BetEvent? GetBetEventById(int? Id)
        {
            if (Id == null) return null;
            return _context.BetEvent.Where(m => m.Id == Id).FirstOrDefault();
        }

        public void AddEvent(BetEvent betEvent)
        {
            _context.Add(betEvent);
        }

        public void UpdateEvent(BetEvent betEvent)
        {
            _context.Update(betEvent);
        }

        public async Task<BetEvent?> GetBetEventAsync(int? id)
        {
            return await _context.BetEvent.FindAsync(id);
        }

        public async Task<List<BetEvent>> GetBetEventsListAsync()
        {
            return await _context.BetEvent.Where(r => r.EventEndDate >= DateTime.Now && r.IsActive == true).ToListAsync();
        }
    }
}
