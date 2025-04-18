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
    public class EstadoEquiposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadoEquiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstadoEquipos
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoEquipos.ToListAsync());
        }

        // GET: EstadoEquipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoEquipos = await _context.EstadoEquipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoEquipos == null)
            {
                return NotFound();
            }

            return View(estadoEquipos);
        }

        // GET: EstadoEquipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoEquipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado")] EstadoEquipos estadoEquipos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoEquipos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoEquipos);
        }

        // GET: EstadoEquipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoEquipos = await _context.EstadoEquipos.FindAsync(id);
            if (estadoEquipos == null)
            {
                return NotFound();
            }
            return View(estadoEquipos);
        }

        // POST: EstadoEquipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado")] EstadoEquipos estadoEquipos)
        {
            if (id != estadoEquipos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoEquipos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoEquiposExists(estadoEquipos.Id))
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
            return View(estadoEquipos);
        }

        // GET: EstadoEquipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoEquipos = await _context.EstadoEquipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoEquipos == null)
            {
                return NotFound();
            }

            return View(estadoEquipos);
        }

        // POST: EstadoEquipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoEquipos = await _context.EstadoEquipos.FindAsync(id);
            if (estadoEquipos != null)
            {
                _context.EstadoEquipos.Remove(estadoEquipos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoEquiposExists(int id)
        {
            return _context.EstadoEquipos.Any(e => e.Id == id);
        }
    }
}
