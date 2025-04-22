using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Acción para mostrar la vista de inicio de sesión
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el inicio de sesión
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("RedirectByRole", "Account");
                }
                ModelState.AddModelError(string.Empty, "Inicio de sesión inválido.");
            }
            return View(model);
        }

        // Acción para redirigir al usuario según su rol
        public async Task<IActionResult> RedirectByRole()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Administrador"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (roles.Contains("Técnico Calidad"))
                {
                    return RedirectToAction("Index", "TecnicoCalidad");
                }
                else if (roles.Contains("Supervisor"))
                {
                    return RedirectToAction("Index", "Supervisor");
                }
                else if (roles.Contains("Ingeniería"))
                {
                    return RedirectToAction("Index", "Ingenieria");
                }
                else if (roles.Contains("Mantenimiento"))
                {
                    return RedirectToAction("Index", "Mantenimiento");
                }
            }
            return RedirectToAction("Index", "Home"); // Si no tiene roles, redirigir al inicio
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }


        // Acción para cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account"); // Redirigir al login después de cerrar sesión
        }
    }
}