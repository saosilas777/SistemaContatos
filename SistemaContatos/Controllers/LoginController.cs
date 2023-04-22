using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Models;

namespace SistemaContatos.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Login", login);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível efetuar seu login! erro:{e.Message}";
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
