using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kcal.Data;
using Kcal.Models;
using System.Security.Claims;

namespace Kcal.Controllers
{
    public class ConsumptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsumptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consumptions
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var consumptions = _context.Consumption.Where(x => x.UserId == userId).ToList();
                return consumptions != null ?
                            View(consumptions) :
                            Problem("Entity set 'ApplicationDbContext.Consumption'  is null.");
            }

            return Problem("User is not logged in");

        }

        // GET: Consumptions/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.Consumption == null)
            {
                return NotFound();
            }

            var consumption = await _context.Consumption
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumption == null)
            {
                return NotFound();
            }

            return View(consumption);
        }

        // GET: Consumptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consumptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,TotalKcal,Date")] Consumption consumption)
        {
            consumption.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ModelState.Clear();
            TryValidateModel(consumption);
            ModelState.Remove("User");
            
            if (ModelState.IsValid)
            {
                _context.Add(consumption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consumption);
        }

        // GET: Consumptions/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null || _context.Consumption == null)
            {
                return NotFound();
            }

            var consumption = await _context.Consumption.FindAsync(id);
            if (consumption == null)
            {
                return NotFound();
            }
            return View(consumption);
        }

        // POST: Consumptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,ProductName,TotalKcal,Date")] Consumption consumption)
        {
            if (id != consumption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumptionExists(consumption.Id))
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
            return View(consumption);
        }

        // GET: Consumptions/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.Consumption == null)
            {
                return NotFound();
            }

            var consumption = await _context.Consumption
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumption == null)
            {
                return NotFound();
            }

            return View(consumption);
        }

        // POST: Consumptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.Consumption == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consumption'  is null.");
            }
            var consumption = await _context.Consumption.FindAsync(id);
            if (consumption != null)
            {
                _context.Consumption.Remove(consumption);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumptionExists(uint id)
        {
          return (_context.Consumption?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
