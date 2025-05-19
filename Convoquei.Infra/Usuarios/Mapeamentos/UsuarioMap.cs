using Convoquei.Core.Usuarios.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.Usuarios.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Endereco)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(150);
            });
        }
    }
}
