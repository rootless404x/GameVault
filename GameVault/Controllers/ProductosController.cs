using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace GameVault.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(TiendaDbContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Verficiacion de admin
        private bool VerificarAdmin()
        {
            string esAdmin = HttpContext.Session.GetString("EsAdmin");
            return esAdmin == "True";
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!VerificarAdmin())
            {
                context.Result = RedirectToAction("Index", "Login");
            }

            base.OnActionExecuting(context);
        }

        // READ - listar productos
        public async Task<IActionResult> Index()
        {
            try
            {
                var productos = await _context.Productos
                    .FromSqlRaw("EXEC spListarProductos")
                    .ToListAsync();

                return View(productos);
            }
            catch (Exception ex)
            {
                // Registra el error en el archivo .txt
                _logger.LogError(ex, "Error al listar los productos desde la base de datos.");
                return View(new List<Producto>());
            }
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
    [Bind("Nombre,Precio,Stock,Categoria,Descripcion")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var parameters = new[]
                    {
                new SqlParameter("@Nombre", producto.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@Precio", producto.Precio),
                new SqlParameter("@Stock", producto.Stock),
                new SqlParameter("@Categoria", producto.Categoria ?? (object)DBNull.Value),
                new SqlParameter("@Descripcion", producto.Descripcion ?? (object)DBNull.Value)
            };

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC spInsertarProducto @Nombre, @Precio, @Stock, @Categoria, @Descripcion",
                        parameters
                    );

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al insertar el producto.");
                    ModelState.AddModelError(
                        string.Empty,
                        "Error al intentar registrar el producto."
                    );
                }
            }

            return View(producto);
        }


        //EDIT - mostrar formulario
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var parameter = new SqlParameter("@Id", id.Value);

                var producto = _context.Productos
                    .FromSqlRaw("EXEC spBuscarProducto @Id", parameter)
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault();

                if (producto == null)
                {
                    _logger.LogWarning(
                        "Producto con ID {Id} no fue encontrado para edición.",
                        id
                    );

                    return NotFound();
                }

                return View(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error al buscar el producto con ID {Id}.",
                    id
                );

                throw;
            }
        }

        // EDIT - guardar cambios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Nombre,Precio,Stock,Categoria,Descripcion")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Actualizando producto ID {Id}...", producto.Id);

                    var parameters = new[]
                    {
                        new SqlParameter("@Id", producto.Id),
                        new SqlParameter("@Nombre", producto.Nombre ?? (object)DBNull.Value),
                        new SqlParameter("@Precio", producto.Precio),
                        new SqlParameter("@Stock", producto.Stock),
                        new SqlParameter("@Categoria", producto.Categoria ?? (object)DBNull.Value),
                        new SqlParameter("@Descripcion", producto.Descripcion ?? (object)DBNull.Value),
                    };

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC spActualizarProducto @Id, @Nombre, @Precio, @Stock, @Categoria, @Descripcion",
                        parameters
                    );

                    _logger.LogInformation("Producto ID {Id} actualizado con éxito.", producto.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al actualizar el producto ID {Id}.", producto.Id);
                    ModelState.AddModelError(string.Empty, "Error al intentar actualizar el producto.");
                }
            }

            return View(producto);
        }

        // DELETE - mostrar confirmación
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var parameter = new SqlParameter("@Id", id.Value);

                var producto = _context.Productos
                    .FromSqlRaw("EXEC spBuscarProducto @Id", parameter)
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault();

                if (producto == null)
                {
                    _logger.LogWarning(
                        "Producto con ID {Id} no fue encontrado para eliminación.",
                        id
                    );

                    return NotFound();
                }

                return View(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error al buscar producto ID {Id} para eliminación.",
                    id
                );

                throw;
            }
        }

        // DELETE - confirmar eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _logger.LogInformation(
                    "Eliminando producto con ID {Id}",
                    id
                );

                var parameter = new SqlParameter("@Id", id);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spEliminarProducto @Id",
                    parameter
                );

                _logger.LogInformation(
                    "Producto ID {Id} eliminado con éxito.",
                    id
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error al eliminar el producto ID {Id}.",
                    id
                );

                ModelState.AddModelError(
                    string.Empty,
                    "Error al eliminar el producto."
                );

                return RedirectToAction(nameof(Index));
            }
        }
    } 
}
