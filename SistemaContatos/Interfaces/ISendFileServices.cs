using SistemaContatos.Models;

namespace SistemaContatos.Interfaces
{
	public interface ISendFileServices
	{
		public List<ContatoModel> ReadXls(IFormFile uploadFile);
		public MemoryStream ReadStrem(IFormFile file);
	}
}
