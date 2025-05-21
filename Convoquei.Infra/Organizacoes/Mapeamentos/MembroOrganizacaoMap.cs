using Convoquei.Core.Organizacoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MembroOrganizacaoMap : IEntityTypeConfiguration<MembroOrganizacao>
{
    public void Configure(EntityTypeBuilder<MembroOrganizacao> builder)
    {
        builder.ToTable("membros_organizacao");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id");

        builder.Property(m => m.Cargo)
            .HasColumnName("cargo")
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(m => m.Usuario)
            .WithMany()
            .HasForeignKey("usuario_id")
            .IsRequired();

        builder.HasOne(m => m.Organizacao)
            .WithMany(o => o.Membros)
            .HasForeignKey("organizacao_id")
            .IsRequired();
    }
}
