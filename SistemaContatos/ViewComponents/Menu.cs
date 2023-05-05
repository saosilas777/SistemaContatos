using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContatos.Models;
using SistemaContatos.Services;

namespace SistemaContatos.ViewComponents
{
	public class Menu : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				string? UserSection = HttpContext.Session.GetString("Token");

				if (string.IsNullOrEmpty(UserSection))
				{
					return null;
				}
				UserModel? user = TokenService.GetDataInToken(UserSection);
				return View(user);
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
			
		}

	}
}
