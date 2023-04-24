using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class ContatoModel
    {
        
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Digite um nome")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "minimo 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Digite um email")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite um telefone")]
        [Phone(ErrorMessage = "Telefone inválido!")]
        public string Phone { get; set; }
        public Guid _UserId { get; set; }
    }
}
