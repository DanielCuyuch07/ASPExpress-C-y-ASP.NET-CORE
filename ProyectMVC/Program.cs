using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using ProyectMVC.Clases;
using ProyectMVC.Interfaces;
using ProyectMVC.Interfaces.Contrato;
using ProyectMVC.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


// Inicializar cadena de conexion
builder.Services.AddDbContext<DbbancolombiaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaConexion"));
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddScoped<IPersona, PersonServices>();
builder.Services.AddScoped<IUsuarioServices, UsuarioService>();
builder.Services.AddScoped<IDepartamentos, DepartamentoServices>();
builder.Services.AddScoped<IInversiones, InversionesServives>();
builder.Services.AddScoped<IClientes, ClientesServices>();
// Agrega el servicio IHttpContextAccessor
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



// Redirige solicitudes http (Aumentando su seguridad)
app.UseHttpsRedirection();
// Permite proporcionar archivos estaticos (html , css imagenes etc)
app.UseStaticFiles();
// Activa una funcion del enrutamiento en la aplicacion ASP.NET  
// permitiendo que las solicitudes de los usuarios sean dirigidas al código correcto de tu aplicación que sabe cómo manejarlas
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

// Ejecuta la aplicacion 
app.Run();
