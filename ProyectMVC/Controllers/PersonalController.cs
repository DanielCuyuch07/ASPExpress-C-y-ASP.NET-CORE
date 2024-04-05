using Microsoft.AspNetCore.Mvc;

namespace ProyectMVC.Controllers
{
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
