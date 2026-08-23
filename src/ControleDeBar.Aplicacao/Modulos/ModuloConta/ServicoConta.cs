using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloConta;

public class ServicoConta : ServicoBase<Conta>
{
    private readonly IRepositorioConta repositorioConta;
    private readonly IRepositorioMesa repositorioMesa;
    private readonly IRepositorioGarcom repositorioGarcom;

    public ServicoConta(
        IRepositorioConta repositorioConta,
        IRepositorioMesa repositorioMesa,
        IRepositorioGarcom repositorioGarcom
    )
    {
        this.repositorioConta = repositorioConta;
        this.repositorioMesa = repositorioMesa;
        this.repositorioGarcom = repositorioGarcom;
    }

    public Result Cadastrar(CadastrarContaDto dto)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(dto.MesaId);

        if (mesa == null)
            return Falha(nameof(dto.MesaId), "Mesa não encontrada.");

        if (ExisteContaAbertaParaMesa(dto.MesaId))
            return Falha(nameof(dto.MesaId), "Já existe uma conta aberta para esta mesa.");

        Garcom? garcom = repositorioGarcom.SelecionarPorId(dto.GarcomId);

        if (garcom == null)
            return Falha(nameof(dto.GarcomId), "Garçom não encontrado.");


        Conta novaConta = new Conta(
            mesa,
            garcom
        );

        Result resultadoValidacao = ValidarEntidade(novaConta);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;


        mesa.MesaEstaOcupada();
        repositorioMesa.Editar(mesa.Id, mesa);

        repositorioConta.Cadastrar(novaConta);
        return Result.Ok();

    }

    private bool ExisteContaAbertaParaMesa(Guid mesaId)
    {
        return repositorioConta
        .SelecionarTodos()
        .Any(c =>
            c.Mesa.Id == mesaId &&
            c.Status == StatusConta.Aberta
        );
    }

    public Result Excluir(Guid id)
    {
        Conta? conta = repositorioConta.SelecionarPorId(id);

        if (conta == null)
            return Falha(string.Empty, "Conta não encontrada.");

        if (conta.Status == StatusConta.Aberta)
            return Falha(string.Empty, "Uma conta aberta não pode ser excluída.");

        bool conseguiuExcluir = repositorioConta.Excluir(id);

        if (!conseguiuExcluir)
            return Falha(string.Empty, "Não foi possível excluir a conta.");

        return Result.Ok();
    }

    public Result Fechar(Guid id)
    {
        Conta? conta = repositorioConta.SelecionarPorId(id);

        if (conta == null)
            return Falha(string.Empty, "Conta não encontrada.");

        if (conta.Status == StatusConta.Fechada)
            return Falha(string.Empty, "A conta já está fechada.");

        conta.Fechar();

        conta.Mesa.MesaEstaLivre();

        bool conseguiuEditar = repositorioConta.Editar(id, conta);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Não foi possível fechar a conta.");

        repositorioMesa.Editar(conta.Mesa.Id, conta.Mesa);

        return Result.Ok();
    }

    public List<ListarContasDto> SelecionarTodos()
    {
        return repositorioConta
            .SelecionarTodos()
            .Select(c => new ListarContasDto(
                c.Id,
                c.Mesa,
                c.Garcom,
                c.DataAbertura,
                c.DataFechamento,
                c.Status
            ))
            .ToList();
    }

    public Result<DetalhesContaDto> SelecionarPorId(Guid id)
    {
        Conta? conta = repositorioConta.SelecionarPorId(id);


        if (conta == null)
            return Result.Fail("Conta não encontrada.");


        return Result.Ok(new DetalhesContaDto(
            conta.Id,
            conta.Mesa,
            conta.Garcom,
            conta.DataAbertura,
            conta.DataFechamento,
            conta.Status,
            conta.Pedidos
            )
        );
    }
}