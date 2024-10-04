using Microsoft.AspNetCore.Mvc;

namespace web_app_performance.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
