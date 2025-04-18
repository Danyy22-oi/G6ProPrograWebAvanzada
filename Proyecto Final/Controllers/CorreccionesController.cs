using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class CorreccionesController : Controller
    {
        private readonly ICorreccionesService _correccionesService;
        private readonly IPruebaService _pruebaService;

        public CorreccionesController(ICorreccionesService correccionesService, IPruebaService pruebaService)
        {
            _correccionesService = correccionesService;
            _pruebaService = pruebaService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _correccionesService.ObtenerTodasAsync();
            return View(lista);
        }

        public async Task<IActionResult> IndexTec()
        {
            var lista = await _correccionesService.ObtenerTodasAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var correccion = await _correccionesService.ObtenerPorIdAsync(id.Value);
            if (correccion == null) return NotFound();

            return View(correccion);
        }

        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null) return NotFound();

            var correccion = await _correccionesService.ObtenerPorIdAsync(id.Value);
            if (correccion == null) return NotFound();

            return View(correccion);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Fecha,PruebaId")] Correcciones correccion)
        {
            if (ModelState.IsValid)
            {
                await _correccionesService.CrearAsync(correccion);
                return RedirectToAction(nameof(Index));
            }

            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", correccion.PruebaId);
            return View(correccion);
        }

        public async Task<IActionResult> CreateTec()
        {
            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTec([Bind("Id,Descripcion,Fecha,PruebaId")] Correcciones correccion)
        {
            if (ModelState.IsValid)
            {
                await _correccionesService.CrearAsync(correccion);
                return RedirectToAction(nameof(Index));
            }

            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", correccion.PruebaId);
            return View(correccion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var correccion = await _correccionesService.ObtenerPorIdAsync(id.Value);
            if (correccion == null) return NotFound();

            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", correccion.PruebaId);
            return View(correccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Fecha,PruebaId")] Correcciones correccion)
        {
            if (id != correccion.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _correccionesService.ActualizarAsync(correccion);
                }
                catch
                {
                    if (!await _correccionesService.ExisteAsync(correccion.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", correccion.PruebaId);
            return View(correccion);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var correccion = await _correccionesService.ObtenerPorIdAsync(id.Value);
            if (correccion == null) return NotFound();

            return View(correccion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _correccionesService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
