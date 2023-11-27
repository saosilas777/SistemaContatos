using DocumentFormat.OpenXml.Drawing;
using OfficeOpenXml;
using RestSharp.Extensions;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Models.ViewModels;

namespace SistemaContatos.Services.SendFileServices
{
	public class SendFileServices : ISendFileServices
	{
		private readonly ISection _section;

		public SendFileServices(ISection section)
		{
			_section = section;
		}

		public List<ContatoModel> ReadXls(IFormFile uploadFile)
		{
			var streamFile = ReadStrem(uploadFile);

			string token = _section.GetUserSection();
			UserModel user = TokenService.GetDataInToken(token);
			List<ContatoModel> response = new List<ContatoModel>();
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			using (ExcelPackage package = new ExcelPackage((Stream)streamFile))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
				int colCount = worksheet.Dimension.End.Column;
				int rowCount = worksheet.Dimension.End.Row;




				for (int row = 2; row < rowCount; row++)
				{
					ContatoModel contato = new ContatoModel();
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

		public MemoryStream ReadStrem(IFormFile file)
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
