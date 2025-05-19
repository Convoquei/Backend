using Convoquei.Core.Assinaturas.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.Assinaturas.Mapeamentos
{
    public class AssinaturaMap : IEntityTypeConfiguration<Assinatura>
    {
        public void Configure(EntityTypeBuilder<Assinatura> builder)
        {
            builder.ToTable("assinaturas");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id");

            builder.Property(a => a.DataInicio)
                .HasColumnName("data_inicio")
                .IsRequired();

            builder.Property(a => a.DataFim)
                .HasColumnName("data_fim");

            builder.HasOne(a => a.Organizacao)
                .WithOne(o => o.Assinatura)
                .HasForeignKey<Assinatura>(a => a.Id)
                .IsRequired();

            builder.HasOne(a => a.Plano)
                .WithMany(p => p.Assinaturas)
                .HasForeignKey("plano_id")
                .IsRequired();

            builder.Ignore(a => a.Ativa);
        }

    }
}
