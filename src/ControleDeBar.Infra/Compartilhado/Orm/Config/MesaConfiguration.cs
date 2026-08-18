using ControleDeBar.Dominio.Modulos.ModuloMesa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Compartilhado.Orm.Config;

public sealed class MesaConfiguration : IEntityTypeConfiguration<Mesa>
{
    public void Configure(EntityTypeBuilder<Mesa> builder)
    {
        builder.ToTable("TBMesa");

        builder.HasKey(m => m.Id)
            .HasName("PK_TBMesa");

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.Numero)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(m => m.NumeroLugares)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(m => m.StatusMesa)
            .IsRequired();

        builder.HasIndex(m => m.Numero)
            .IsUnique()
            .HasDatabaseName("UQ_TBMesa_Numero");
    }
}