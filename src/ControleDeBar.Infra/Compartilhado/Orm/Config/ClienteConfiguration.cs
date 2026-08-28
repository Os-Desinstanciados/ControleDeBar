using ControleDeBar.Dominio.Modulos.ModuloCliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Compartilhado.Orm.Config;

public sealed class ClienteCongiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("TBCliente");

        builder.HasKey(c => c.Id)
            .HasName("PK_TBCliente");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => new { c.UserId, c.Nome })
            .IsUnique()
            .HasDatabaseName("UQ_TBCliente_UserId_Nome");
    }
}