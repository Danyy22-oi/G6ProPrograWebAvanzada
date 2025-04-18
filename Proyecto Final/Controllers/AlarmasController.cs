using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class AlarmasController : Controller
    {
        private readonly IAlarmaService _alarmaService;
        private readonly IPruebaService _pruebaService;

        public AlarmasController(IAlarmaService alarmaService, IPruebaService pruebaService)
        {
            _alarmaService = alarmaService;
            _pruebaService = pruebaService;
        }

        public async Task<IActionResult> Index()
        {
            var alarmas = await _alarmaService.ObtenerTodasAsync();
            return View(alarmas);
        }

        public async Task<IActionResult> IndexTec()
        {
            var alarmas = await _alarmaService.ObtenerTodasAsync();
            return View(alarmas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var alarma = await _alarmaService.ObtenerPorIdAsync(id.Value);
            if (alarma == null) return NotFound();

            return View(alarma);
        }

        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null) return NotFound();

            var alarma = await _alarmaService.ObtenerPorIdAsync(id.Value);
            if (alarma == null) return NotFound();

            return View(alarma);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Fecha,Estado,Investigacion,PruebaId")] Alarmas alarmas)
        {
            if (ModelState.IsValid)
            {
                await _alarmaService.CrearAsync(alarmas);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", alarmas.PruebaId);
            return View(alarmas);
        }

        public async Task<IActionResult> CreateTec()
        {
            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTec([Bind("Id,Tipo,Fecha,Estado,Investigacion,PruebaId")] Alarmas alarmas)
        {
            if (ModelState.IsValid)
            {
                await _alarmaService.CrearAsync(alarmas);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", alarmas.PruebaId);
            return View(alarmas);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var alarma = await _alarmaService.ObtenerPorIdAsync(id.Value);
            if (alarma == null) return NotFound();

            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", alarma.PruebaId);
            return View(alarma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Fecha,Estado,Investigacion,PruebaId")] Alarmas alarmas)
        {
            if (id != alarmas.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _alarmaService.ActualizarAsync(alarmas);
                }
                catch
                {
                    if (!_alarmaService.Existe(alarmas.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PruebaId"] = new SelectList(await _pruebaService.ObtenerTodasAsync(), "Id", "Id", alarmas.PruebaId);
            return View(alarmas);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var alarma = await _alarmaService.ObtenerPorIdAsync(id.Value);
            if (alarma == null) return NotFound();

            return View(alarma);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _alarmaService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
