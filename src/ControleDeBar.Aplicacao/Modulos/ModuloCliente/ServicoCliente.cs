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

    public Result Cadastrar(CadastrarClienteDto dto)
    {
        if (ExisteClienteComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe um cliente com este nome.");

        Cliente novoCliente = new Cliente(
            dto.Nome
        );

        Result resultadoValidacao = ValidarEntidade(novoCliente);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCliente.Cadastrar(novoCliente);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Cliente? cliente = repositorioCliente.SelecionarPorId(id);

        if (cliente == null)
            return Falha(string.Empty, "Cliente não encontrado.");

        repositorioCliente.Excluir(id);

        return Result.Ok();
    }

    private bool ExisteClienteComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        string nomeNormalizado = NormalizarNome(nome);

        return repositorioCliente
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                NormalizarNome(c.Nome) == nomeNormalizado);
    }

    private string NormalizarNome(string nome)
    {
        return nome.Trim().ToLower();
    }

    public List<ListarClientesDto> SelecionarTodos()
    {
        return repositorioCliente
            .SelecionarTodos()
            .Select(c => new ListarClientesDto(c.Nome))
            .ToList();
    }
}
