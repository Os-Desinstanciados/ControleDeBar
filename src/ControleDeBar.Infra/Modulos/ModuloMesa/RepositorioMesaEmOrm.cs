using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Infra.Modulos.ModuloMesa;

public sealed class RepositorioMesaEmOrm(ControleDeBarDbContext dbContext) :
    RepositorioBaseEmOrm<Mesa>(dbContext), IRepositorioMesa
{
}