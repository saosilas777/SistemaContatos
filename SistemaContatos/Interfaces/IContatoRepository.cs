using SistemaContatos.Models;

namespace SistemaContatos.Interfaces
{
    public interface IContatoRepository
    {
        ContatoModel Adicionar(ContatoModel contato);
        List<ContatoModel> BuscarTodos(Guid id);
        ContatoModel BuscarPorId(Guid id);
        ContatoModel Editar(ContatoModel contato);
        bool Deletar(Guid id);
		List<ContatoModel> AdicionarTodos(List<ContatoModel> contatos);
	}
}
