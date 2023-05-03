using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.IdentityModel.Tokens;
using SistemaContatos.Filters;
using SistemaContatos.Models;
using SistemaContatos.Models.ViewModels;
using SistemaContatos.Services;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SistemaContatos.Controllers
{
    [LoggedUser]
    public class HomeController : Controller
    {
		
        public IActionResult Index()
        {
			return View();
        }

		[Authorize(Roles = "admin")]
		public string GetTeste()
		{
			return "teste";
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