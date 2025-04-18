using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto.Services.Interfaces;
using Proyecto.Services;
using Proyecto_Final.Data;
using Proyecto_Final.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuraracion de la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Aqui se agrega Identity con roles habilitados
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Esto permite roles
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

//inyeccion de dependencias 
builder.Services.AddScoped<IAlarmaService, AlarmaService>();
builder.Services.AddScoped<ICateterService, CateterService>();
builder.Services.AddScoped<ICorreccionesService, CorreccionesService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IPruebaService, PruebaService>();


//Aqui se configuran las rutas de acceso:
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();


//Se llama a IdentityInitializer.SeedRolesAndAdmin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityInitializer.SeedRolesAndAdmin(services);
}


// Se configura el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Asegurar autenticación
app.UseAuthorization();

//app.MapControllerRoute( 
//name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");




app.MapRazorPages();

app.Run();
