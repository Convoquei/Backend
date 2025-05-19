using Convoquei.Core.Assinaturas.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PlanoMap : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder.ToTable("planos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LimiteMembros)
            .HasColumnName("limite_membros")
            .IsRequired();

        builder.Property(p => p.LimiteEventosMensais)
            .HasColumnName("limite_eventos_mensais")
            .IsRequired();

        builder.Property(p => p.Valor)
            .HasColumnName("valor")
            .HasColumnType("numeric(10,2)")
            .IsRequired();

        builder.Property(p => p.Tipo)
            .HasColumnName("tipo")
            .HasConversion<string>()
            .IsRequired();

        //builder.HasMany(p => p.Assinaturas)
        //    .WithOne(a => a.Plano)
        //    .HasForeignKey("plano_id")
        //    .IsRequired();
    }
}
