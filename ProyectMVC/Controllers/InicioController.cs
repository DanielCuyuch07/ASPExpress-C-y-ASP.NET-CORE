using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectMVC.Interfaces.Contrato;
using ProyectMVC.Models;
using ProyectMVC.Recursos;
using System.Security.Claims;

namespace ProyectMVC.Controllers
{
    public class InicioController : Controller
    {

        private readonly IUsuarioServices _usuarioServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InicioController(IUsuarioServices usuarioServices, IWebHostEnvironment webHostEnvironment)
        {
            _usuarioServices = usuarioServices;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Clave = Utilidades.EncriptarClave(modelo.Clave);

            Usuario usuario_creado = await _usuarioServices.SaveUsuario(modelo);

            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";

            // Agregar el atributo id al formulario
            return View("NombreDeTuVista", modelo); // Reemplaza "NombreDeTuVista" con el nombre real de tu vista
        }



        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {

            Usuario usuario_encontrado = await _usuarioServices.GetUsuario(correo, Utilidades.EncriptarClave(clave));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }


    }
}
