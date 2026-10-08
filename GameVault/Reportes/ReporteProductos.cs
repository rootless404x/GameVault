using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GameVault.Reportes
{
    // Define la estructura de datos que mostrarás en la tabla
    public class ProductoReporteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }

    public class ReporteProductosDocument : IDocument
    {
        private readonly List<ProductoReporteDto> _productos;

        public ReporteProductosDocument(List<ProductoReporteDto> productos)
        {
            _productos = productos;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.Size(PageSizes.A4);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeFooter);
                });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("GameVault - Reporte de Productos").FontSize(20).Bold().FontColor(Colors.Blue.Darken2);
                    column.Item().Text($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(10).FontColor(Colors.Grey.Medium);
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(15).Table(table =>
            {
                // Definición de las 4 columnas
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); // Nombre
                    columns.RelativeColumn(2); // Categoria
                    columns.RelativeColumn(1); // Precio
                    columns.RelativeColumn(1); // Stock
                });

                // Encabezados de la Tabla
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Nombre").Bold();
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Categoría").Bold();
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Precio").Bold();
                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Stock").Bold();
                });

                // Filas de Productos
                foreach (var item in _productos)
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Nombre);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Categoria);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"S/. {item.Precio:N2}");
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text(item.Stock.ToString());
                }
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(x =>
            {
                x.Span("Página ");
                x.CurrentPageNumber();
                x.Span(" de ");
                x.TotalPages();
            });
        }
    }
}