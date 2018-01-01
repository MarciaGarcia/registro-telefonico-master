using Microsoft.EntityFrameworkCore;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confere.Telefonia.Web.Dados
{
    internal class LigacaoConfiguration : IEntityTypeConfiguration<Ligacao>
    {
        public void Configure(EntityTypeBuilder<Ligacao> builder)
        {
            //builder.
        }
    }
}