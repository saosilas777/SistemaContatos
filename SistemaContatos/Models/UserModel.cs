using SistemaContatos.Enums;
using SistemaContatos.Helper;
using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Digite um nome")]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "minimo 3")]
		public string FirstName { get; set; }
        [Required(ErrorMessage = "Digite um sobrenome")]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "minimo 3")]
		public string LastName { get; set; }
        [Required(ErrorMessage = "Digite um login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite um email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite uma senha")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Digite um perfil de usuário")]
        public PerfilEnum Perfil { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? RegistrationUpdate { get; set; }
		public virtual List<ContatoModel>? Contatos { get; set; }

        public bool SenhaValida(string pwd)
        {
            if(pwd == Password || Password == pwd.HashGeneration())
            {
                return true;
            }
            return false;
        }

        public void SetPwdHash()
        {
            Password = Password.HashGeneration();
        }

        public string PasswordGeneration()
        {
            string newPwd = Guid.NewGuid().ToString().Substring(0,8);
            Password = newPwd.HashGeneration();
            return newPwd;
        }
    }
}
