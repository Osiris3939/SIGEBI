using Microsoft.AspNetCore.Mvc;

namespace SIGEBI.Web.Controllers
{
    // Controlador principal para la pagina de inicio / dashboard
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
