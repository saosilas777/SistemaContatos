using SistemaContatos.Models;

namespace SistemaContatos.Interfaces
{
    public interface IUserRepository
    {
        UserModel BuscarPorLogin(string login);
        UserModel BuscarPorEmaileLogin(string login, string email);
        UserModel Adicionar(UserModel contato);
        List<UserModel> BuscarTodos();
        UserModel BuscarPorId(Guid id);
        UserModel Editar(UserModel contato);
        bool Deletar(Guid id);


    }
}

