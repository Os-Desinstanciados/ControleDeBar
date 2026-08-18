using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloMesa;

public class ServicoMesa : ServicoBase<Mesa>
{
    private readonly IRepositorioMesa repositorioMesa;

    public ServicoMesa(IRepositorioMesa repositorioMesa)
    {
        this.repositorioMesa = repositorioMesa;
    }

    public Result Cadastrar(CadastrarMesaDto dto)
    {
        if (ExisteMesaComMesmoNumero(dto.Numero))
            return Falha(nameof(dto.Numero), "Já existe uma mesa com este número.");

        Mesa novaMesa = new Mesa(dto.Numero, dto.NumeroLugares);        

        novaMesa = new Mesa(dto.Numero, dto.NumeroLugares);

        Result resultadoValidacao = ValidarEntidade(novaMesa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMesa.Cadastrar(novaMesa);

        return Result.Ok();
    }

    public Result Editar(EditarMesaDto dto)
    {
        if (ExisteMesaComMesmoNumero(dto.Numero, dto.Id))
            return Falha(nameof(dto.Numero), "Já existe uma mesa com este número.");

        Mesa mesaAtualizada = new Mesa(dto.Numero, dto.NumeroLugares);

        if (dto.StatusMesa == StatusMesa.Ocupada)
            mesaAtualizada.MesaEstaOcupada();

        Result resultadoValidacao = ValidarEntidade(mesaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioMesa.Editar(dto.Id, mesaAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Mesa não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(id);

        if (mesa == null)
            return Falha(string.Empty, "Mesa não encontrada.");

        repositorioMesa.Excluir(id);

        return Result.Ok();
    }

    public List<ListarMesasDto> SelecionarTodos()
    {
        return repositorioMesa
            .SelecionarTodos()
            .Select(m => new ListarMesasDto(
                m.Numero,
                m.NumeroLugares,
                m.StatusMesa,
                m.Id
            ))
            .ToList();
    }

    public Result<DetalhesMesaDto> SelecionarPorId(Guid id)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(id);

        if (mesa == null)
            return Result.Fail("Mesa não encontrada.");

        return Result.Ok(new DetalhesMesaDto(           
            mesa.Numero,
            mesa.NumeroLugares,
            mesa.StatusMesa,
            mesa.Id
        ));
    }

    private bool ExisteMesaComMesmoNumero(string numero, Guid? idIgnorado = null)
    {
        string identificacaoNormalizada = NormalizarIdentificacao(numero);

        return repositorioMesa
            .SelecionarTodos()
            .Any(m =>
                m.Id != idIgnorado &&
                NormalizarIdentificacao(m.Numero) == identificacaoNormalizada
            );
    }

    private static string NormalizarIdentificacao(string numero)
    {
        return numero.Trim().ToLowerInvariant();
    }
}