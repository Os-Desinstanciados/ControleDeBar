using ControleDeBar.Dominio.Modulos.ModuloConta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Compartilhado.Orm.Config;

public sealed class ContaConfiguration : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.ToTable("TBConta");

        builder.HasKey(c => c.Id)
            .HasName("PK_TBConta");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.DataAbertura)
            .IsRequired();

        builder.Property(c => c.DataFechamento)
            .IsRequired(false);

        builder.Property(c => c.Status)
            .IsRequired();

        builder.HasOne(c => c.Mesa)
            .WithMany()
            .IsRequired();

        builder.HasOne(c => c.Garcom)
            .WithMany()
            .IsRequired();
    }
}