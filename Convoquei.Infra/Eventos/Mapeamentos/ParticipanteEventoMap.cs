using Convoquei.Core.Eventos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ParticipanteEventoMap : IEntityTypeConfiguration<ParticipanteEvento>
{
    public void Configure(EntityTypeBuilder<ParticipanteEvento> builder)
    {
        builder.ToTable("participantes_evento");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.StatusParticipacao)
            .HasColumnName("status_participacao")
            .HasConversion<string>() 
            .IsRequired();

        builder.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey("usuario_id")
            .IsRequired();

        builder.HasOne(p => p.Evento)
            .WithMany(e => e.Participantes)
            .HasForeignKey("evento_id")
            .IsRequired();
    }
}
