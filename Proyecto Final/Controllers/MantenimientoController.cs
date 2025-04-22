using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    [Authorize(Roles = "Mantenimiento")]
    public class MantenimientoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
