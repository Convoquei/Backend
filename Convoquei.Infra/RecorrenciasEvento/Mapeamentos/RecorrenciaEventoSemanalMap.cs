using Convoquei.Core.RecorrenciasEvento.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.RecorrenciasEvento.Mapeamentos
{
    public class RecorrenciaEventoSemanalMap : IEntityTypeConfiguration<RecorrenciaEventoSemanal>
    {
        public void Configure(EntityTypeBuilder<RecorrenciaEventoSemanal> builder)
        {
            builder.Property(r => r.DiasRecorrenciaSemanaisFlag)
                .HasColumnName("dias_recorrencia_flag")
                .IsRequired();
        }
    }
}
