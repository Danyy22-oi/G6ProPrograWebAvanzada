using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Proyecto_Final.Data;

namespace Proyecto.Controllers
{
    public class CateterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CateterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cateter
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cateter.ToListAsync());
        }

        public async Task<IActionResult> IndexTec()
        {
            return View(await _context.Cateter.ToListAsync());
        }

        // GET: Cateter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateter = await _context.Cateter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cateter == null)
            {
                return NotFound();
            }

            return View(cateter);
        }


        // GET: Cateter/Details/5
        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateter = await _context.Cateter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cateter == null)
            {
                return NotFound();
            }

            return View(cateter);
        }

        // GET: Cateter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cateter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoCateter,Material,Longitud,Diametro,UsoPrevisto")] Cateter cateter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cateter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cateter);
        }

        // GET: Cateter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateter = await _context.Cateter.FindAsync(id);
            if (cateter == null)
            {
                return NotFound();
            }
            return View(cateter);
        }

        // POST: Cateter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoCateter,Material,Longitud,Diametro,UsoPrevisto")] Cateter cateter)
        {
            if (id != cateter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cateter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CateterExists(cateter.Id))
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
            return View(cateter);
        }

        // GET: Cateter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cateter = await _context.Cateter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cateter == null)
            {
                return NotFound();
            }

            return View(cateter);
        }

        // POST: Cateter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cateter = await _context.Cateter.FindAsync(id);
            if (cateter != null)
            {
                _context.Cateter.Remove(cateter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CateterExists(int id)
        {
            return _context.Cateter.Any(e => e.Id == id);
        }
    }
}
