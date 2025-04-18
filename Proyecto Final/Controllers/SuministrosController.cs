using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class SuministrosController : Controller
    {
        private readonly ISuministrosService _suministrosService;

        public SuministrosController(ISuministrosService suministrosService)
        {
            _suministrosService = suministrosService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _suministrosService.ObtenerTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var suministro = await _suministrosService.ObtenerPorIdAsync(id.Value);
            if (suministro == null) return NotFound();

            return View(suministro);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,Descripcion")] Suministros suministro)
        {
            if (ModelState.IsValid)
            {
                await _suministrosService.CrearAsync(suministro);
                return RedirectToAction(nameof(Index));
            }
            return View(suministro);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var suministro = await _suministrosService.ObtenerPorIdAsync(id.Value);
            if (suministro == null) return NotFound();

            return View(suministro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Descripcion")] Suministros suministro)
        {
            if (id != suministro.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _suministrosService.ActualizarAsync(suministro);
                }
                catch
                {
                    if (!await _suministrosService.ExisteAsync(suministro.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(suministro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var suministro = await _suministrosService.ObtenerPorIdAsync(id.Value);
            if (suministro == null) return NotFound();

            return View(suministro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _suministrosService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
