using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Repository;

namespace SistemaContatos.Controllers
{
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
            _contatoRepository.Adicionar(contato);
            return RedirectToAction("Index", "Contato");
        }

        [HttpPost]
        public IActionResult Editar(ContatoModel contato)
        {
            _contatoRepository.Editar(contato);
            return RedirectToAction("Index", "Contato");
        }
        [HttpGet]
        public IActionResult Deletar(Guid id)
        {
            _contatoRepository.Deletar(id);
            return RedirectToAction("Index", "Contato");
        }


    }
}
