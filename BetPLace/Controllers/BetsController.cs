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
    public class BetsController : Controller
    {
        private readonly BetPlaceContext _context;

        public BetsController(BetPlaceContext context)
        {
            _context = context;
        }

        // GET: Bets
        public async Task<IActionResult> Index()
        {
            var betPlaceContext = _context.Bet.Include(b => b.BetEvent).Include(b => b.User);
            return View(await betPlaceContext.ToListAsync());
        }

        // GET: Bets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bet == null)
            {
                return NotFound();
            }

            var bet = await _context.Bet
                .Include(b => b.BetEvent)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bet == null)
            {
                return NotFound();
            }

            return View(bet);
        }

        // GET: Bets/Create
        public IActionResult Create()
        {
            ViewData["BetEventId"] = new SelectList(_context.BetEvent, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Bets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WiningTeam,BetEventId,balance,UserId")] Bet bet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BetEventId"] = new SelectList(_context.BetEvent, "Id", "Id", bet.BetEventId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bet.UserId);
            return View(bet);
        }

        // GET: Bets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bet == null)
            {
                return NotFound();
            }

            var bet = await _context.Bet.FindAsync(id);
            if (bet == null)
            {
                return NotFound();
            }
            ViewData["BetEventId"] = new SelectList(_context.BetEvent, "Id", "Id", bet.BetEventId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bet.UserId);
            return View(bet);
        }

        // POST: Bets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WiningTeam,BetEventId,balance,UserId")] Bet bet)
        {
            if (id != bet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BetExists(bet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BetEventId"] = new SelectList(_context.BetEvent, "Id", "Id", bet.BetEventId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bet.UserId);
            return View(bet);
        }

        // GET: Bets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bet == null)
            {
                return NotFound();
            }

            var bet = await _context.Bet
                .Include(b => b.BetEvent)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bet == null)
            {
                return NotFound();
            }

            return View(bet);
        }

        // POST: Bets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bet == null)
            {
                return Problem("Entity set 'BetPlaceContext.Bet'  is null.");
            }
            var bet = await _context.Bet.FindAsync(id);
            if (bet != null)
            {
                _context.Bet.Remove(bet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BetExists(int id)
        {
          return (_context.Bet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
