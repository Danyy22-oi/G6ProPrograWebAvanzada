using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Proyecto_Final.Models;

namespace Proyecto_Final.Data
{
    public static class IdentityInitializer
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {



            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Aquí se verifica si el departamento con ID 1 existe, si no, crearlo
            if (!dbContext.Departamentos.Any(d => d.Id == 1))
            {
                dbContext.Departamentos.Add(new Departamentos
                {
                    Nombre = "Test Nombre",
                    Supervisor = "Test Supervisor",
                    Area = "Test Area"
                    // Agrega aquí otros campos requeridos
                });
                await dbContext.SaveChangesAsync();
            }



            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Técnico Calidad", "Supervisor", "Ingeniería", "Mantenimiento", "Administrador" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Crear el usuario administrador por default
            await CreateUserIfNotExists(userManager, "admin@empresa.com", "Admin123*", "Administrador", 1); // Asignar departamento 1

            // Crear un usuario por cada rol (excepto Administrador)
            await CreateUserIfNotExists(userManager, "tecnico@empresa.com", "Password123*", "Técnico Calidad", 1);
            await CreateUserIfNotExists(userManager, "supervisor@empresa.com", "Password123*", "Supervisor", 1);
            await CreateUserIfNotExists(userManager, "ingenieria@empresa.com", "Password123*", "Ingeniería", 1);
            await CreateUserIfNotExists(userManager, "mantenimiento@empresa.com", "Password123*", "Mantenimiento", 1);
        }

        private static async Task CreateUserIfNotExists(UserManager<ApplicationUser> userManager, string email, string password, string role, int departamentoId)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    DepartamentoId = departamentoId
                };
                var createUser = await userManager.CreateAsync(user, password);
                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}