﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetPlace.Data;
using BetPlace.Models;
using BetPlace.Services;
using BetPlace.Repositories;

namespace BetPlace.Controllers
{
    public class EventResultsController : Controller
    {
        private readonly BetPlaceContext _context;
        private EventResultsRepository _eventResultsRepository;

        public EventResultsController(BetPlaceContext context)
        {
            _context = context;
            _eventResultsRepository = new EventResultsRepository(context);
        }

        // GET: EventResults
        public async Task<IActionResult> Index()
        {
            return View(await _eventResultsRepository.GetEventResultsListAsync());
        }

        // GET: EventResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventResult == null)
            {
                return NotFound();
            }

            var eventResult = await _eventResultsRepository.GetEventsResultsAsync(id);
            if (eventResult == null)
            {
                return NotFound();
            }

            return View(eventResult);
        }

        // GET: EventResults/Create
        public IActionResult Create()
        {
            ViewData["BetEventId"] = new SelectList(_context.BetEvent.Where(m => m.IsActive == true), "Id", "Id");
            return View();
        }

        // POST: EventResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WiningTeam,BetEventId")] EventResult eventResult)
        {
            _eventResultsRepository.AddResult(eventResult);
            _context.SaveChanges();
            EventService eventService = new EventService(_context);
            eventService.ResultEventKafka(eventResult.BetEventId, eventResult);

            return RedirectToAction(nameof(Index));
        }

        // GET: EventResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventResult == null)
            {
                return NotFound();
            }

            var eventResult = await _eventResultsRepository.GetResultWithoutBetEvent(id);
            if (eventResult == null)
            {
                return NotFound();
            }
            ViewData["BetEventId"] = new SelectList(_context.BetEvent, "Id", "Id", eventResult.BetEventId);
            return View(eventResult);
        }

        // POST: EventResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WiningTeam,BetEventId")] EventResult eventResult)
        {
            if (id != eventResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _eventResultsRepository.UpdateResult(eventResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventResultExists(eventResult.Id))
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
            ViewData["BetEventId"] = new SelectList(_context.BetEvent, "Id", "Id", eventResult.BetEventId);
            return View(eventResult);
        }

        // GET: EventResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventResult == null)
            {
                return NotFound();
            }

            var eventResult = await _eventResultsRepository.GetEventsResultsAsync(id);
            if (eventResult == null)
            {
                return NotFound();
            }

            return View(eventResult);
        }

        // POST: EventResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventResult == null)
            {
                return Problem("Entity set 'BetPlaceContext.EventResult'  is null.");
            }
            var eventResult = await _eventResultsRepository.GetEventsResultsAsync(id);
            if (eventResult != null)
            {
                _eventResultsRepository.RemoveResultEvent(eventResult);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventResultExists(int id)
        {
          return (_context.EventResult?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
