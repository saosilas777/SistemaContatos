using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContatos.Models;
using SistemaContatos.Enums;
using SistemaContatos.Services;

namespace SistemaContatos.Filters
{
	public class AdminLogged : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			try
			{
				string? userSection = context.HttpContext.Session.GetString("Token");
				if (userSection != null)
				{
					base.OnActionExecuting(context);
				}	


				if (string.IsNullOrEmpty(userSection))
				{
					context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
				}
				else
				{
					UserModel? user = TokenService.GetDataInToken(userSection);
					
					if (user == null)
					{
						context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
					}

					if (user?.Perfil != @PerfilEnum.admin)
					{
						context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrict" }, { "action", "Index" } });
					}
				}
				base.OnActionExecuting(context);
			}
			catch (Exception e)
			{

				base.OnActionExecuting(context);


			}
			
		}
	}
}
