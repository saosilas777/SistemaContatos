using Microsoft.AspNetCore.Mvc;

namespace SistemaContatos.Controllers
{
	public class SendFileController : Controller
	{
		[HttpPost]
		public IActionResult SendFile(IList<IFormFile> file)
		{
			
			return Ok();
		}

		public IActionResult EnviarArquivo()
		{
			return RedirectToAction("Index", "Home");
		}
	}
}
