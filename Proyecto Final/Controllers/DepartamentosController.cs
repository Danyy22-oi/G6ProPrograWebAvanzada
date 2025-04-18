using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentosController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var departamentos = await _departamentoService.ObtenerTodosAsync();
            return View(departamentos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var departamento = await _departamentoService.ObtenerPorIdAsync(id.Value);
            if (departamento == null) return NotFound();

            return View(departamento);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Supervisor,Area")] Departamentos departamento)
        {
            if (ModelState.IsValid)
            {
                await _departamentoService.CrearAsync(departamento);
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var departamento = await _departamentoService.ObtenerPorIdAsync(id.Value);
            if (departamento == null) return NotFound();

            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Supervisor,Area")] Departamentos departamento)
        {
            if (id != departamento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _departamentoService.ActualizarAsync(departamento);
                }
                catch
                {
                    if (!await _departamentoService.ExisteAsync(departamento.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(departamento);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var departamento = await _departamentoService.ObtenerPorIdAsync(id.Value);
            if (departamento == null) return NotFound();

            return View(departamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departamentoService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
