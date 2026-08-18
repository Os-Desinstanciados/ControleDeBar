using ControleDeBar.Dominio.Modulos.ModuloCliente;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ControleDeBar.Infra.Compartilhado.Orm;

public sealed class ControleDeBarDbContext(
    DbContextOptions<ControleDeBarDbContext> options
    ) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly assembly = typeof(ControleDeBarDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}