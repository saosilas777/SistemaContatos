using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
	public class RecoveryPasswordModel
	{
		[Required(ErrorMessage = "Digite um login")]
		public string? Login { get; set; }
		[Required(ErrorMessage = "Digite um email")]
		public string? Email { get; set; }
	}
}
