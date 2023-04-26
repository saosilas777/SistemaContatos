using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using System.ComponentModel;
using System.IO;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace SistemaContatos.Controllers
{
	public class SendFileController : Controller
	{
		private readonly IContatoRepository _contatoRepository;
		private readonly ISection _section;

		public SendFileController(IContatoRepository contatoRepository,ISection section)
		{
			_contatoRepository = contatoRepository;
			_section = section;
		}

		[HttpPost]
		public IActionResult SendFile(IFormFile uploadFile)
		{
			var streamFile = ReadStrem(uploadFile);
			List<ContatoModel> contatos = ReadXls(streamFile);

			_contatoRepository.AdicionarTodos(contatos);
			return RedirectToAction("Index", "Contato");
		}

		public IActionResult EnviarArquivo()
		{
			return RedirectToAction("Index", "Home");
		}

		public List<ContatoModel> ReadXls(object streamFile)
		{
			UserModel user = _section.GetUserSection();
			List<ContatoModel> response = new List<ContatoModel>();
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;			

			using (ExcelPackage package = new ExcelPackage((Stream)streamFile))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
				int colCount = worksheet.Dimension.End.Column;
				int rowCount = worksheet.Dimension.End.Row;

				for (int row = 1; row < rowCount; row++)
				{
					var contato = new ContatoModel();
					contato.Id = Guid.NewGuid();
					contato.Name = worksheet.Cells[row, 1].Value.ToString();
					contato.Email = worksheet.Cells[row, 2].Value.ToString();
					contato.Phone = worksheet.Cells[row, 3].Value.ToString();
					contato.UserId = user.Id;

					response.Add(contato);
				}
			}
			return response;

		}

		protected MemoryStream ReadStrem(IFormFile file)
		{
			using (var stream = new MemoryStream())
			{

				file?.CopyTo(stream);
				var byteArray = stream.ToArray();
				return new MemoryStream(byteArray);

			}
		}

	}
}
