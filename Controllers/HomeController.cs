using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tpNoNum.Models;

namespace tpNoNum.Controllers;

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

    public IActionResult LogIn(string nombre, string contraIntentada){
        BD miBd = new BD();
        Usuario intentoIntegrante = miBd.BuscarUsuarioPorUsername(nombre);
        if(intentoIntegrante == null){
            ViewBag.mensaje = "Nombre de usuario inexistente";
            return View("Index");
        }
        else if(contraIntentada != intentoIntegrante.contra){
            ViewBag.mensaje = "Contraseña incorrecta";
            return View("Index");
        }
        else{
            string rutaFoto = "/images/default.png";
            rutaFoto = "/images/" + intentoIntegrante.foto;
            HttpContext.Session.SetString("fotoPerfil", rutaFoto);
            HttpContext.Session.SetInt32("usuarioId", intentoIntegrante.id);
            return RedirectToAction("Perfil", new { idSolicitado = intentoIntegrante.id });
        }
    }
    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Perfil(int idSolicitado)
    {
        int? idUsuario = HttpContext.Session.GetInt32("usuarioId");
        if (idUsuario == null)
        {
            return RedirectToAction("Index");
        }

        BD miBd = new BD();
        Usuario intentoIntegrante = miBd.BuscarUsuarioPorId(idUsuario.Value);

        ViewBag.nombre = intentoIntegrante.nombre;
        ViewBag.contra = intentoIntegrante.contra; 
        ViewBag.email = intentoIntegrante.email;
        ViewBag.fechaNac = intentoIntegrante.fechaNac;
        ViewBag.telefono = intentoIntegrante.telefono;
        ViewBag.direccion = intentoIntegrante.direccion;
        ViewBag.rol = intentoIntegrante.rol;
        // List<Usuario> integrantesGrupo = miBd.ObtenerIntegrantesPorGrupo(intentoIntegrante.idGrupo);
        // ViewBag.integrantesGrupo = integrantesGrupo;
        
        return View();
    }
    public IActionResult OlvideContra()
    {
        return View();
    }

    public IActionResult CambiarContra(string nombre, string nuevaContra)
    {
        BD miBd = new BD();
        Usuario integrante = miBd.BuscarUsuarioPorUsername(nombre);
        if (integrante == null)
        {
            ViewBag.mensaje = "El usuario no existe";
            return View("OlvideContrasenia");
        }

        miBd.CambiarContra(nombre, nuevaContra);

        ViewBag.mensaje = "Contraseña cambiada correctamente";
        return View("Index");
    }
    public IActionResult SignIn()
    {
        return View();
    }
    public IActionResult CrearCuenta(string nombre, string contra, string email, DateTime fechaNac, string telefono, string direccion, string rol, IFormFile foto, int idGrupo)
    {
        BD miBd = new BD();

        if (miBd.BuscarUsuarioPorUsername(nombre) != null)
        {
            ViewBag.mensaje = "El nombre de usuario ya existe.";
            return View("Registro");
        }

        string nombreArchivo = "default.png";

        if (foto != null && foto.Length > 0)
        {
            string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            nombreArchivo = Path.GetFileName(foto.FileName);
            string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                foto.CopyTo(stream);
            }
        }

        miBd.AgregarUsuario(nombre, contra, email, fechaNac, telefono, direccion, rol, nombreArchivo, idGrupo);

        ViewBag.mensaje = "Cuenta creada correctamente.";
        return View("Index");
    }
}
