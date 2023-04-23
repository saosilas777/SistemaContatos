using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Repository;

namespace SistemaContatos.Controllers
{
	[LoggedUser]
	public class ContatoController : Controller
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepository.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
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
