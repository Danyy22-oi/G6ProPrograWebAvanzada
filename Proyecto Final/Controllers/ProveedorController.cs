using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _proveedorService.ObtenerTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _proveedorService.ObtenerPorIdAsync(id.Value);
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Contacto")] Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                await _proveedorService.CrearAsync(proveedor);
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _proveedorService.ObtenerPorIdAsync(id.Value);
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Contacto")] Proveedores proveedor)
        {
            if (id != proveedor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _proveedorService.ActualizarAsync(proveedor);
                }
                catch
                {
                    if (!await _proveedorService.ExisteAsync(proveedor.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _proveedorService.ObtenerPorIdAsync(id.Value);
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _proveedorService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
