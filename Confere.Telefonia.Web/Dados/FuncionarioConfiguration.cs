using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confere.Telefonia.Web.Dados
{
    internal class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.Property(f => f.Ativo).HasDefaultValue(true);
        }
    }
}