using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Controllers
{
    public class ContactosController : Controller
    {
        private readonly TiendaDbContext _context;

        public ContactosController(TiendaDbContext context)
        {
            _context = context;
        }

        // Listar - mensajes
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EsAdmin") != "True")
            {
                return RedirectToAction("Index", "Login");
            }

            var contactos = await _context.Contactos
                .FromSqlRaw("EXEC spListarContactos")
                .ToListAsync();

            return View(contactos);
        }

        // Listar - detalles del mensaje
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "True")
            {
                return RedirectToAction("Index", "Login");
            }

            var contactos = await _context.Contactos
                .FromSqlRaw(
                    "EXEC spListarContactos"
                )
                .ToListAsync();

            var contacto = contactos.FirstOrDefault(c => c.Id == id);

            if (contacto == null)
            {
                return NotFound();
            }

            //Actualizar - estado del mensaje
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spMarcarContactoLeido @Id = {0}",
                id
            );

            contacto.Leido = true;

            return View(contacto);
        }

        //Confirmar - eliminar mensaje
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "True")
            {
                return RedirectToAction("Index", "Login");
            }

            var contactos = await _context.Contactos
                .FromSqlRaw(
                    "EXEC spListarContactos"
                )
                .ToListAsync();

            var contacto = contactos.FirstOrDefault(c => c.Id == id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // Eliminar - mensaje
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "True")
            {
                return RedirectToAction("Index", "Login");
            }

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spEliminarContacto @Id = {0}",
                id
            );

            return RedirectToAction(nameof(Index));
        }
    }
}