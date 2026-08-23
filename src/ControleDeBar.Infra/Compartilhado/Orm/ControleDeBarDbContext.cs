using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloInstituicao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Compartilhado.Identity;

namespace ControleDeBar.Infra.Compartilhado.Orm;

public sealed class ControleDeBarDbContext(
    DbContextOptions<ControleDeBarDbContext> options,
    IUserProvider? userProvider = null
    ) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Garcom> Garcons => Set<Garcom>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Mesa> Mesas => Set<Mesa>();
    public DbSet<Instituicao> Instituicoes => Set<Instituicao>();
    public DbSet<Conta> Contas => Set<Conta>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Guid? userId = userProvider?.Id;

        Assembly assembly = typeof(ControleDeBarDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);            

        modelBuilder.Entity<Cliente>()
            .HasQueryFilter(c => userProvider == null || c.UserId == userProvider.Id);

        modelBuilder.Entity<Garcom>()
            .HasQueryFilter(g => userProvider == null || g.UserId == userProvider.Id);

        modelBuilder.Entity<Mesa>()
            .HasQueryFilter(m => userProvider == null || m.UserId == userProvider.Id);

        modelBuilder.Entity<Produto>()
            .HasQueryFilter(p => userProvider == null || p.UserId == userProvider.Id);
        
    }

    public override int SaveChanges()
    {
        Guid? userId = userProvider?.Id;

        if (!userId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "Não é possível salvar entidades da instituição sem estar autenticado."
            );
        }

        foreach (var entry in ChangeTracker.Entries<IEntidadeUsuario>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.UserId == Guid.Empty)
                    {
                        entry.Property(nameof(IEntidadeUsuario.UserId)).CurrentValue = userId.Value;
                    }
                    else if (entry.Entity.UserId != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de criar entidade para outra instituição."
                        );
                    }

                    break;

                case EntityState.Modified:
                    Guid idOriginalInstituicao = entry
                        .Property(nameof(IEntidadeUsuario.UserId))
                        .OriginalValue is Guid idOriginal
                        ? idOriginal
                        : Guid.Empty;

                    Guid idAtualInstituicao = entry
                        .Property(nameof(IEntidadeUsuario.UserId))
                        .CurrentValue is Guid idAtual
                        ? idAtual
                        : Guid.Empty;

                    if (idOriginalInstituicao != idAtualInstituicao)
                    {
                        throw new UnauthorizedAccessException(
                              "Não é permitido alterar a instituição de uma entidade."
                          );
                    }

                    if (idAtualInstituicao != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de modificar entidade de outra instituição."
                        );
                    }

                    break;

                case EntityState.Deleted:
                    Guid instituicaoOriginal = entry
                        .Property(nameof(IEntidadeUsuario.UserId))
                        .OriginalValue is Guid original
                        ? original
                        : Guid.Empty;

                    if (instituicaoOriginal != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de excluir entidade de outra instituicao."
                        );
                    }

                    break;

            }
        }

        return base.SaveChanges();
    }
}