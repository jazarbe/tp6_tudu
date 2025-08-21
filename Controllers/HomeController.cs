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

    public IActionResult Index(string mensaje)
    {
        ViewBag.mensaje = mensaje;
        HttpContext.Session.Remove("usuarioId");
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
    public IActionResult CrearTarea(){
        return View();
    }
    public IActionResult SendTarea(string titulo, string descripcion, DateTime fecha){
        BD miBd = new BD();
        bool finalizada = false;
        int? idUsuarioInSession = HttpContext.Session.GetInt32("usuarioId");

        miBd.AgregarTarea(titulo, descripcion, fecha, finalizada, idUsuarioInSession.Value);

        return RedirectToAction("Tasks", "Home", new { idSolicitado = idUsuarioInSession.Value});
    }

    public IActionResult EditTarea(int id){
        BD miBd = new BD();
        Tarea tarea = miBd.BuscarTareaPorId(id);
        ViewBag.tarea = tarea;
        return View();
    }
    public IActionResult SendEditTarea(int id, string titulo, string descripcion, DateTime fecha, bool finalizada){
        BD miBd = new BD();
        Tarea tarea = miBd.BuscarTareaPorId(id);
        miBd.UpdateTarea(id, titulo, descripcion, fecha, finalizada);
        return RedirectToAction("Tasks", new { idSolicitado = tarea.idUsuario });

    }

    public IActionResult EliminarTarea(Tarea tarea){
        BD miBd = new BD();
        miBd.DeleteTarea(tarea);

        return RedirectToAction("Tasks", new { idSolicitado = tarea.idUsuario });
    }

    public IActionResult CambiarEstado(int id){
        BD miBd = new BD();
        miBd.CambiarEstado(id);
        int? idUsuarioInSession = HttpContext.Session.GetInt32("usuarioId");
        if (idUsuarioInSession == null)
        {
            return RedirectToAction("Index");
        }
        return RedirectToAction("Tasks", new{ idSolicitado =  idUsuarioInSession });
    }
}
