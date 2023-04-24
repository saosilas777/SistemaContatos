﻿using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Repository;

namespace SistemaContatos.Controllers
{
	[LoggedUser]
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
			UserModel user = _section.GetUserSection();
			List<ContatoModel> contatos = _contatoRepository.BuscarTodos();
			List<ContatoModel> _contatos = new List<ContatoModel>();
			foreach (var item in contatos)
            {
				if (item._UserId == user.Id)
                { 
                    _contatos.Add(item);
					
				}
			}
			return View(_contatos);
		}

        public IActionResult Criar()
        {
            UserModel user = _section.GetUserSection();
            ContatoModel contatoModel = new ContatoModel();
			contatoModel._UserId = user.Id;
			return View(contatoModel);


		} 
        public IActionResult Editar(Guid id)
        {
            ContatoModel contato = _contatoRepository.BuscarPorId(id);
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
