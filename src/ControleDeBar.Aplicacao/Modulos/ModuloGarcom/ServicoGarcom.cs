using FluentResults;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Aplicacao.Compartilhado;


namespace ControleDeBar.Aplicacao.Modulos.ModuloGarcom;

public class ServicoGarcom : ServicoBase<Garcom>
{
    private readonly IRepositorioGarcom repositorioGarcom;
    

    public ServicoGarcom(
        IRepositorioGarcom repositorioGarcom
        
    )
    {
        this.repositorioGarcom = repositorioGarcom;        
    }

    public Result Cadastrar(CadastrarGarcomDto dto)
    {
        if (ExisteGarcomComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe um garçom com este nome.");

        Garcom novoGarcom = new Garcom(dto.Nome);

        Result resultadoValidacao = ValidarEntidade(novoGarcom);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioGarcom.Cadastrar(novoGarcom);

        return Result.Ok();
    }

    public Result Editar(EditarGarcomDto dto)
    {
        if (ExisteGarcomComMesmoNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe um garçom com este título.");

        Garcom garcomAtualizado = new Garcom(dto.Nome);

        Result resultadoValidacao = ValidarEntidade(garcomAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioGarcom.Editar(dto.Id, garcomAtualizado);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Garçom não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Garcom? garcom = repositorioGarcom.SelecionarPorId(id);

        if (garcom == null)
            return Falha(string.Empty, "Garçom não encontrado.");

        //if (PossuiContasVinculadas(id))
            //return Falha(string.Empty, "Não é possível excluir este garçom, pois ele possui contas vinculadas.");

        repositorioGarcom.Excluir(id);

        return Result.Ok();
    }

    public List<ListarGarconsDto> SelecionarTodos()
    {
        return repositorioGarcom
            .SelecionarTodos()
            .Select(g => new ListarGarconsDto(g.Id, g.Nome))
            .ToList();
    }

    public Result<DetalhesGarcomDto> SelecionarPorId(Guid id)
    {
        Garcom? garcom = repositorioGarcom.SelecionarPorId(id);

        if (garcom == null)
            return Result.Fail("Garçom não encontrado.");

        return Result.Ok(new DetalhesGarcomDto(garcom.Id, garcom.Nome));
    }

    private bool ExisteGarcomComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        string nomeNormalizado = NormalizarNome(nome);

        return repositorioGarcom
            .SelecionarTodos()
            .Any(g =>
                g.Id != idIgnorado &&
                NormalizarNome(g.Nome) == nomeNormalizado
            );
    }

    //private bool PossuiContasVinculadas(Guid garcomId)
    //{
        //return repositorioConta
          //  .SelecionarTodos()
            //.Any(d => g.Garcons.Any(c => g.Id == garcomId));
    //}

    private static string NormalizarNome(string nome)
    {
        return nome.Trim().ToLowerInvariant();
    }
}