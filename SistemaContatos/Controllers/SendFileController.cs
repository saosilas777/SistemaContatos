using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Models.ViewModels;
using SistemaContatos.Services.SendFileServices;
using System.ComponentModel;
using System.IO;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace SistemaContatos.Controllers
{
	public class SendFileController : Controller
	{
		private readonly IContatoRepository _contatoRepository;
		private readonly ISection _section;
		private readonly SendFileServices _sendFileServices;

		public SendFileController(IContatoRepository contatoRepository,ISection section, SendFileServices sendFileServices )
		{
			_contatoRepository = contatoRepository;
			_section = section;
			_sendFileServices = sendFileServices;
		}

		[HttpPost]
		public IActionResult SendFile(IFormFile uploadFile)
		{
			List<ContatoModel> contatos = _sendFileServices.ReadXls(uploadFile);
			_contatoRepository.AdicionarTodos(contatos);
			return RedirectToAction("Index", "Contato");
		}

		public IActionResult EnviarArquivo()
		{
			return RedirectToAction("Index", "Home");
		}

		

	}
}
