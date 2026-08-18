using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ControleDeBar.Infra.Compartilhado.Orm;

public sealed class ControleDeBarDbContext(
    DbContextOptions<ControleDeBarDbContext> options
    ) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly assembly = typeof(ControleDeBarDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}