using SistemaContatos.Models;
using SistemaContatos.Models.ViewModels;

namespace SistemaContatos.Interfaces
{
	public interface ISendFileServices
	{
		public List<ContatoModel> ReadXls(IFormFile uploadFile);
		public MemoryStream ReadStrem(IFormFile file);
	}
}
