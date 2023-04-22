using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class ContatoModel
    {
        
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Digite um nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Digite um email")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite um telefone")]
        [Phone(ErrorMessage = "Telefone inválido!")]
        public string Phone { get; set; }
    }
}
