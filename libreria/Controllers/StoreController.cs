using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using libreria.Models.dbModels;
using libreria.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace libreria.Controllers
{
    [Authorize(Roles = "admin")]
    public class StoreController : Controller
    {
        private readonly BookstoreManagerContext _context;

        public StoreController(BookstoreManagerContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Store
        public async Task<IActionResult> Index()
        {
            var bookstoreManagerContext = _context.Stores.Include(s => s.CityNavigation).Include(s => s.CountryNavigation).Include(s => s.State);
            return View(await bookstoreManagerContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Store/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.CityNavigation)
                .Include(s => s.CountryNavigation)
                .Include(s => s.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Store/Create
        public IActionResult Create()
        {
            ViewData["City"] = new SelectList(_context.Cities, "Id", "Id");
            ViewData["Country"] = new SelectList(_context.Countries, "Id", "Id");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Street,City,StateId,Country,Zipcode")] StoreHR store)
        {
            if (ModelState.IsValid)
            {
                Store store1 = new Store
                {
                    Name = store.Name,
                    Street = store.Street,
                    City = store.City,
                    StateId = store.StateId,
                    Country = store.Country,
                    Zipcode = store.Zipcode
                };
                _context.Stores.Add(store1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["City"] = new SelectList(_context.Cities, "Id", "Id", store.City);
            ViewData["Country"] = new SelectList(_context.Countries, "Id", "Id", store.Country);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id", store.StateId);
            return View(store);
        }

        // GET: Store/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["City"] = new SelectList(_context.Cities, "Id", "Id", store.City);
            ViewData["Country"] = new SelectList(_context.Countries, "Id", "Id", store.Country);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id", store.StateId);
            return View(store);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Street,City,StateId,Country,Zipcode")] Store store)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
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
            ViewData["City"] = new SelectList(_context.Cities, "Id", "Id", store.City);
            ViewData["Country"] = new SelectList(_context.Countries, "Id", "Id", store.Country);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id", store.StateId);
            return View(store);
        }

        // GET: Store/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.CityNavigation)
                .Include(s => s.CountryNavigation)
                .Include(s => s.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'BookstoreManagerContext.Stores'  is null.");
            }
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
          return (_context.Stores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
