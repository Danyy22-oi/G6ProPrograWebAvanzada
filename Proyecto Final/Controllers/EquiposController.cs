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
    public class EquiposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipos
        public async Task<IActionResult> Index()
        {
            var pAWContext = _context.Equipos.Include(e => e.Departamentos).Include(e => e.EstadoEquipos);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Equipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipos = await _context.Equipos
                .Include(e => e.Departamentos)
                .Include(e => e.EstadoEquipos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipos == null)
            {
                return NotFound();
            }

            return View(equipos);
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Id");
            ViewData["EstadoEquipoId"] = new SelectList(_context.EstadoEquipos, "Id", "Id");
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,FechaMantenimientoPreventivo,FechaCalibracion,Cantidad,Ubicacion,DepartamentoId,EstadoEquipoId")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Id", equipos.DepartamentoId);
            ViewData["EstadoEquipoId"] = new SelectList(_context.EstadoEquipos, "Id", "Id", equipos.EstadoEquipoId);
            return View(equipos);
        }

        // GET: Equipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipos = await _context.Equipos.FindAsync(id);
            if (equipos == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Id", equipos.DepartamentoId);
            ViewData["EstadoEquipoId"] = new SelectList(_context.EstadoEquipos, "Id", "Id", equipos.EstadoEquipoId);
            return View(equipos);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaMantenimientoPreventivo,FechaCalibracion,Cantidad,Ubicacion,DepartamentoId,EstadoEquipoId")] Equipos equipos)
        {
            if (id != equipos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquiposExists(equipos.Id))
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
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Id", equipos.DepartamentoId);
            ViewData["EstadoEquipoId"] = new SelectList(_context.EstadoEquipos, "Id", "Id", equipos.EstadoEquipoId);
            return View(equipos);
        }

        // GET: Equipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipos = await _context.Equipos
                .Include(e => e.Departamentos)
                .Include(e => e.EstadoEquipos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipos == null)
            {
                return NotFound();
            }

            return View(equipos);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipos = await _context.Equipos.FindAsync(id);
            if (equipos != null)
            {
                _context.Equipos.Remove(equipos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquiposExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }
    }
}
