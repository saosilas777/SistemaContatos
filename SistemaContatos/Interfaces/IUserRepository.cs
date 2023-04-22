using SistemaContatos.Models;

namespace SistemaContatos.Interfaces
{
    public interface IUserRepository
    {
        UserModel Adicionar(UserModel contato);
        List<UserModel> BuscarTodos();
        UserModel BuscarPorId(Guid id);
        UserModel Editar(UserModel contato);
        bool Deletar(Guid id);
    }
}

