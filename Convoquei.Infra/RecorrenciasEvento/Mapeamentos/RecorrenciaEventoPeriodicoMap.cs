using Convoquei.Core.RecorrenciasEvento.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.RecorrenciasEvento.Mapeamentos
{
    public class RecorrenciaEventoPeriodicoMap : IEntityTypeConfiguration<RecorrenciaEventoPeriodico>
    {
        public void Configure(EntityTypeBuilder<RecorrenciaEventoPeriodico> builder)
        {
            builder.Property(r => r.IntervaloDias)
                .HasColumnName("intervalo_dias")
                .IsRequired();
        }
    }
}
