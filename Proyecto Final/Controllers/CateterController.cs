using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class CateterController : Controller
    {
        private readonly ICateterService _cateterService;

        public CateterController(ICateterService cateterService)
        {
            _cateterService = cateterService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _cateterService.ObtenerTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> IndexTec()
        {
            var lista = await _cateterService.ObtenerTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cateter = await _cateterService.ObtenerPorIdAsync(id.Value);
            if (cateter == null) return NotFound();

            return View(cateter);
        }

        public async Task<IActionResult> DetailsTec(int? id)
        {
            if (id == null) return NotFound();

            var cateter = await _cateterService.ObtenerPorIdAsync(id.Value);
            if (cateter == null) return NotFound();

            return View(cateter);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoCateter,Material,Longitud,Diametro,UsoPrevisto")] Cateter cateter)
        {
            if (ModelState.IsValid)
            {
                await _cateterService.CrearAsync(cateter);
                return RedirectToAction(nameof(Index));
            }
            return View(cateter);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cateter = await _cateterService.ObtenerPorIdAsync(id.Value);
            if (cateter == null) return NotFound();

            return View(cateter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoCateter,Material,Longitud,Diametro,UsoPrevisto")] Cateter cateter)
        {
            if (id != cateter.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _cateterService.ActualizarAsync(cateter);
                }
                catch
                {
                    if (!await _cateterService.ExisteAsync(cateter.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cateter);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cateter = await _cateterService.ObtenerPorIdAsync(id.Value);
            if (cateter == null) return NotFound();

            return View(cateter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cateterService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
