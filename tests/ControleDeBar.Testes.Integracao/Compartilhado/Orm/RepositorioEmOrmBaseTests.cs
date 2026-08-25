using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Testes.Integracao.Identity;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Testes.Integracao.Compartilhado.Orm;

public abstract class RepositorioEmOrmBaseTests
{
    protected ControleDeBarDbContext CriarDbContext()
    {
        DbContextOptions<ControleDeBarDbContext> options =
            new DbContextOptionsBuilder<ControleDeBarDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        return new ControleDeBarDbContext(
            options,
            new ProvedorDeUsuarioFake(Guid.NewGuid())
        );
    }
}