using SistemaContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Digite um nome")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Digite um sobrenome")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Digite um login")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Digite um email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite uma senha")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Digite um perfil de usuário")]
        public PerfilEnum? Perfil { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? RegistrationUpdate { get; set; }

    }
}
