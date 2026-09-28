using GameVault.Data;
using GameVault.Models;
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

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .FromSqlRaw("EXEC spListarProductos")
                .ToListAsync();

            return View(productos);
        }
    }
}