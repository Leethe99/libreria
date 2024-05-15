using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using libreria.Models.dbModels;

namespace libreria.Controllers
{
    public class ReservationstatusController : Controller
    {
        private readonly BookstoreManagerContext _context;

        public ReservationstatusController(BookstoreManagerContext context)
        {
            _context = context;
        }

        // GET: Reservationstatus
        public async Task<IActionResult> Index()
        {
              return _context.ReservationStatuses != null ? 
                          View(await _context.ReservationStatuses.ToListAsync()) :
                          Problem("Entity set 'BookstoreManagerContext.ReservationStatuses'  is null.");
        }

        // GET: Reservationstatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReservationStatuses == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // GET: Reservationstatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservationstatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ReservationStatus reservationStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationStatus);
        }

        // GET: Reservationstatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReservationStatuses == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses.FindAsync(id);
            if (reservationStatus == null)
            {
                return NotFound();
            }
            return View(reservationStatus);
        }

        // POST: Reservationstatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ReservationStatus reservationStatus)
        {
            if (id != reservationStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationStatusExists(reservationStatus.Id))
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
            return View(reservationStatus);
        }

        // GET: Reservationstatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReservationStatuses == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // POST: Reservationstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReservationStatuses == null)
            {
                return Problem("Entity set 'BookstoreManagerContext.ReservationStatuses'  is null.");
            }
            var reservationStatus = await _context.ReservationStatuses.FindAsync(id);
            if (reservationStatus != null)
            {
                _context.ReservationStatuses.Remove(reservationStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationStatusExists(int id)
        {
          return (_context.ReservationStatuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
