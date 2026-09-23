using System.ComponentModel.DataAnnotations;

namespace GameVault.Models
{
    public class Contacto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingresa un formato de correo válido (ejemplo@dominio.com).")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El mensaje no puede estar vacío.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "El mensaje debe tener entre 10 y 500 caracteres.")]
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}
