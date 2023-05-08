using BetPlace.Data;
using BetPlace.Models;
using Microsoft.EntityFrameworkCore;

namespace BetPlace.Repositories
{
    public class UserRepository
    {
        private BetPlaceContext _context;
        public UserRepository(BetPlaceContext context)
        {
            _context = context;
        }

        public int GetUserIdByEmail(string email)
        {
            return _context.User.Where(m => m.Email == email).First().Id;
        }

        public User? GetUserById(int Id)
        {
            return _context.User.Where(m=> m.Id == Id).FirstOrDefault();
        }        
        
        public async Task<User?> GetUserByIdAsync(int? Id)
        {
            return await _context.User.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public void TakeUserAmount(User User, decimal Amount)
        {
            User.Balance = User.Balance - Amount;
            _context.Update(User);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _context.User.ToListAsync();
        }

        public void AddUser(User User)
        {
            _context.User.Add(User);
        }

        public void UpdateUser(User User)
        {
            _context.User.Update(User);
        }

        public void DeleteUser(User User)
        {
            _context.User.Remove(User);
        }

        public bool IfUserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
