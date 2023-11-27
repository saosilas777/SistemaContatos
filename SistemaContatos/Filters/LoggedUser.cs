using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SistemaContatos.Models;

namespace SistemaContatos.Filters
{
	public class LoggedUser : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			//mudar UserLoged para UserLogged
			string? userSection = context.HttpContext.Session.GetString("Token");
			if (string.IsNullOrEmpty(userSection))
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
			}
			//else
			//{
			//	UserModel? user = JsonConvert.DeserializeObject<UserModel>(userSection);
			//	if (user == null)
			//	{
			//		context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
			//	}
			//}
			base.OnActionExecuting(context);
		}
	}
}
