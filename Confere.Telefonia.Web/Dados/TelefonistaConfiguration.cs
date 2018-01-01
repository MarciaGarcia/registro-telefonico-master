using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confere.Telefonia.Web.Dados
{
    internal class TelefonistaConfiguration : IEntityTypeConfiguration<Telefonista>
    {
        public void Configure(EntityTypeBuilder<Telefonista> builder)
        {
            builder.Property(t => t.Ativo).HasDefaultValue(true);
        }
    }
}