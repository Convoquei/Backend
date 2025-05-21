using Convoquei.Core.Recorrencias.Entidades;
using Convoquei.Core.RecorrenciasEvento.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.RecorrenciasEvento.Mapeamentos
{
    public class RecorrenciaEventoBaseMap : IEntityTypeConfiguration<RecorrenciaEventoBase>
    {
        public void Configure(EntityTypeBuilder<RecorrenciaEventoBase> builder)
        {
            builder.ToTable("recorrencias_evento");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id");

            builder.Property(r => r.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Local)
                .HasColumnName("local")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(500);

            builder.Property(r => r.DataHoraInicio)
                .HasColumnName("data_hora_inicio")
                .IsRequired();

            builder.Property(r => r.FechamentoEscalaAntecedencia)
                .HasColumnName("fechamento_escala_antecedencia")
                .IsRequired();

            builder.Property(r => r.UltimaGeracao)
                .HasColumnName("data_ultima_geracao");

            builder.HasOne(r => r.Criador)
                .WithMany()
                .HasForeignKey("criador_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Organizacao)
                .WithMany(o => o.Recorrencias)
                .HasForeignKey("organizacao_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasDiscriminator<string>("tipo_recorrencia")
                .HasValue<RecorrenciaEventoSemanal>("semanal")
                .HasValue<RecorrenciaEventoPeriodico>("dias");

            builder.Ignore(c => c.Tipo);
            builder.HasIndex(r => r.UltimaGeracao);
        }
    }
}
