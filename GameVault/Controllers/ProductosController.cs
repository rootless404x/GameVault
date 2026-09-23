using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace GameVault.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaDbContext _context;

        public ProductosController(TiendaDbContext context)
        {
            _context = context;
        }

        // READ - listar productos
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .FromSqlRaw("EXEC spListarProductos")
                .ToListAsync();

            return View(productos);
        }


        // CREATE - mostrar formulario
        public IActionResult Create()
        {
            return View();
        }


        // CREATE - guardar producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Nombre,Precio,Stock,Categoria,Descripcion,Imagen")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                    new SqlParameter("@Nombre",
                        producto.Nombre ?? (object)DBNull.Value),

                    new SqlParameter("@Precio",
                        producto.Precio),

                    new SqlParameter("@Stock",
                        producto.Stock),

                    new SqlParameter("@Categoria",
                        producto.Categoria ?? (object)DBNull.Value),

                    new SqlParameter("@Descripcion",
                        producto.Descripcion ?? (object)DBNull.Value),

                    new SqlParameter("@Imagen",
                        producto.Imagen ?? (object)DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spInsertarProducto @Nombre, @Precio, @Stock, @Categoria, @Descripcion, @Imagen",
                    parameters
                );

                return RedirectToAction(nameof(Index));
            }

            return View(producto);
        }


        // EDIT - mostrar formulario
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = new SqlParameter("@Id", id);

            var producto = await _context.Productos
                .FromSqlRaw("EXEC spBuscarProducto @Id", parameter)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        // EDIT - guardar cambios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Nombre,Precio,Stock,Categoria,Descripcion,Imagen")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                    new SqlParameter("@Id",
                        producto.Id),

                    new SqlParameter("@Nombre",
                        producto.Nombre ?? (object)DBNull.Value),

                    new SqlParameter("@Precio",
                        producto.Precio),

                    new SqlParameter("@Stock",
                        producto.Stock),

                    new SqlParameter("@Categoria",
                        producto.Categoria ?? (object)DBNull.Value),

                    new SqlParameter("@Descripcion",
                        producto.Descripcion ?? (object)DBNull.Value),

                    new SqlParameter("@Imagen",
                        producto.Imagen ?? (object)DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spActualizarProducto @Id, @Nombre, @Precio, @Stock, @Categoria, @Descripcion, @Imagen",
                    parameters
                );

                return RedirectToAction(nameof(Index));
            }

            return View(producto);
        }


        // DELETE - mostrar confirmación
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = new SqlParameter("@Id", id);

            var producto = await _context.Productos
                .FromSqlRaw("EXEC spBuscarProducto @Id", parameter)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        // DELETE - confirmar eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parameter = new SqlParameter("@Id", id);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spEliminarProducto @Id",
                parameter
            );

            return RedirectToAction(nameof(Index));
        }
    }
}
