using Convoquei.Core.Eventos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.Eventos.Mapeamentos
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable("eventos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Local)
                .HasColumnName("local")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Descricao)
                .HasColumnName("descricao")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Tipo)
                .HasColumnName("tipo")
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.DataHoraInicio)
                .HasColumnName("data_hora_inicio")
                .IsRequired();

            builder.Property(e => e.FechamentoEscalaAntecedencia)
                .HasColumnName("fechamento_escala_antecedencia")
                .IsRequired();

            builder.HasOne(e => e.Criador)
                .WithMany() 
                .HasForeignKey("criador_id")
                .IsRequired();

            builder.HasOne(e => e.Organizacao)
                .WithMany(o => o.Eventos)
                .HasForeignKey("organizacao_id")
                .IsRequired();

            builder.HasOne(e => e.Recorrencia)
                .WithMany()
                .HasForeignKey("recorrencia_id");

            builder.OwnsOne(e => e.Cancelamento, cancelamento =>
            {
                cancelamento.Property(c => c.Data)
                    .HasColumnName("data_cancelamento")
                    .IsRequired();

                cancelamento.Property(c => c.Motivo)
                    .HasColumnName("motivo_cancelamento")
                    .HasMaxLength(300)
                    .IsRequired();

                cancelamento.HasOne(c => c.Usuario)
                    .WithMany()
                    .HasForeignKey("usuario_cancelamento_id")
                    .HasConstraintName("fk_usuario_cancelamento")
                    .IsRequired();
            });

            builder.Ignore(e => e.Cancelado);
        }
    }

}
