using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
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
            .Include(c => c.Pedidos)
                .ThenInclude(p => p.Produto)
            .SingleOrDefault(c => c.Id == idSelecionado);
    }

    public Conta? SelecionarRegistroPorId(Guid contaId)
    {
        throw new NotImplementedException();
    }

    public bool ExisteGarcomContaAberta(Guid garcomId)
    {
        return registros.Any(c =>
            c.Garcom.Id ==  garcomId &&
            c.Status == StatusConta.Aberta
        );
    }

    public bool ExisteMesaContaAberta(Guid mesaId)
    {
        return registros.Any(c =>
            c.Mesa.Id == mesaId &&
            c.Status == StatusConta.Aberta
        );
    }
}