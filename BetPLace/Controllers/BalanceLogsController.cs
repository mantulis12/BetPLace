using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetPlace.Data;
using BetPlace.Models;

namespace BetPlace.Controllers
{
    public class BalanceLogsController : Controller
    {
        private readonly BetPlaceContext _context;

        public BalanceLogsController(BetPlaceContext context)
        {
            _context = context;
        }

        // GET: BalanceLogs
        public async Task<IActionResult> Index()
        {
              return _context.BalanceLog_1 != null ? 
                          View(await _context.BalanceLog_1.ToListAsync()) :
                          Problem("Entity set 'BetPlaceContext.BalanceLog_1'  is null.");
        }

        // GET: BalanceLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BalanceLog_1 == null)
            {
                return NotFound();
            }

            var balanceLog = await _context.BalanceLog_1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (balanceLog == null)
            {
                return NotFound();
            }

            return View(balanceLog);
        }
    }
}
