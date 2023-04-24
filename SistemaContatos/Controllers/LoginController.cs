﻿using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;

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

		public IActionResult Login()
		{
			if (_section.GetUserSection() != null)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		public IActionResult Logout()
		{
			_section.UserSectionRemove();
			return RedirectToAction("Login", "Login");
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
							_section.UserSectionCreate(user);
							return RedirectToAction("Index", "Home");
						}
						TempData["ErrorMessage"] = "Senha inválida, tente novamente!";
						return View("Login", login);
					}
					TempData["ErrorMessage"] = "Usuário ou senha inválidos, tente novamente!";
					return View("Login", login);
				}
				return View("Login", login);
			}
			catch (Exception e)
			{
				TempData["ErrorMessage"] = $"Não foi possível efetuar seu login! erro:{e.Message}";
				return RedirectToAction("Login", "Login");
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
						if (sendmail)
						{
							_userRepository.Editar(user);
							TempData["SuccessMessage"] = "Uma senha foi enviada no email cadastrado!";
							return RedirectToAction("Login", "Login");

						}
						else
						{
							TempData["ErrorMessage"] = "Não foi possível redefinir sua senha, verifique os dados informados!";
							return RedirectToAction("RecoveryPassword", "Login");
						}
					}
					TempData["ErrorMessage"] = "Não foi possível redefinir sua senha, verifique os dados informados!";
					return RedirectToAction("RecoveryPassword", "Login");
				}
				return View("Login");
			}
			catch (Exception e)
			{
				TempData["ErrorMessage"] = $"Não foi possível redefinir sua senha,tente novamente! erro:{e.Message}";
				return RedirectToAction("Login", "Login");
			}

		}
		
	}
}
