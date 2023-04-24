using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
	public class PasswordUpdateModel
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage = "Digite uma Senha válida")]
		public string PastPassword { get; set; }
		[Required(ErrorMessage = "Digite uma nova senha válida")]
		public string NewPassword { get; set; }
	}
}
