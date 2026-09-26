using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameVault.Controllers
{
    public class RegistroController : Controller
    {
        private readonly TiendaDbContext _context;

        public RegistroController(TiendaDbContext context)
        {
            _context = context;
        }

        // cargar vista de registro
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Usuario usuario)
        {
            bool correoExiste = _context.Usuarios.Any(u => u.Correo == usuario.Correo);

            if (correoExiste)
            {
                ViewBag.Mensaje = "Este correo ya está registrado.";
                return View(usuario);
            }

            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction("Index", "Login");
            }

            return View(usuario);
        }
    }
}
