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
    public class SuministrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuministrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Suministros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suministros.ToListAsync());
        }

        // GET: Suministros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministros = await _context.Suministros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suministros == null)
            {
                return NotFound();
            }

            return View(suministros);
        }

        // GET: Suministros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suministros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,Descripcion")] Suministros suministros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suministros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suministros);
        }

        // GET: Suministros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministros = await _context.Suministros.FindAsync(id);
            if (suministros == null)
            {
                return NotFound();
            }
            return View(suministros);
        }

        // POST: Suministros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Descripcion")] Suministros suministros)
        {
            if (id != suministros.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suministros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuministrosExists(suministros.Id))
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
            return View(suministros);
        }

        // GET: Suministros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministros = await _context.Suministros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suministros == null)
            {
                return NotFound();
            }

            return View(suministros);
        }

        // POST: Suministros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suministros = await _context.Suministros.FindAsync(id);
            if (suministros != null)
            {
                _context.Suministros.Remove(suministros);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuministrosExists(int id)
        {
            return _context.Suministros.Any(e => e.Id == id);
        }
    }
}
