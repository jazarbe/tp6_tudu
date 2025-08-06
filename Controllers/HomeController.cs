using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp6_tudu.Models;

namespace tp6_tudu.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.mensaje = null;
        return View();
    }
    
    public IActionResult Tasks(int idSolicitado)
    {
        BD miBd = new BD();
        ViewBag.tareasUsuario = miBd.ConseguirTareasDeUsuario(idSolicitado)
        return View();
    }
    
}
