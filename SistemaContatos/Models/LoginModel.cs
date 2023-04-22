using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite um login")]
        public string?  Login { get; set; }
        [Required (ErrorMessage = "Digite uma senha")]
        public string? Password { get; set; }
    }
}
