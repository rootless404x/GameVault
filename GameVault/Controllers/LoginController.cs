using GameVault.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GameVault.Controllers
{
    public class LoginController : Controller
    {
        private readonly TiendaDbContext _context;

        public LoginController(TiendaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string correo, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Password == password);

            if (usuario != null)
            {
                HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);
                HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

                HttpContext.Session.SetString("EsAdmin", usuario.EsAdmin.ToString());

                if (usuario.EsAdmin)
                {
                    return RedirectToAction("Index", "Productos");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Mensaje = "Correo o contraseña incorrectos.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}