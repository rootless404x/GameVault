using GameVault.Data;
using GameVault.Models;
using GameVault.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly TiendaDbContext _context;

        public HomeController(TiendaDbContext context)
        {
            _context = context;
        }

        //Listar - productos
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .FromSqlRaw("EXEC spListarProductos")
                .ToListAsync();

            var modelo = new HomeViewModel
            {
                Productos = productos,
                Contacto = new Contacto()
            };

            return View(modelo);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacto(Contacto contacto)
                {
                    if (!ModelState.IsValid)
                    {
                        var productos = await _context.Productos
                            .FromSqlRaw("EXEC spListarProductos")
                            .ToListAsync();

                        var modelo = new HomeViewModel
                        {
                            Productos = productos,
                            Contacto = contacto
                        };

                        return View("Index", modelo);
                    }

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC spRegistrarContacto @Nombre = {0}, @Email = {1}, @Mensaje = {2}",
                        contacto.Nombre,
                        contacto.Email,
                        contacto.Mensaje
                    );

                    TempData["MensajeContacto"] = "¡Tu mensaje fue enviado correctamente!";

                    return RedirectToAction("Index");
                }



    }
}