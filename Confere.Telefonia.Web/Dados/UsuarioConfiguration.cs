using Confere.Telefonia.Web.Models;
using Confere.Telefonia.Web.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confere.Telefonia.Web.Dados
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.TextoPapeis).HasColumnName("Papeis");
            builder.Ignore(u => u.Papeis);
            builder.HasAlternateKey(u => u.Login);
            builder.HasAlternateKey(u => u.Email);
            builder.Property(u => u.Ativo).HasDefaultValue(true);
        }
    }
}