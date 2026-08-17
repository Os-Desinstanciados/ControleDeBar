using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloCliente;
using FluentResults;


namespace ControleDeBar.Aplicacao.Modulos.ModuloCliente;

public class ServicoCliente : ServicoBase<Cliente>
{
    private readonly IRepositorioCliente repositorioCliente;

    public ServicoCliente(IRepositorioCliente repositorioCliente)
    {
        this.repositorioCliente = repositorioCliente;
    }
    public List<ListarClientesDto> SelecionarTodos()
    {
        return repositorioCliente
            .SelecionarTodos()
            .Select(c => new ListarClientesDto(c.Nome))
            .ToList();
    }
}
