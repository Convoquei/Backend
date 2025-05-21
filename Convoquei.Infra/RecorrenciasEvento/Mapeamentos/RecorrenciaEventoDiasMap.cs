using Convoquei.Core.RecorrenciasEvento.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Convoquei.Infra.RecorrenciasEvento.Mapeamentos
{
    public class RecorrenciaEventoDiasMap : IEntityTypeConfiguration<RecorrenciaEventoDias>
    {
        public void Configure(EntityTypeBuilder<RecorrenciaEventoDias> builder)
        {
            builder.Property(r => r.IntervaloDias)
                .HasColumnName("intervalo_dias")
                .IsRequired();
        }
    }
}
