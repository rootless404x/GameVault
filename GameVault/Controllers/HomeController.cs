using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameVault.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
