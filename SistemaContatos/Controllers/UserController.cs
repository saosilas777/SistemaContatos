using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Repository;

namespace SistemaContatos.Controllers
{
	
    [AdminLogged]
	public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            List<UserModel> user = _userRepository.BuscarTodos();
            return View(user);
        }

        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(Guid id)
        {
            UserModel user = _userRepository.BuscarPorId(id);
            return View(user);
        }
        public IActionResult ApagarConfirmacao(Guid id)
        {
            var user = _userRepository.BuscarPorId(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Apagar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Adicionar(user);
                    TempData["SuccessMessage"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index", "User");

                }
                return View(user);

            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível efetuar o cadastro, tente novamente! erro:{e.Message}";
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public IActionResult Editar(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Editar(user);
                    TempData["SuccessMessage"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index", "User");

                }
                return View(user);

            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível efetuar o cadastro, tente novamente! erro:{e.Message}";
                return RedirectToAction("Index", "User");
            }

        }
        [HttpGet]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                bool result = _userRepository.Deletar(id);

                if (result)
                {
                    TempData["SuccessMessage"] = "Usuário apagado com sucesso";
                }
                else
                {
                    TempData["ErrorMessage"] = "Não foi possível apagar o usuário";
                }
                return RedirectToAction("Index", "User");    
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível apagar o usuário! erro:{e.Message}";
                return RedirectToAction("Index", "User");
            }
        }


    }
}
