using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;

namespace ControleDeBar.Infra.Modulos.ModuloGarcom;

public sealed class RepositorioGarcomEmOrm(ControleDeBarDbContext dbContext) :
    RepositorioBaseEmOrm<Garcom>(dbContext), IRepositorioGarcom
{
}