using GameVault.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Controllers
{
    public class LoginController : Controller
    {
        private readonly TiendaDbContext _context;

    public LoginController(TiendaDbContext context)
        {
            _context = context;
        }

        // Mostrar formulario de login
        public IActionResult Index()
        {
            return View();
        }

        // Comprobar usuario
        [HttpPost]
        public async Task<IActionResult> Index(string correo, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Password == password);

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = "Correo o contraseña incorrectos.";
            return View();
        }
    }

}
