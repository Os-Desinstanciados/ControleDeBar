using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Infra.Modulos.ModuloConta;

public sealed class RepositorioContaEmOrm(ControleDeBarDbContext dbContext) :
    RepositorioBaseEmOrm<Conta>(dbContext), IRepositorioConta
{
    public override List<Conta> SelecionarTodos()
    {
        return registros
            .Include(c => c.Mesa)
            .Include(c => c.Garcom)
            .ToList();
    }

    public override Conta? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(c => c.Mesa)
            .Include(c => c.Garcom)
            .SingleOrDefault(c => c.Id == idSelecionado);
    }

    public Conta? SelecionarRegistroPorId(Guid contaId)
    {
        throw new NotImplementedException();
    }
}