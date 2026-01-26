using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class TutoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tutores
        public async Task<IActionResult> Index()
        {
              return _context.Tutures != null ? 
                          View(await _context.Tutures.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tutures'  is null.");
        }

        // GET: Tutores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tutures == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        // GET: Tutores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tutores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CPF,Telefone")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutor);
        }

        // GET: Tutores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tutures == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutures.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }

        // POST: Tutores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,Telefone")] Tutor tutor)
        {
            if (id != tutor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorExists(tutor.Id))
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
            return View(tutor);
        }

        // GET: Tutores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tutures == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        // POST: Tutores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tutures == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tutures'  is null.");
            }
            var tutor = await _context.Tutures.FindAsync(id);
            if (tutor != null)
            {
                _context.Tutures.Remove(tutor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorExists(int id)
        {
          return (_context.Tutures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
