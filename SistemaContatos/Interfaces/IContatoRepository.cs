using SistemaContatos.Models;

namespace SistemaContatos.Interfaces
{
    public interface IContatoRepository
    {
        ContatoModel Adicionar(ContatoModel contato);
        List<ContatoModel> BuscarTodos();
        ContatoModel BuscarPorId(Guid id);
        ContatoModel Editar(ContatoModel contato);
        bool Deletar(Guid id);




    }
}
