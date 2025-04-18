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
    public class PruebasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PruebasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pruebas
        public async Task<IActionResult> Index()
        {
            var pAWContext = _context.Pruebas.Include(p => p.Cateter);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Pruebas
        public async Task<IActionResult> IndexTec()
        {
            var pAWContext = _context.Pruebas.Include(p => p.Cateter);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Pruebas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebas = await _context.Pruebas
                .Include(p => p.Cateter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pruebas == null)
            {
                return NotFound();
            }

            return View(pruebas);
        }

        // GET: Pruebas/Details/5
        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebas = await _context.Pruebas
                .Include(p => p.Cateter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pruebas == null)
            {
                return NotFound();
            }

            return View(pruebas);
        }

        // GET: Pruebas/Create
        public IActionResult Create()
        {
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id");
            return View();
        }

        // POST: Pruebas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas pruebas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pruebas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id", pruebas.CateterId);
            return View(pruebas);
        }


        // GET: Pruebas/Create
        public IActionResult CreateTec()
        {
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id");
            return View();
        }

        // POST: Pruebas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTec([Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas pruebas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pruebas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id", pruebas.CateterId);
            return View(pruebas);
        }

        // GET: Pruebas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebas = await _context.Pruebas.FindAsync(id);
            if (pruebas == null)
            {
                return NotFound();
            }
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id", pruebas.CateterId);
            return View(pruebas);
        }

        // POST: Pruebas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas pruebas)
        {
            if (id != pruebas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pruebas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PruebasExists(pruebas.Id))
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
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id", pruebas.CateterId);
            return View(pruebas);
        }


        // GET: Pruebas/Edit/5
        public async Task<IActionResult> EditTec(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebas = await _context.Pruebas.FindAsync(id);
            if (pruebas == null)
            {
                return NotFound();
            }
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id", pruebas.CateterId);
            return View(pruebas);
        }

        // POST: Pruebas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTec(int id, [Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas pruebas)
        {
            if (id != pruebas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pruebas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PruebasExists(pruebas.Id))
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
            ViewData["CateterId"] = new SelectList(_context.Cateter, "Id", "Id", pruebas.CateterId);
            return View(pruebas);
        }



        // GET: Pruebas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebas = await _context.Pruebas
                .Include(p => p.Cateter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pruebas == null)
            {
                return NotFound();
            }

            return View(pruebas);
        }

        // POST: Pruebas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pruebas = await _context.Pruebas.FindAsync(id);
            if (pruebas != null)
            {
                _context.Pruebas.Remove(pruebas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PruebasExists(int id)
        {
            return _context.Pruebas.Any(e => e.Id == id);
        }
    }
}
