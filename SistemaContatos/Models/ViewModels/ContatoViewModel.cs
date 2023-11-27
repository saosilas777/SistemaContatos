namespace SistemaContatos.Models.ViewModels
{
	public class ContatoViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public Guid UserId { get; set; }


	}
}
