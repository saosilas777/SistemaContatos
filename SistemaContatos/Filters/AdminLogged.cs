using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContatos.Models;
using SistemaContatos.Enums;

namespace SistemaContatos.Filters
{
	public class AdminLogged : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			//mudar UserLoged para UserLogged
			string userSection = context.HttpContext.Session.GetString("UserLogged");
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

				if(user.Perfil != @PerfilEnum.admin)
				{
					context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrict" }, { "action", "Index" } });
				}
			}
			base.OnActionExecuted(context);
		}
	}
}
