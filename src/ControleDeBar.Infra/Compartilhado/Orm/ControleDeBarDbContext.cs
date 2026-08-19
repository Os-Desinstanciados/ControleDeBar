using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloInstituicao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ControleDeBar.Infra.Compartilhado.Orm;

public sealed class ControleDeBarDbContext(
    DbContextOptions<ControleDeBarDbContext> options
    ) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Garcom> Garcons => Set<Garcom>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Mesa> Mesas => Set<Mesa>();
    public DbSet<Instituicao> Instituicoes => Set<Instituicao>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Assembly assembly = typeof(ControleDeBarDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}