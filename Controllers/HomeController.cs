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
        int? idUsuarioInSession = HttpContext.Session.GetInt32("usuarioId");
        if (idUsuarioInSession == null)
        {
            return RedirectToAction("Index");
        } else {
            BD miBd = new BD();
            ViewBag.tareasUsuario = miBd.ConseguirTareasDeUsuario(idSolicitado);
            return View();
        }
    }
    
    public IActionResult CrearTarea(string titulo, string descripcion, DateTime fecha){
        BD miBd = new BD();
        bool finalizada = false;
        int? idUsuarioInSession = HttpContext.Session.GetInt32("usuarioId");

        miBd.AgregarTarea(titulo, descripcion, fecha, finalizada, idUsuarioInSession.Value);

        return View("Tasks");
    }

    public IActionResult EditarTarea(Tarea tarea, string titulo, string descripcion, DateTime fecha, bool finalizada){
        BD miBd = new BD();
        miBd.UpdateTarea(tarea, titulo, descripcion, fecha, finalizada);

        return View("Tasks");
    }

    public IActionResult EliminarTarea(Tarea tarea){
        BD miBd = new BD();
        miBd.DeleteTarea(tarea);

        return View("Tasks");
    }

    public IActionResult FinalizarTarea(int id){
        BD miBd = new BD();
        miBd.FinalizarTarea(id);

        return View("Tasks");
    }
}
