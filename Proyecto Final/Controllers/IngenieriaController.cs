using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    [Authorize(Roles = "Ingeniería")]
    public class IngenieriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
