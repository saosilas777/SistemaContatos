using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Models;
using SistemaContatos.Models.ViewModels;
using System.Diagnostics;

namespace SistemaContatos.Controllers
{
    [LoggedUser]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}