using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SistemaContatos.Models;

namespace SistemaContatos.Helper
{
	public class Section : ISection
	{
		private readonly IHttpContextAccessor _httpContext;

		public Section(IHttpContextAccessor httpContext)
		{
			_httpContext = httpContext;
		}

		public string GetUserSection()
		{
			string? userSection = _httpContext.HttpContext.Session.GetString("Token");
			if (string.IsNullOrEmpty(userSection)) return null;
			return userSection;
		}

		public void UserSectionCreate(string token)
		{
			//string _user = JsonConvert.SerializeObject(user);
			_httpContext.HttpContext.Session.SetString("Token", token);
		}

		public void UserSectionRemove()
		{
			_httpContext.HttpContext.Session.Remove("Token");
		}
	}
}
