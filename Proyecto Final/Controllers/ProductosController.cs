using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
