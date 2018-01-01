using Confere.Telefonia.Web.Models;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Confere.Telefonia.Web.Dados
{
    public class TelefoniaContexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Telefonista> Telefonistas { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ligacao> Ligacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["telefonia"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new DestinoConfiguration());
            modelBuilder.ApplyConfiguration(new TelefonistaConfiguration());
            modelBuilder.ApplyConfiguration(new SetorConfiguration());
            modelBuilder.ApplyConfiguration(new FuncionarioConfiguration());
            modelBuilder.ApplyConfiguration(new LigacaoConfiguration());
        }
    }
}