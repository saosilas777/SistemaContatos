using Microsoft.EntityFrameworkCore;
using SistemaContatos.Models;

namespace SistemaContatos.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<ContatoModel> Contato { get; set; }

    }
}
