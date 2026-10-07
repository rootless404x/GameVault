using GameVault.Models;

namespace GameVault.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Producto> Productos { get; set; }

        public Contacto Contacto { get; set; }
    }
}
