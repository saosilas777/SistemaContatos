using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContatos.Models;

namespace SistemaContatos.ViewComponents
{
	public class Menu : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				string? UserSection = HttpContext.Session.GetString("UserLogged");

				if (string.IsNullOrEmpty(UserSection))
				{
					return null;
				}
				UserModel? user = JsonConvert.DeserializeObject<UserModel>(UserSection);
				return View(user);
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
			
		}

	}
}
