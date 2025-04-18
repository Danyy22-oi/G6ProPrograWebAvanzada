using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class PruebasController : Controller
    {
        private readonly IPruebaService _pruebaService;
        private readonly ICateterService _cateterService;

        public PruebasController(IPruebaService pruebaService, ICateterService cateterService)
        {
            _pruebaService = pruebaService;
            _cateterService = cateterService;
        }

        public async Task<IActionResult> Index()
        {
            var pruebas = await _pruebaService.ObtenerTodasAsync();
            return View(pruebas);
        }

        public async Task<IActionResult> IndexTec()
        {
            var pruebas = await _pruebaService.ObtenerTodasAsync();
            return View(pruebas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var prueba = await _pruebaService.ObtenerPorIdAsync(id.Value);
            if (prueba == null) return NotFound();

            return View(prueba);
        }

        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null) return NotFound();

            var prueba = await _pruebaService.ObtenerPorIdAsync(id.Value);
            if (prueba == null) return NotFound();

            return View(prueba);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas prueba)
        {
            if (ModelState.IsValid)
            {
                await _pruebaService.CrearAsync(prueba);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id", prueba.CateterId);
            return View(prueba);
        }

        public async Task<IActionResult> CreateTec()
        {
            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTec([Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas prueba)
        {
            if (ModelState.IsValid)
            {
                await _pruebaService.CrearAsync(prueba);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id", prueba.CateterId);
            return View(prueba);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var prueba = await _pruebaService.ObtenerPorIdSimpleAsync(id.Value);
            if (prueba == null) return NotFound();

            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id", prueba.CateterId);
            return View(prueba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas prueba)
        {
            if (id != prueba.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _pruebaService.ActualizarAsync(prueba);
                }
                catch
                {
                    if (!await _pruebaService.ExisteAsync(prueba.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id", prueba.CateterId);
            return View(prueba);
        }

        public async Task<IActionResult> EditTec(int? id)
        {
            if (id == null) return NotFound();

            var prueba = await _pruebaService.ObtenerPorIdSimpleAsync(id.Value);
            if (prueba == null) return NotFound();

            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id", prueba.CateterId);
            return View(prueba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTec(int id, [Bind("Id,Descripcion,Fecha,PresionBalloon,InspeccionTactil,RupturaBalloon,Desinflado,Estado,CateterId")] Pruebas prueba)
        {
            if (id != prueba.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _pruebaService.ActualizarAsync(prueba);
                }
                catch
                {
                    if (!await _pruebaService.ExisteAsync(prueba.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CateterId"] = new SelectList(await _cateterService.ObtenerTodosAsync(), "Id", "Id", prueba.CateterId);
            return View(prueba);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var prueba = await _pruebaService.ObtenerPorIdAsync(id.Value);
            if (prueba == null) return NotFound();

            return View(prueba);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pruebaService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
