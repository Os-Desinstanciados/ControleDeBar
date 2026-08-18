using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Infra.Compartilhado.Orm;

public sealed class RepositorioClienteEmOrm(ControleDeBarDbContext dbContext) : 
    RepositorioBaseEmOrm<Cliente>(dbContext), IRepositorioCliente
{
    public override List<Cliente> SelecionarTodos()
    {
        return registros.OrderBy(c => c.Nome).ToList();
    }

    public override List<Cliente> Filtrar(Func<Cliente, bool> filtro)
    {
        return registros.Where(filtro).OrderBy(c => c.Nome).ToList();
    }
}