using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
