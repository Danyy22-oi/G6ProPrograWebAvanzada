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
    public class CorreccionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CorreccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Correcciones
        public async Task<IActionResult> Index()
        {
            var pAWContext = _context.Correcciones.Include(c => c.Pruebas);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Correcciones
        public async Task<IActionResult> IndexTec()
        {
            var pAWContext = _context.Correcciones.Include(c => c.Pruebas);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Correcciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correcciones = await _context.Correcciones
                .Include(c => c.Pruebas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correcciones == null)
            {
                return NotFound();
            }

            return View(correcciones);
        }

        // GET: Correcciones/Details/5
        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correcciones = await _context.Correcciones
                .Include(c => c.Pruebas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correcciones == null)
            {
                return NotFound();
            }

            return View(correcciones);
        }

        // GET: Correcciones/Create
        public IActionResult Create()
        {
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id");
            return View();
        }

        // POST: Correcciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Fecha,PruebaId")] Correcciones correcciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correcciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", correcciones.PruebaId);
            return View(correcciones);
        }


        // GET: Correcciones/Create
        public IActionResult CreateTec()
        {
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id");
            return View();
        }

        // POST: Correcciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTec([Bind("Id,Descripcion,Fecha,PruebaId")] Correcciones correcciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correcciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", correcciones.PruebaId);
            return View(correcciones);
        }


        // GET: Correcciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correcciones = await _context.Correcciones.FindAsync(id);
            if (correcciones == null)
            {
                return NotFound();
            }
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", correcciones.PruebaId);
            return View(correcciones);
        }



        // POST: Correcciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Fecha,PruebaId")] Correcciones correcciones)
        {
            if (id != correcciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correcciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorreccionesExists(correcciones.Id))
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
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", correcciones.PruebaId);
            return View(correcciones);
        }

        // GET: Correcciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correcciones = await _context.Correcciones
                .Include(c => c.Pruebas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correcciones == null)
            {
                return NotFound();
            }

            return View(correcciones);
        }

        // POST: Correcciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var correcciones = await _context.Correcciones.FindAsync(id);
            if (correcciones != null)
            {
                _context.Correcciones.Remove(correcciones);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorreccionesExists(int id)
        {
            return _context.Correcciones.Any(e => e.Id == id);
        }
    }
}
