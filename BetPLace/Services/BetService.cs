using BetPlace.Data;
using BetPlace.Models;
using BetPlace.Repositories;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BetPlace.Services
{
    public class BetService
    {
        private BetPlaceContext _context;
        private JwtService _jwtService;
        private BalanceLogRepository _balanceLogRepository;
        private UserRepository _userRepository;
        private BetEventRepository _betEventRepository;
        private BetRepository _betRepository;
        public BetService(BetPlaceContext context)
        {
            _context = context;
            _jwtService = new JwtService();
            _balanceLogRepository = new BalanceLogRepository(_context);
            _userRepository = new UserRepository(_context);
            _betEventRepository = new BetEventRepository(_context);
            _betRepository = new BetRepository(_context);
        }

        public void MakeBet(int EventId, string Token, decimal amount, string WinningTeam)
        {
            var principle = _jwtService.GetPrincipalFromToken(Token);
            var claims = principle.Claims.First().Value;
            var UserId = _userRepository.GetUserIdByEmail(claims);
            // Checking if balance is good
            User? user = _userRepository.GetUserById(UserId);

            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            var balance = user.Balance;

            if (balance < amount)
            {
                throw new Exception("Balance is not enough");
            }
            // taking balance

            _userRepository.TakeUserAmount(user, amount);

            _balanceLogRepository.AddBalanceLog(amount, balance, user.Id, 1);

            BetEvent? betEvent = _betEventRepository.GetBetEventById(UserId);

            if (betEvent == null) {
                throw new Exception("Bet event not found");
            }

            var coef = betEvent.coef0;

            if (betEvent.Team1 == WinningTeam)
            {
                coef = betEvent.coef1;
            } else if (betEvent.Team2 == WinningTeam) {
                coef = betEvent.coef2;
            }

            // adding bet
            _betRepository.AddBet(amount, EventId, user.Id, WinningTeam, coef);

            _context.SaveChanges();
        }
        
    }
}
