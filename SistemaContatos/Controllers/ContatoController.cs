using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Services;
using System.Collections.Generic;

namespace SistemaContatos.Controllers
{
	//[LoggedUser]
	public class ContatoController : Controller
    {
        private readonly IContatoRepository _contatoRepository;
		private readonly ISection _section;

		public ContatoController(IContatoRepository contatoRepository, ISection section)
        {
            _contatoRepository = contatoRepository;
            _section = section;
        }

        public IActionResult Index()
        {
			string token = _section.GetUserSection();

			UserModel user = TokenService.GetDataInToken(token);
            
			List<ContatoModel> contatos = _contatoRepository.BuscarTodos(user.Id);
			
			return View(contatos);
		}

        public IActionResult Criar()
        {
            string token = _section.GetUserSection();
            UserModel user = TokenService.GetDataInToken(token);
			ContatoModel contatoModel = new ContatoModel();
			contatoModel.UserId = user.Id;
			return View(contatoModel);


		} 
        public IActionResult Editar()
        {
			string token = _section.GetUserSection();
			UserModel user = TokenService.GetDataInToken(token);
			ContatoModel contato = _contatoRepository.BuscarPorId(user.Id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(Guid id)
        {
			var contato = _contatoRepository.BuscarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Apagar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
					string token = _section.GetUserSection();
					UserModel user = TokenService.GetDataInToken(token);
					contato.UserId = user.Id;
					_contatoRepository.Adicionar(contato);
                    TempData["SuccessMessage"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index", "Contato");

                }
                return View(contato);

            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível efetuar o cadastro, tente novamente! erro:{e.Message}";
                return RedirectToAction("Index", "Contato");
            }
        }

        [HttpPost]
        public IActionResult Editar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
					string token = _section.GetUserSection();
					UserModel user = TokenService.GetDataInToken(token);
					contato.UserId = user.Id;
					_contatoRepository.Editar(contato);
                    TempData["SuccessMessage"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index", "Contato");

                }
                return View(contato);

            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível efetuar o cadastro, tente novamente! erro:{e.Message}";
                return RedirectToAction("Index", "Contato");
            }

        }
        [HttpGet]
        public IActionResult Deletar(Guid id)
        {
            try
            {
				bool result = _contatoRepository.Deletar(id);

                if (result)
                {
                    TempData["SuccessMessage"] = "Contato apagado com sucesso";
                }
                else
                {
                    TempData["ErrorMessage"] = "Não foi possível apagar o contato";
                }
                return RedirectToAction("Index", "Contato");    
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível apagar o contato! erro:{e.Message}";
                return RedirectToAction("Index", "Contato");
            }
        }


    }
}
