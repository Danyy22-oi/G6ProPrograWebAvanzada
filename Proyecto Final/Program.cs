//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Proyecto.Services.Interfaces;
//using Proyecto.Services;
//using Proyecto_Final.Data;
//using Proyecto_Final.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Configuraracion de la cadena de conexi�n
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//// Aqui se agrega Identity con roles habilitados
//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddRoles<IdentityRole>() // Esto permite roles
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddControllersWithViews();

////inyeccion de dependencias 
//builder.Services.AddScoped<IAlarmaService, AlarmaService>();
//builder.Services.AddScoped<ICateterService, CateterService>();
//builder.Services.AddScoped<ICorreccionesService, CorreccionesService>();
//builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
//builder.Services.AddScoped<IPruebaService, PruebaService>();
//builder.Services.AddScoped<ISuministrosService, SuministrosService>();
//builder.Services.AddScoped<IProveedorService, ProveedorService>();
//builder.Services.AddScoped<IMaterialesService, MaterialesService>();
//builder.Services.AddScoped<IEstadoEquiposService, EstadoEquiposService>();
//builder.Services.AddScoped<IEstadoEquiposService, EstadoEquiposService>();
//builder.Services.AddScoped<IEquiposService, EquiposService>();



////Aqui se configuran las rutas de acceso:
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//});

//var app = builder.Build();


////Se llama a IdentityInitializer.SeedRolesAndAdmin
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await IdentityInitializer.SeedRolesAndAdmin(services);
//}


//// Se configura el pipeline de la aplicaci�n
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthentication(); // Asegurar autenticaci�n
//app.UseAuthorization();

////app.MapControllerRoute( 
////name: "default",
////pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Account}/{action=Login}/{id?}");




//app.MapRazorPages();

//app.Run();


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto.Services.Interfaces;
using Proyecto.Services;
using Proyecto_Final.Data;
using Proyecto_Final.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔵 Configuración de cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 🔵 Configuración de Identity con Roles
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


// 🔵 Inyección de dependencias (servicios)
builder.Services.AddScoped<IAlarmaService, AlarmaService>();
builder.Services.AddScoped<ICateterService, CateterService>();
builder.Services.AddScoped<ICorreccionesService, CorreccionesService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IPruebaService, PruebaService>();
builder.Services.AddScoped<ISuministrosService, SuministrosService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IMaterialesService, MaterialesService>();
builder.Services.AddScoped<IEstadoEquiposService, EstadoEquiposService>();
builder.Services.AddScoped<IEquiposService, EquiposService>();

// 🔵 Configuración de Cookies (Login/AccessDenied)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// 🔵 Agregamos controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔵 Inicializar Roles y Usuario Admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityInitializer.SeedRolesAndAdmin(services);
}

// 🔵 Configuración del Pipeline
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

app.UseAuthentication();
app.UseAuthorization();

// 🔵 Configuración de Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 🔵 Rutas para Razor Pages (Register, Login, etc.)
app.MapRazorPages();

app.Run();
