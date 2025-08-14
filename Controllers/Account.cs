using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp6_tudu.Models;

namespace tp6_tudu.Controllers;

public class Account  : Controller
{
    private readonly ILogger<Account> _logger;

    public Account(ILogger<Account> logger)
    {
        _logger = logger;
    }

    public IActionResult LogIn(string username, string passwordIntentada){
        BD miBd = new BD();
        string msg = null;
        Usuario intentoIntegrante = miBd.BuscarUsuarioPorUsername(username);
        if(intentoIntegrante == null){
            msg = "Nombre de usuario inexistente";
            return RedirectToAction("Index", "Home", new {mensaje = msg});
        }
        else if(passwordIntentada != intentoIntegrante.password){
            msg = "Contraseña incorrecta";
            return RedirectToAction("Index", "Home", new {mensaje = msg});
        }
        else{
            string rutaFoto = "/images/default.png";
            rutaFoto = "/images/" + intentoIntegrante.foto;
            HttpContext.Session.SetString("fotoPerfil", rutaFoto);
            HttpContext.Session.SetInt32("usuarioId", intentoIntegrante.id);
            miBd.ActualizarLogin(intentoIntegrante.id);


            return RedirectToAction("Tasks", "Home", new { idSolicitado = intentoIntegrante.id });
        }
    }

    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
    public IActionResult OlvidePassword()
    {
        return View();
    }

    public IActionResult CambiarPassword(string username, string nuevapassword)
    {
        BD miBd = new BD();
        Usuario integrante = miBd.BuscarUsuarioPorUsername(username);
        if (integrante == null)
        {
            ViewBag.mensaje = "El usuario no existe";
            return View("OlvidePassword");
        }
        
        miBd.CambiarPassword(username, nuevapassword);

        ViewBag.mensaje = "Contraseña cambiada correctamente";
        return RedirectToAction("Index", "Home");
    }
    public IActionResult SignUp()
    {
        return View();
    }
    public IActionResult CrearCuenta(string nombre, string apellido, string password, string username, DateTime fecha, IFormFile foto, int idUsuario)
    {
        BD miBd = new BD();

        if (miBd.BuscarUsuarioPorUsername(username) != null)
        {
            ViewBag.mensaje = "El nombre de usuario ya existe.";
            return View("SignUp");
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

        miBd.AgregarUsuario(nombre, apellido, password, username, nombreArchivo);

        ViewBag.mensaje = "Cuenta creada correctamente.";
        return RedirectToAction("Index", "Home");
    }
}