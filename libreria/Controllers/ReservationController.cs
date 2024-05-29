using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using libreria.Models.dbModels;
using libreria.Models;

namespace libreria.Controllers
{
    public class ReservationController : Controller
    {
        private readonly BookstoreManagerContext _context;

        public ReservationController(BookstoreManagerContext context)
        {
            _context = context;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var bookstoreManagerContext = _context.Reservations.Include(r => r.Book).Include(r => r.Status).Include(r => r.Store).Include(r => r.User);
            return View(await bookstoreManagerContext.ToListAsync());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.Status)
                .Include(r => r.Store)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.ReservationStatuses, "Id", "Id");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,StoreId,BookId,Quantity,StatusId,CreatedAt")] ReservationHR reservation)
        {
            if (ModelState.IsValid)
            {
                Reservation reservation1 = new Reservation
                {
                    UserId = reservation.UserId,
                    StoreId = reservation.StoreId,
                    BookId = reservation.BookId,
                    Quantity = reservation.Quantity,
                    StatusId = reservation.StatusId,
                    CreatedAt = reservation.CreatedAt
                };
                _context.Reservations.Add(reservation1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", reservation.BookId);
            ViewData["StatusId"] = new SelectList(_context.ReservationStatuses, "Id", "Id", reservation.StatusId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", reservation.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", reservation.BookId);
            ViewData["StatusId"] = new SelectList(_context.ReservationStatuses, "Id", "Id", reservation.StatusId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", reservation.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,StoreId,BookId,Quantity,StatusId,CreatedAt")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", reservation.BookId);
            ViewData["StatusId"] = new SelectList(_context.ReservationStatuses, "Id", "Id", reservation.StatusId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", reservation.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.Status)
                .Include(r => r.Store)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'BookstoreManagerContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.StatusId = 1;

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.BookId == reservation.BookId && i.StoreId == reservation.StoreId);

            if (inventory == null)
            {
                return NotFound("Inventory record not found");
            }

            if (inventory.Quantity < reservation.Quantity)
            {
                return BadRequest("Not enough stock in inventory");
            }

            inventory.Quantity -= reservation.Quantity;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = reservation.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Deny(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.StatusId = 2;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = reservation.Id });
        }

        private bool ReservationExists(int id)
        {
          return (_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
