using SistemaContatos.Data;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;

namespace SistemaContatos.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }
		public UserModel BuscarPorLogin(string login)
		{
			return _context.Users.FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper());
		}

		public UserModel BuscarPorEmaileLogin(string login, string email)
		{
			return _context.Users.FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper() && u.Email.ToUpper() == email.ToUpper());
		}

		public UserModel Adicionar(UserModel user)
        {
            user.RegistrationDate = DateTime.Now;
            user.RegistrationUpdate = DateTime.Now;
            user.SetPwdHash();
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserModel BuscarPorId(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }


		public List<UserModel> BuscarTodos()
        {
            return _context.Users.ToList();
        }

        public bool Deletar(Guid id)
        {
            UserModel userDb = BuscarPorId(id);

            if (userDb == null) throw new Exception("Contato não encontrado!");
            _context.Remove(userDb);
            _context.SaveChanges();
            return true;
        }

        public UserModel Editar(UserModel user)
        {
            UserModel userDb = BuscarPorId(user.Id);

            if (userDb == null) throw new Exception("Houve um erro ao atualizar contato!");

            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Login = user.Login;
            userDb.Email = user.Email;
            if(user.Password != userDb.Password)
            {
                userDb.Password = user.Password;
			}
            else
            {
				userDb.Password = userDb.Password;
			}
            userDb.Perfil = user.Perfil;
            userDb.RegistrationUpdate = DateTime.Now;            
   
            _context.Update(userDb);
            _context.SaveChanges();
            return userDb;

        }

        public bool PasswordUpdate(PasswordUpdateModel pwd)
        {
            UserModel pwdDb = BuscarPorId(pwd.Id);
            string result = pwd.PastPassword.HashGeneration();
			if (result == pwdDb.Password)
            {
                pwdDb.Password = pwd.NewPassword.HashGeneration();
				_context.Users.Update(pwdDb);
                _context.SaveChanges();
                return true;

            }
            return false;
        }
    }
}
