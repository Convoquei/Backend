using Convoquei.Core.Organizacoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ConviteOrganizacaoMap : IEntityTypeConfiguration<ConviteOrganizacao>
{
    public void Configure(EntityTypeBuilder<ConviteOrganizacao> builder)
    {
        builder.ToTable("convites_organizacao");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id");

        builder.Property(c => c.DataExpiracao)
            .HasColumnName("data_expiracao")
            .IsRequired();

        builder.Property(c => c.UltimoReenvio)
            .HasColumnName("ultimo_reenvio");

        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(e => e.Endereco)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(150);
        });

        builder.HasOne(c => c.Organizacao)
            .WithMany(o => o.Convites)
            .HasForeignKey("organizacao_id")
            .IsRequired();

        builder.HasOne(c => c.Convidador)
            .WithMany()
            .HasForeignKey("convidador_id")
            .IsRequired();
    }
}
