using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite um login")]
        public string _Login { get; set; }
        [Required(ErrorMessage = "Digite uma Senha")]
        public string _Password { get; set; }
    }
}
