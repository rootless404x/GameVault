using GameVault.Data;
using GameVault.Reportes; // Ajusta a tu namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace GameVault.Controllers
{
    public class ReportesController : Controller
    {
        private readonly TiendaDbContext _context; // Reemplaza por el nombre de tu DbContext

        public ReportesController(TiendaDbContext context)
        {
            _context = context;

            // Configurar la licencia gratuita de QuestPDF para proyectos comunitarios/estudiantiles
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public IActionResult Productos()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GenerarReportePdf(string? nombre, string? categoria, decimal? precioMax)
        {
            // 1. Consulta con filtros dinámicos a SQL Server
            var query = _context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                query = query.Where(p => p.Categoria.Contains(categoria));
            }

            if (precioMax.HasValue && precioMax.Value > 0)
            {
                query = query.Where(p => p.Precio <= precioMax.Value);
            }

            var lista = await query.Select(p => new ProductoReporteDto
            {
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Precio = p.Precio ?? 0m,
                Stock = p.Stock ?? 0
            }).ToListAsync();

            // 2. Generar el documento con QuestPDF
            var document = new ReporteProductosDocument(lista);
            byte[] pdfBytes = document.GeneratePdf();

            // 3. Devolver como "inline" para abrir vista previa en el navegador/iframe
            Response.Headers.Append("Content-Disposition", "inline; filename=Reporte_Productos.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }
}