using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;

namespace SistemaContatos.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserRepository _userRepository;

		public LoginController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginModel login)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UserModel user = _userRepository.BuscarPorLogin(login._Login);
					if (user != null)
					{
						if (user.SenhaValida(login._Password))
						{

							return RedirectToAction("Index", "Home");
						}
						TempData["ErrorMessage"] = "Senha inválida, tente novamente!";
					}
					TempData["ErrorMessage"] = "Usuário ou senha inválidos, tente novamente!";
				}
				return View("Login", login);
			}
			catch (Exception e)
			{
				TempData["ErrorMessage"] = $"Não foi possível efetuar seu login! erro:{e.Message}";
				return RedirectToAction("Login", "Login");
			}
		}
	}
}
