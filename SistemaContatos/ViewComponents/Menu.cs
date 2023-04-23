using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContatos.Models;

namespace SistemaContatos.ViewComponents
{
	public class Menu : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			string UserSection = HttpContext.Session.GetString("UserLoged");

			if (string.IsNullOrEmpty(UserSection))
			{
				return null;
			}
			UserModel user = JsonConvert.DeserializeObject<UserModel>(UserSection);
			return View(user);
		}

	}
}
