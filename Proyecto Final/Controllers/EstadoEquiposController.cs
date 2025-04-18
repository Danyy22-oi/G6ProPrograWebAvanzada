using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class EstadoEquiposController : Controller
    {
        private readonly IEstadoEquiposService _estadoEquiposService;

        public EstadoEquiposController(IEstadoEquiposService estadoEquiposService)
        {
            _estadoEquiposService = estadoEquiposService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _estadoEquiposService.ObtenerTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var estado = await _estadoEquiposService.ObtenerPorIdAsync(id.Value);
            if (estado == null) return NotFound();

            return View(estado);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado")] EstadoEquipos estadoEquipos)
        {
            if (ModelState.IsValid)
            {
                await _estadoEquiposService.CrearAsync(estadoEquipos);
                return RedirectToAction(nameof(Index));
            }
            return View(estadoEquipos);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var estado = await _estadoEquiposService.ObtenerPorIdAsync(id.Value);
            if (estado == null) return NotFound();

            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado")] EstadoEquipos estadoEquipos)
        {
            if (id != estadoEquipos.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _estadoEquiposService.ActualizarAsync(estadoEquipos);
                }
                catch
                {
                    if (!await _estadoEquiposService.ExisteAsync(estadoEquipos.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estadoEquipos);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var estado = await _estadoEquiposService.ObtenerPorIdAsync(id.Value);
            if (estado == null) return NotFound();

            return View(estado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _estadoEquiposService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
