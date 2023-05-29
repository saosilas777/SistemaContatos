using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Services;

namespace SistemaContatos.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly ISection _section;
		private readonly ISendEmail _mail;



		public LoginController(IUserRepository userRepository, ISection section, ISendEmail mail)
		{
			_userRepository = userRepository;
			_section = section;
			_mail = mail;
		}

		public IActionResult Index()
		{
			string token = _section.GetUserSection();
			if (TokenService.TokenIsValid(token))
			{
				return RedirectToAction("Index", "Home");

			}
			return View();
		}
		public IActionResult LogoutConfirm()
		{
			return View();
		}

		public IActionResult Logout()
		{
			_section.UserSectionRemove();
			return RedirectToAction("Index", "Login");
		}

		[HttpPost]
		public IActionResult Login(LoginModel login)
		{

			try
			{
				if (ModelState.IsValid)
				{
					UserModel user = _userRepository.BuscarPorLogin(login._Login);

					var authenticated = TokenService.Authenticate(user);
					if (user != null)
					{
						if (user.SenhaValida(login._Password))
						{
							if (authenticated.Result != null)
							{
								if (!TokenService.TokenIsValid(authenticated.Result)) return RedirectToAction("Login", "Login");
								_section.UserSectionCreate(authenticated.Result);
								return RedirectToAction("Index", "Home", new { token = authenticated.Result });
							}
						}
					}

				}
				TempData["ErrorMessage"] = "Usuário ou senha inválidos, tente novamente!";
				return View("Index");
			}
			catch (Exception e)
			{
				TempData["ErrorMessage"] = $"Não foi possível efetuar seu login! erro:{e.Message}";
				return RedirectToAction("Index", "Login");
			}
		}

		public IActionResult RecoveryPassword()
		{
			return View();
		}

		[HttpPost]
		public IActionResult RecoveryPassword(RecoveryPasswordModel recoveryPassword)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UserModel user = _userRepository.BuscarPorEmaileLogin(recoveryPassword.Login, recoveryPassword.Email);
					if (user != null)
					{
						string newPwd = user.PasswordGeneration();
						string message = $"Senha temporária é: {newPwd}";
						bool sendmail = _mail.SendEmail(user.Email, "Sistema de Contatos - nova senha", message);
						if (!sendmail)
						{
							TempData["ErrorMessage"] = "Não foi possível redefinir sua senha, verifique os dados informados!";
							return RedirectToAction("RecoveryPassword", "Login");
						}
						_userRepository.Editar(user);
						TempData["SuccessMessage"] = "Uma senha foi enviada no email cadastrado!";
						return RedirectToAction("Index", "Login");
					}
					TempData["ErrorMessage"] = "Não foi possível redefinir sua senha, verifique os dados informados!";
					return RedirectToAction("RecoveryPassword", "Login");
				}
				return View("Login");
			}
			catch (Exception e)
			{
				TempData["ErrorMessage"] = $"Não foi possível redefinir sua senha,tente novamente! erro:{e.Message}";
				return RedirectToAction("RecoveryPassword", "Login");
			}

		}

	}
}
