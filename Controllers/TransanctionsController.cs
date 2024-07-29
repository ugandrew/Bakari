using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bakari.Data;
using Bakari.Models;

namespace Bakari.Controllers
{
    public class TransanctionsController : Controller
    {
        private readonly BakariContext _context;

        public TransanctionsController(BakariContext context)
        {
            _context = context;
        }

        // GET: Transanctions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transanction.ToListAsync());
        }

        // GET: Transanctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transanction = await _context.Transanction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transanction == null)
            {
                return NotFound();
            }

            return View(transanction);
        }

        // GET: Transanctions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transanctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Amount,Description,Date,PerformedBy")] Transanction transanction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transanction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transanction);
        }

        // GET: Transanctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transanction = await _context.Transanction.FindAsync(id);
            if (transanction == null)
            {
                return NotFound();
            }
            return View(transanction);
        }

        // POST: Transanctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Amount,Description,Date,PerformedBy")] Transanction transanction)
        {
            if (id != transanction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transanction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransanctionExists(transanction.Id))
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
            return View(transanction);
        }

        // GET: Transanctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transanction = await _context.Transanction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transanction == null)
            {
                return NotFound();
            }

            return View(transanction);
        }

        // POST: Transanctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transanction = await _context.Transanction.FindAsync(id);
            if (transanction != null)
            {
                _context.Transanction.Remove(transanction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransanctionExists(int id)
        {
            return _context.Transanction.Any(e => e.Id == id);
        }
    }
}
