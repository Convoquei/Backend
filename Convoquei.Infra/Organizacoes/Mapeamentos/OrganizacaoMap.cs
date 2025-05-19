using Convoquei.Core.Assinaturas.Entidades;
using Convoquei.Core.Organizacoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.Organizacoes.Mapeamentos
{
    public class OrganizacaoMap : IEntityTypeConfiguration<Organizacao>
    {
        public void Configure(EntityTypeBuilder<Organizacao> builder)
        {
            builder.ToTable("organizacoes");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.ExigirAprovacaoDisponibilidade)
                .HasColumnName("exigir_aprovacao_disponibilidade")
                .IsRequired();

            builder.HasOne(o => o.Assinatura)
                .WithOne(a => a.Organizacao)
                .HasForeignKey<Assinatura>(a => a.Id)
                .IsRequired();

            builder.HasMany(o => o.Convites)
                .WithOne(c => c.Organizacao)
                .HasForeignKey("organizacao_id") 
                .IsRequired();

            builder.HasMany(o => o.Membros)
                .WithOne(m => m.Organizacao)
                .HasForeignKey("organizacao_id")
                .IsRequired();

            builder.HasMany(o => o.Eventos)
                .WithOne(e => e.Organizacao)
                .HasForeignKey("organizacao_id")
                .IsRequired();
        }
    }
}
