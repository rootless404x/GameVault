using System.ComponentModel.DataAnnotations;

namespace GameVault.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no debe superar los 150 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 10000.00, ErrorMessage = "El precio debe ser un número positivo (entre 0.01 y 10,000.00).")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, 10000, ErrorMessage = "El stock debe ser un número entero mayor o igual a 0.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public string Categoria { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción no puede superar los 1000 caracteres.")]
        public string Descripcion { get; set; }

        [Url(ErrorMessage = "La imagen debe ser una URL válida (ej. https://sitio.com/imagen.jpg).")]
        public string Imagen { get; set; }
    }
}
