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
    public class AlarmasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlarmasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alarmas
        public async Task<IActionResult> Index()
        {
            var pAWContext = _context.Alarmas.Include(a => a.Pruebas);
            return View(await pAWContext.ToListAsync());
        }

        public async Task<IActionResult> IndexTec()
        {
            var pAWContext = _context.Alarmas.Include(a => a.Pruebas);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Alarmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarmas = await _context.Alarmas
                .Include(a => a.Pruebas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alarmas == null)
            {
                return NotFound();
            }


            return View(alarmas);
        }


        // GET: Alarmas/Details/5
        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarmas = await _context.Alarmas
                .Include(a => a.Pruebas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alarmas == null)
            {
                return NotFound();
            }


            return View(alarmas);
        }




        // GET: Alarmas/Create
        public IActionResult Create()
        {
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id");
            return View();
        }

        // POST: Alarmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Fecha,Estado,Investigacion,PruebaId")] Alarmas alarmas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alarmas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", alarmas.PruebaId);
            return View(alarmas);
        }


        // GET: Alarmas/Create
        public IActionResult CreateTec()
        {
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id");
            return View();
        }

        // POST: Alarmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTec([Bind("Id,Tipo,Fecha,Estado,Investigacion,PruebaId")] Alarmas alarmas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alarmas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", alarmas.PruebaId);
            return View(alarmas);
        }



        // GET: Alarmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarmas = await _context.Alarmas.FindAsync(id);
            if (alarmas == null)
            {
                return NotFound();
            }
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", alarmas.PruebaId);
            return View(alarmas);
        }

        // POST: Alarmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Fecha,Estado,Investigacion,PruebaId")] Alarmas alarmas)
        {
            if (id != alarmas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alarmas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlarmasExists(alarmas.Id))
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
            ViewData["PruebaId"] = new SelectList(_context.Pruebas, "Id", "Id", alarmas.PruebaId);
            return View(alarmas);
        }

        // GET: Alarmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarmas = await _context.Alarmas
                .Include(a => a.Pruebas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alarmas == null)
            {
                return NotFound();
            }

            return View(alarmas);
        }

        // POST: Alarmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alarmas = await _context.Alarmas.FindAsync(id);
            if (alarmas != null)
            {
                _context.Alarmas.Remove(alarmas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlarmasExists(int id)
        {
            return _context.Alarmas.Any(e => e.Id == id);
        }
    }
}
