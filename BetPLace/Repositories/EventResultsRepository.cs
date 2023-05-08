using BetPlace.Data;
using BetPlace.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BetPlace.Repositories
{
    public class EventResultsRepository
    {
        private BetPlaceContext _context;
        public EventResultsRepository(BetPlaceContext context) 
        {
            _context = context;
        }

        public EventResult? GetEventResultById(int? Id)
        {
            if (Id == null) return null;
            return _context.EventResult.Where(m => m.Id == Id).FirstOrDefault();
        }

        public void AddResult(EventResult eventResult)
        {
            _context.Add(eventResult);
        }

        public void UpdateResult(EventResult eventResult)
        {
            _context.Update(eventResult);
        }

        public async Task<EventResult?> GetEventsResultsAsync(int? id)
        {
            return await _context.EventResult.Include(e => e.BetEvent).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<EventResult?> GetResultWithoutBetEvent(int? id)
        {
            return await _context.EventResult.FindAsync(id);
        }

        public async Task<List<EventResult>> GetEventResultsListAsync()
        {
            return await _context.EventResult.Include(e => e.BetEvent).ToListAsync();
        }        
        
        public void RemoveResultEvent(EventResult eventResult)
        {
            _context.EventResult.Remove(eventResult);
        }
    }
}
