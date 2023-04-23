using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SistemaContatos.Models;

namespace SistemaContatos.Filters
{
	public class LoggedUser : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			//mudar UserLoged para UserLogged
			string userSection = context.HttpContext.Session.GetString("UserLoged");
			if (string.IsNullOrEmpty(userSection))
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
			}
			else
			{
				UserModel user = JsonConvert.DeserializeObject<UserModel>(userSection);
				if (user == null)
				{
					context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
				}
			}
			base.OnActionExecuted(context);
		}
	}
}
