using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;

namespace SistemaContatos.Controllers
{
	//[LoggedUser]
	public class RestrictController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
