﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetPlace.Data;
using BetPlace.Models;
using NuGet.Protocol;
using BetPlace.Services;
using Azure.Core;
using Serilog;
using BetPlace.Repositories;

namespace BetPlace.Controllers
{
    public class BetEventsController : Controller
    {
        private readonly BetPlaceContext _context;
        private BetEventRepository _betEventRepository;

        public BetEventsController(BetPlaceContext context)
        {
            _context = context;
            _betEventRepository = new BetEventRepository(_context);
        }

        // GET: BetEvents
        public async Task<IActionResult> Index()
        {
              return _context.BetEvent != null ? 
                          View(await _betEventRepository.GetBetEventsListAsync()) :
                          Problem("Entity set 'BetPlaceContext.BetEvent'  is null.");
        }        
        
        // GET: ApiGetAll
        public async Task<string> ApiGetAll()
        {
            var EventList = await _betEventRepository.GetBetEventsListAsync();

            return _context.BetEvent != null ? 
                        EventList.ToJson() :
                        "Entity set 'BetPlaceContext.BetEvent'  is null.";
        }

        // GET: ApiPostBet
        [HttpPost]
        public async Task<string> ApiPostBet([FromBody] BetPlaceModel betPlaceModel)
        {
            BetService betService = new BetService(_context);

            try
            {
                betService.MakeBet(betPlaceModel.EventId, betPlaceModel.Token, betPlaceModel.amount, betPlaceModel.WinningTeam);
            } catch (Exception ex) {
                return ex.Message;
            }

            return "success";
        }

        // GET: BetEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BetEvent == null)
            {
                return NotFound();
            }

            var betEvent = _betEventRepository.GetBetEventById(id);
            if (betEvent == null)
            {
                return NotFound();
            }

            return View(betEvent);
        }

        // GET: BetEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BetEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Team1,Team2,EventStartDate,EventEndDate,coef1,coef0,coef2,Team1Description,Team2Description,IsActive")] BetEvent betEvent)
        {
            if (ModelState.IsValid)
            {
                _betEventRepository.AddEvent(betEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(betEvent);
        }

        // GET: BetEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BetEvent == null)
            {
                return NotFound();
            }

            var betEvent = await _betEventRepository.GetBetEventAsync(id);
            if (betEvent == null)
            {
                return NotFound();
            }
            return View(betEvent);
        }

        // POST: BetEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Team1,Team2,EventStartDate,EventEndDate,coef1,coef0,coef2,Team1Description,Team2Description,IsActive")] BetEvent betEvent)
        {
            if (id != betEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _betEventRepository.UpdateEvent(betEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BetEventExists(betEvent.Id))
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
            return View(betEvent);
        }

        // GET: BetEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BetEvent == null)
            {
                return NotFound();
            }

            var betEvent = await _betEventRepository.GetBetEventAsync(id);
            if (betEvent == null)
            {
                return NotFound();
            }

            return View(betEvent);
        }

        // POST: BetEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BetEvent == null)
            {
                return Problem("Entity set 'BetPlaceContext.BetEvent'  is null.");
            }
            var betEvent = await _context.BetEvent.FindAsync(id);
            if (betEvent != null)
            {
                _context.BetEvent.Remove(betEvent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BetEventExists(int id)
        {
          return (_context.BetEvent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
