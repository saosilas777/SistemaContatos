using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Repository;
using SistemaContatos.Services;

namespace SistemaContatos.Controllers
{
	
   
	[LoggedUser]
	public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IContatoRepository _contatoRepository;
		private readonly ISection _section;

		public UserController(IUserRepository userRepository, IContatoRepository contatoRepository, ISection section)
        {
            _userRepository = userRepository;
            _contatoRepository = contatoRepository;
            _section = section;
        }

		[AdminLogged]
		public IActionResult Index()
        {
            
				var token = _section.GetUserSection();
                if (!TokenService.TokenIsValid(token))
                {
					_section.UserSectionRemove();
					return RedirectToAction("Index", "Login");
				} 
				List<UserModel> user = _userRepository.BuscarTodos();

				return View(user);
			
			
		}
		[AdminLogged]
		public IActionResult Criar()
        {
            return View();
        }
		[AdminLogged]
		public IActionResult Editar(Guid id)
        {
            UserModel user = _userRepository.BuscarPorId(id);
            return View(user);
        }
		[AdminLogged]
		public IActionResult ApagarConfirmacao(Guid id)
        {
            var user = _userRepository.BuscarPorId(id);
            return View(user);
        }
		
		public IActionResult PasswordUpdate(Guid id)
        {
            PasswordUpdateModel user = new PasswordUpdateModel();
            user.Id = id;
            return View(user);
        }
		[AdminLogged]
		public IActionResult ListarContatosPorUsuarioId(Guid id)
        {
            List<ContatoModel> contatos = _contatoRepository.BuscarTodos(id);

			return PartialView("_UserContacts",contatos);
        }

		[HttpPost]
		[AdminLogged]
		public IActionResult Apagar()
        {
            return View();
        }

        [HttpPost]
		[AdminLogged]
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
		[AdminLogged]
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
		[AdminLogged]
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

		[HttpPost]
		public IActionResult PasswordUpdate(PasswordUpdateModel pwd)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    if (_userRepository.PasswordUpdate(pwd))
                    {
						TempData["SuccessMessage"] = "Senha alterada com sucesso!";
						return RedirectToAction("Index", "User");

					}
					TempData["ErrorMessage"] = "Não foi possível alterar a senha, tente novamente!";
					return RedirectToAction("PasswordUpdate", "User");
				}
				TempData["ErrorMessage"] = "Não foi possível alterar a senha, tente novamente!";
				return RedirectToAction("PasswordUpdate", "User");
			}
            catch (Exception)
            {

				TempData["ErrorMessage"] = "Não foi possível alterar a senha, verifique os dados informados!";
				return RedirectToAction("PasswordUpdate", "User");
			}

		}


	}
}
