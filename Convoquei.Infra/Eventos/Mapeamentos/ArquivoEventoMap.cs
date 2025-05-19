using Convoquei.Core.Eventos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ArquivoEventoMap : IEntityTypeConfiguration<ArquivoEvento>
{
    public void Configure(EntityTypeBuilder<ArquivoEvento> builder)
    {
        builder.ToTable("arquivos_evento");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(a => a.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.MimeType)
            .HasColumnName("mime_type")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.TamanhoEmBytes)
            .HasColumnName("tamanho_em_bytes")
            .IsRequired();

        builder.Property(a => a.ChaveStorage)
            .HasColumnName("chave_storage")
            .IsRequired()
            .HasMaxLength(250);

        // Relacionamento com Evento (N:1)
        builder.HasOne(a => a.Evento)
            .WithMany(e => e.Arquivos)
            .HasForeignKey("evento_id")
            .IsRequired();

        // Relacionamento com Usuario (N:1)
        builder.HasOne(a => a.Criador)
            .WithMany()
            .HasForeignKey("criador_id")
            .IsRequired();
    }
}
