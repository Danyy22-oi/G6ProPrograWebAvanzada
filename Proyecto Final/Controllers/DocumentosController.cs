using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Proyecto_Final.Data;

namespace Proyecto.Controllers
{
    public class DocumentosController : Controller
    {
        private readonly ApplicationDbContext _context;


        public DocumentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Documentos
        [Authorize(Roles = "Administrador,Técnico Calidad,Supervisor,Ingeniería,Mantenimiento")]
        public async Task<IActionResult> Index()
        {
            var pAWContext = _context.Documentos.Include(d => d.Usuario);
            return View(await pAWContext.ToListAsync());
        }

        public async Task<IActionResult> IndexTec()
        {
            var pAWContext = _context.Documentos.Include(d => d.Usuario);
            return View(await pAWContext.ToListAsync());
        }

        public async Task<IActionResult> IndexSup()
        {
            var pAWContext = _context.Documentos.Include(d => d.Usuario);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Documentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentos = await _context.Documentos
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentos == null)
            {
                return NotFound();
            }

            return View(documentos);
        }


        // GET: Documentos/Details/5
        public async Task<IActionResult> DetailsSup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentos = await _context.Documentos
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentos == null)
            {
                return NotFound();
            }

            return View(documentos);
        }

        // GET: Documentos/Details/5
        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentos = await _context.Documentos
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentos == null)
            {
                return NotFound();
            }

            return View(documentos);
        }

        // GET: Documentos/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Documentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Archivo,UsuarioId")] Documentos documentos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", documentos.UsuarioId);
            return View(documentos);
        }

        // GET: Documentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentos = await _context.Documentos.FindAsync(id);
            if (documentos == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", documentos.UsuarioId);
            return View(documentos);
        }

        // POST: Documentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Archivo,UsuarioId")] Documentos documentos)
        {
            if (id != documentos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentosExists(documentos.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", documentos.UsuarioId);
            return View(documentos);
        }


        // GET: Documentos/Edit/5
        public async Task<IActionResult> EditSup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentos = await _context.Documentos.FindAsync(id);
            if (documentos == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", documentos.UsuarioId);
            return View(documentos);
        }

        // POST: Documentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSup(int id, [Bind("Id,Nombre,Descripcion,Archivo,UsuarioId")] Documentos documentos)
        {
            if (id != documentos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentosExists(documentos.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", documentos.UsuarioId);
            return View(documentos);
        }

        // GET: Documentos/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentos = await _context.Documentos
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentos == null)
            {
                return NotFound();
            }

            return View(documentos);
        }

        // POST: Documentos/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentos = await _context.Documentos.FindAsync(id);
            if (documentos != null)
            {
                _context.Documentos.Remove(documentos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentosExists(int id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}
