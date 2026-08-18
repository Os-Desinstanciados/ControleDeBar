using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Compartilhado.Orm;

public sealed class RepositorioProdutoEmOrm(ControleDeBarDbContext dbContext) :
    RepositorioBaseEmOrm<Produto>(dbContext), IRepositorioProduto
{
    public override List<Produto> SelecionarTodos()
    {
        return registros.OrderBy(p => p.Nome).ToList();
    }

    public override List<Produto> Filtrar(Func<Produto, bool> filtro)
    {
        return registros.Where(filtro).OrderBy(p => p.Nome).ToList();
    }
}