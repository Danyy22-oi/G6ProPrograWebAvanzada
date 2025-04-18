using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly IMaterialesService _materialesService;

        public MaterialesController(IMaterialesService materialesService)
        {
            _materialesService = materialesService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _materialesService.ObtenerTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var material = await _materialesService.ObtenerPorIdAsync(id.Value);
            if (material == null) return NotFound();

            return View(material);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,Descripcion")] Materiales material)
        {
            if (ModelState.IsValid)
            {
                await _materialesService.CrearAsync(material);
                return RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var material = await _materialesService.ObtenerPorIdAsync(id.Value);
            if (material == null) return NotFound();

            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Descripcion")] Materiales material)
        {
            if (id != material.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _materialesService.ActualizarAsync(material);
                }
                catch
                {
                    if (!await _materialesService.ExisteAsync(material.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var material = await _materialesService.ObtenerPorIdAsync(id.Value);
            if (material == null) return NotFound();

            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _materialesService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
