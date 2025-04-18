using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class EquiposController : Controller
    {
        private readonly IEquiposService _equiposService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IEstadoEquiposService _estadoEquiposService;

        public EquiposController(
            IEquiposService equiposService,
            IDepartamentoService departamentoService,
            IEstadoEquiposService estadoEquiposService)
        {
            _equiposService = equiposService;
            _departamentoService = departamentoService;
            _estadoEquiposService = estadoEquiposService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _equiposService.ObtenerTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var equipo = await _equiposService.ObtenerPorIdAsync(id.Value);
            if (equipo == null) return NotFound();

            return View(equipo);
        }

        public async Task<IActionResult> Create()
        {
            await CargarCombosAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,FechaMantenimientoPreventivo,FechaCalibracion,Cantidad,Ubicacion,DepartamentoId,EstadoEquipoId")] Equipos equipo)
        {
            if (ModelState.IsValid)
            {
                await _equiposService.CrearAsync(equipo);
                return RedirectToAction(nameof(Index));
            }
            await CargarCombosAsync(equipo.DepartamentoId, equipo.EstadoEquipoId);
            return View(equipo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var equipo = await _equiposService.ObtenerPorIdSimpleAsync(id.Value);
            if (equipo == null) return NotFound();

            await CargarCombosAsync(equipo.DepartamentoId, equipo.EstadoEquipoId);
            return View(equipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaMantenimientoPreventivo,FechaCalibracion,Cantidad,Ubicacion,DepartamentoId,EstadoEquipoId")] Equipos equipo)
        {
            if (id != equipo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _equiposService.ActualizarAsync(equipo);
                }
                catch
                {
                    if (!await _equiposService.ExisteAsync(equipo.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await CargarCombosAsync(equipo.DepartamentoId, equipo.EstadoEquipoId);
            return View(equipo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var equipo = await _equiposService.ObtenerPorIdAsync(id.Value);
            if (equipo == null) return NotFound();

            return View(equipo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _equiposService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarCombosAsync(int? departamentoId = null, int? estadoId = null)
        {
            ViewData["DepartamentoId"] = new SelectList(await _departamentoService.ObtenerTodosAsync(), "Id", "Id", departamentoId);
            ViewData["EstadoEquipoId"] = new SelectList(await _estadoEquiposService.ObtenerTodosAsync(), "Id", "Id", estadoId);
        }
    }
}
