using Microsoft.EntityFrameworkCore;
using SistemaContatos.Data;
using SistemaContatos.Interfaces;
using SistemaContatos.Models;
using SistemaContatos.Models.ViewModels;

namespace SistemaContatos.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly Context _context;

        public ContatoRepository(Context context)
        {
            _context = context;
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return contato;
        }
		public List<ContatoModel> AdicionarTodos(List<ContatoModel> contatos)
		{
			_context.AddRange(contatos);
			_context.SaveChanges();
			return contatos;
		}


		public ContatoModel BuscarPorId(Guid id)
        {
            return _context.Contato.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos(Guid id)
        {
            return _context.Contato.Where(x => x.UserId == id).ToList();
                
        }

        public bool Deletar(Guid id)
        {
            ContatoModel contatoDb = BuscarPorId(id);

            if (contatoDb == null) throw new Exception("Contato não encontrado!");
            _context.Remove(contatoDb);
            _context.SaveChanges();
            return true;
        }

        public ContatoModel Editar(ContatoModel contato)
        {
            ContatoModel contatoDb = BuscarPorId(contato.Id);

            if (contatoDb == null) throw new Exception("Houve um erro ao atualizar contato!");

            contatoDb.Name = contato.Name;
            contatoDb.Email = contato.Email;
            contatoDb.Phone = contato.Phone;
   
            _context.Update(contatoDb);
            _context.SaveChanges();
            return contatoDb;

        }
    }
}
