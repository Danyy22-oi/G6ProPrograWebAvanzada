using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    [Authorize(Roles = "Técnico Calidad")]
    public class TecnicoCalidadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
