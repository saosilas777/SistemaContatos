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

		public UserModel GetUserSection()
		{
			string userSection = _httpContext.HttpContext.Session.GetString("UserLogged");
			if (string.IsNullOrEmpty(userSection)) return null;
			return JsonConvert.DeserializeObject<UserModel>(userSection);
		}

		public void UserSectionCreate(UserModel user)
		{
			string _user = JsonConvert.SerializeObject(user);
			_httpContext.HttpContext.Session.SetString("UserLogged", _user);
		}

		public void UserSectionRemove()
		{
			_httpContext.HttpContext.Session.Remove("UserLogged");
		}
	}
}
