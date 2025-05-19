using Convoquei.Core.Genericos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Convoquei.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (IMutableEntityType entidade in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(EntidadeBase).IsAssignableFrom(entidade.ClrType))
                {
                    modelBuilder.Entity(entidade.ClrType).Property<DateTime>(nameof(EntidadeBase.DataCriacao))
                        .HasColumnName("data_criacao")
                        .HasDefaultValueSql("NOW()")
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                    modelBuilder.Entity(entidade.ClrType).Property<DateTime?>(nameof(EntidadeBase.DataAlteracao))
                        .HasColumnName("data_alteracao")
                        .ValueGeneratedOnUpdate()
                        .IsRequired(false);
                }
            }
        }
    }
}
