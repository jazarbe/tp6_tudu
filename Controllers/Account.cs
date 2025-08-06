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

    public IActionResult LogIn(string nombre, string passwordIntentada){
        BD miBd = new BD();
        Usuario intentoIntegrante = miBd.BuscarUsuarioPorUsername(nombre);
        if(intentoIntegrante == null){
            ViewBag.mensaje = "Nombre de usuario inexistente";
            return View("Index");
        }
        else if(passwordIntentada != intentoIntegrante.password){
            ViewBag.mensaje = "contraseña incorrecta";
            return View("Index");
        }
        else{
            string rutaFoto = "/images/default.png";
            rutaFoto = "/images/" + intentoIntegrante.foto;
            HttpContext.Session.SetString("fotoPerfil", rutaFoto);
            // HttpContext.Session.SetInt32("usuarioId", intentoIntegrante.id);
            return RedirectToAction("Tasks", "Home", new { idSolicitado = intentoIntegrante.id });
        }
    }
    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    public IActionResult OlvidePassword()
    {
        return View();
    }

    public IActionResult CambiarPassword(string nombre, string nuevapassword)
    {
        BD miBd = new BD();
        Usuario integrante = miBd.BuscarUsuarioPorUsername(nombre);
        if (integrante == null)
        {
            ViewBag.mensaje = "El usuario no existe";
            return View("OlvidePassword");
        }

        miBd.CambiarPassword(nombre, nuevapassword);

        ViewBag.mensaje = "Contraseña cambiada correctamente";
        return View("Index");
    }
    public IActionResult SignIn()
    {
        return View();
    }
    public IActionResult CrearCuenta(string nombre, string password, string username, DateTime fecha, IFormFile foto, int idUsuario)
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

        miBd.AgregarUsuario(nombre, password, username, fecha, nombreArchivo, idUsuario);

        ViewBag.mensaje = "Cuenta creada correctamente.";
        return View("Index");
    }
}