using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confere.Telefonia.Web.Dados
{
    internal class SetorConfiguration : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {
            builder.Property(s => s.Ativo).HasDefaultValue(true);
        }
    }
}