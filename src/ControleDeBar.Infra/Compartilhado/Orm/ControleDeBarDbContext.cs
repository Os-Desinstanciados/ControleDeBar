using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Infra.Compartilhado.Orm;

public sealed class ControleDeBarDbContext(
    DbContextOptions<ControleDeBarDbContext> options
    ) : DbContext(options)
{
    public DbSet<Garcom> Garcons => Set<Garcom>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Mesa> Mesas => Set<Mesa>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly assembly = typeof(ControleDeBarDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}