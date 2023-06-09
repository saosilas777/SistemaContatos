﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContatos.Models;
using SistemaContatos.Enums;
using SistemaContatos.Services;

namespace SistemaContatos.Filters
{
	public class AdminLogged : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			try
			{
				string? userSection = context.HttpContext.Session.GetString("Token");
				if (userSection != null)
				{
					base.OnActionExecuted(context);
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
				base.OnActionExecuted(context);
			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
			
		}
	}
}
