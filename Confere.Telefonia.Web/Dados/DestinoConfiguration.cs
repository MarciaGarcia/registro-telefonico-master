using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confere.Telefonia.Web.Dados
{
    internal class DestinoConfiguration : IEntityTypeConfiguration<Destino>
    {
        public void Configure(EntityTypeBuilder<Destino> builder)
        {
            builder.Property(d => d.Ativo).HasDefaultValue(true);
        }
    }
}