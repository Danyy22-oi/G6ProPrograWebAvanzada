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
    public class MaterialesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Materiales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materiales.ToListAsync());
        }

        // GET: Materiales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiales = await _context.Materiales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiales == null)
            {
                return NotFound();
            }

            return View(materiales);
        }

        // GET: Materiales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materiales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,Descripcion")] Materiales materiales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materiales);
        }

        // GET: Materiales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiales = await _context.Materiales.FindAsync(id);
            if (materiales == null)
            {
                return NotFound();
            }
            return View(materiales);
        }

        // POST: Materiales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Descripcion")] Materiales materiales)
        {
            if (id != materiales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialesExists(materiales.Id))
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
            return View(materiales);
        }

        // GET: Materiales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiales = await _context.Materiales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiales == null)
            {
                return NotFound();
            }

            return View(materiales);
        }

        // POST: Materiales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiales = await _context.Materiales.FindAsync(id);
            if (materiales != null)
            {
                _context.Materiales.Remove(materiales);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialesExists(int id)
        {
            return _context.Materiales.Any(e => e.Id == id);
        }
    }
}
