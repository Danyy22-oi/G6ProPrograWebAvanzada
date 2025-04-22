using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Services;
using Proyecto.Services.Interfaces;
using Proyecto_Final.Data;
using Proyecto_Final.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor del AdminController
        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Método para la página de inicio del administrador
        public IActionResult Index()
        {
            return View();
        }

        // Método para visualizar y gestionar los usuarios

        public async Task<IActionResult> CRUDusers()
        {
            var usersWithRoles = await _context.Users
                .Include(u => u.Departamentos) // Incluye los departamentos
                .Select(user => new UserRoleViewModel
                {
                    Id = user.Id,  // Asignar el Id del usuario
                    UserName = user.UserName,
                    Email = user.Email,
                    Departamento = user.Departamentos != null ? user.Departamentos.Nombre : "Sin Departamento",
                    RoleName = (from userRole in _context.UserRoles
                                join role in _context.Roles on userRole.RoleId equals role.Id
                                where userRole.UserId == user.Id
                                select role.Name).FirstOrDefault()
                })
                .ToListAsync();

            return View(usersWithRoles);
        }


        // Método para mostrar el formulario de edición de un usuario
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar el usuario por su Id
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Obtener los roles para mostrar en el dropdown
            var roles = await _userManager.GetRolesAsync(user);

            // Crear el modelo con los datos del usuario
            var model = new UserRoleEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Departamentos = user.Departamentos != null ? user.Departamentos.Nombre : "Sin Departamento",
                RoleName = roles.FirstOrDefault() // Puede ser más de un rol, pero vamos a mostrar el primero
            };

            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                // Actualizar los datos básicos del usuario
                user.UserName = model.UserName;
                user.Email = model.Email;

                // Guardar los cambios en los datos básicos
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return RedirectToAction("CRUDusers");
                }

                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }



        // Método para eliminar un usuario
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            // Buscar al usuario por su Id
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Eliminar el usuario
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // Redirigir a la vista con el listado actualizado de usuarios
                return RedirectToAction("CRUDusers");
            }

            // En caso de error, mostrar mensaje
            TempData["ErrorMessage"] = "No se pudo eliminar el usuario.";
            return RedirectToAction("CRUDusers");
        }

        // Método para mostrar los reportes
        public IActionResult Reportes()
        {
            return View();
        }

        // Método para generar el reporte
        [HttpPost]
        public IActionResult GenerarReporte()
        {
            try
            {
                QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

                var rutaJson = @"C:\Users\El Doritos PC\Documents\ReportesPAW\cateteres.json";

                if (!System.IO.File.Exists(rutaJson))
                    return NotFound("No se encontró el archivo de catéteres.");

                var jsonData = System.IO.File.ReadAllText(rutaJson);
                var cateteres = JsonSerializer.Deserialize<List<CateterModel>>(jsonData);

                if (cateteres == null || !cateteres.Any())
                    return BadRequest("No hay datos para generar el reporte.");

                var fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var nombreArchivo = $"ReporteCateteres_{fechaHora}.pdf";

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(20);
                        page.Header()
                            .Text("Reporte de Catéteres")
                            .FontSize(20)
                            .SemiBold()
                            .AlignCenter();

                        page.Content()
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Nombre");
                                    header.Cell().Element(CellStyle).Text("Tipo");
                                    header.Cell().Element(CellStyle).Text("Tamaño");
                                    header.Cell().Element(CellStyle).Text("Material");
                                    header.Cell().Element(CellStyle).Text("Fabricante");

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5);
                                    }
                                });

                                foreach (var c in cateteres)
                                {
                                    table.Cell().Text(c?.Nombre ?? "N/A");
                                    table.Cell().Text(c?.Tipo ?? "N/A");
                                    table.Cell().Text(c?.Tamaño ?? "N/A");
                                    table.Cell().Text(c?.Material ?? "N/A");
                                    table.Cell().Text(c?.Fabricante ?? "N/A");
                                }
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("Generado: ");
                                x.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}");
                            });
                    });
                });

                var pdfStream = new MemoryStream();
                document.GeneratePdf(pdfStream);
                pdfStream.Position = 0;

                return File(pdfStream, "application/pdf", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
