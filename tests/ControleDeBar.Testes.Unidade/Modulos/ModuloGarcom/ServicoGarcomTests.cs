using ControleDeBar.Aplicacao.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using FluentResults;
using Moq;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloGarcom;

[TestClass]
public sealed class ServicoGarcomTests
{
    [TestMethod]
    public void Cadastrar_ComTodosCampos_PersisteGarcom()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        repositorioGarcom.Setup(r => r.SelecionarTodos()).Returns([]);

        Garcom? garcomCadastrado = null;

        repositorioGarcom
            .Setup(r => r.Cadastrar(It.IsAny<Garcom>()))
            .Callback<Garcom>(
                garcom => garcomCadastrado = garcom
            );

        ServicoGarcom servicoGarcom = new ServicoGarcom(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        Result resultado = servicoGarcom.Cadastrar(new CadastrarGarcomDto(
            "João Silva"
        ));

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(garcomCadastrado);
        Assert.AreEqual("João Silva", garcomCadastrado.Nome);        

        repositorioGarcom.Verify(r => r.Cadastrar(It.IsAny<Garcom>()), Times.Once);
    }

    [TestMethod]
    public void Cadastrar_ComNomeVazio_RetornaErro()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        repositorioGarcom.Setup(r => r.SelecionarTodos()).Returns([]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        Result resultado = servicoGarcom.Cadastrar(new CadastrarGarcomDto(
            string.Empty
        ));

        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual("O campo \"Nome\" deve ser preenchido.", resultado.Errors.First().Message);

        repositorioGarcom.Verify(r => r.Cadastrar(It.IsAny<Garcom>()), Times.Never);
    }      

    [TestMethod]
    public void Cadastrar_NomeDuplicado_RetornaFalha()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        repositorioGarcom.Setup(r => r.SelecionarTodos())
        .Returns([new Garcom(
            "João Silva"
        )]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        Result resultado = servicoGarcom.Cadastrar(new CadastrarGarcomDto(
            "João Silva"
        ));

        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual("Nome", resultado.Errors.Single().Metadata["Campo"]);
        Assert.Contains("Já existe", resultado.Errors.Single().Message);

        repositorioGarcom.Verify(r => r.Cadastrar(It.IsAny<Garcom>()), Times.Never);
    }

    [TestMethod]
    public void Editar_ComDadosValidos_PersisteGarcom()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Garcom garcomExistente = new Garcom(
            "João Silva"
        );

        List<Garcom> garcom = new() { garcomExistente };

        repositorioGarcom.Setup(r => r.SelecionarTodos()).Returns(() => garcom);
        repositorioGarcom
            .Setup(r => r.Editar(It.IsAny<Guid>(), It.IsAny<Garcom>()))
            .Callback<Guid, Garcom>((id, garcomAtualizado) =>
            {
                garcomAtualizado.Id = id;
                int index = garcom.FindIndex(g => g.Id == id);
                if (index >= 0)
                    garcom[index].Atualizar(garcomAtualizado);
            })
            .Returns<Guid, Garcom>((id, garcomAtualizado) => garcom.Any(g => g.Id == id));

        ServicoGarcom servicoGarcom = new ServicoGarcom(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        Result resultado = servicoGarcom.Editar(new EditarGarcomDto(
            garcomExistente.Id,
            "José Alves"
        ));

        Assert.IsTrue(resultado.IsSuccess);
        repositorioGarcom.Verify(r => r.Editar(garcomExistente.Id, It.IsAny<Garcom>()), Times.Once);

        List<ListarGarconsDto> garconsListados = servicoGarcom.SelecionarTodos();

        Assert.HasCount(1, garconsListados);
        Assert.AreEqual("José Alves", garconsListados[0].Nome);
        
    }  
    

    [TestMethod]
    public void SelecionarPorId_RetornaGarcom()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Garcom garcomExistente = new Garcom(
            "João Silva"
        );

        repositorioGarcom
            .Setup(r => r.SelecionarPorId(garcomExistente.Id))
            .Returns(garcomExistente);

        ServicoGarcom servicoGarcom = new ServicoGarcom(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        Result<DetalhesGarcomDto> resultado = servicoGarcom.SelecionarPorId(garcomExistente.Id);

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(resultado.Value);
        Assert.AreEqual(garcomExistente.Id, resultado.Value.Id);
        Assert.AreEqual("João Silva", resultado.Value.Nome);        

    }

    [TestMethod]
    public void SelecionarTodos_RetornaGarconsCadastrados()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        List<Garcom> garcons = new()
        {
            new Garcom(
                "João Silva"
            ),
            new Garcom(
                "José Alves"
            )
        };

        repositorioGarcom.Setup(r => r.SelecionarTodos()).Returns(() => garcons);

        ServicoGarcom servicoGarcom = new ServicoGarcom(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        List<ListarGarconsDto> garconsListados = servicoGarcom.SelecionarTodos();

        Assert.HasCount(2, garconsListados);
        Assert.AreEqual("João Silva", garconsListados[0].Nome);  

        Assert.AreEqual("José Alves", garconsListados[1].Nome);        
    }

    [TestMethod]
    public void Excluir_SemContasVinculadas_ExcluiGarcom()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Garcom garcom = new Garcom(
            "João Silva"
        );

        repositorioGarcom
            .Setup(r => r.SelecionarPorId(garcom.Id))
            .Returns(garcom);
        repositorioConta
            .Setup(r => r.SelecionarTodos())
            .Returns(new List<Conta>());

        ServicoGarcom servicoGarcom = new ServicoGarcom(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        Result resultado = servicoGarcom.Excluir(garcom.Id);

        Assert.IsTrue(resultado.IsSuccess);
        repositorioGarcom.Verify(r => r.Excluir(garcom.Id), Times.Once);
    }

    [TestMethod]
    public void Excluir_Com_ContaEmAberto_RetornaFalha()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Garcom garcom = new Garcom(
            "João Silva"
        );
        repositorioGarcom
            .Setup(r => r.SelecionarPorId(garcom.Id))
            .Returns(garcom);

        repositorioConta
            .Setup(r => r.ExisteGarcomContaAberta(garcom.Id))
            .Returns(true);

        ServicoGarcom servicoGarcom = new ServicoGarcom(
            repositorioGarcom.Object,
            repositorioConta.Object
        );
        Result resultado = servicoGarcom.Excluir(garcom.Id);
        Assert.IsTrue(resultado.IsFailed);
        Assert.Contains("Não é possível excluir este garçom, pois ele possui contas vinculadas.", resultado.Errors.Single().Message);
        repositorioGarcom.Verify(r => r.Excluir(garcom.Id), Times.Never);
    }

    [TestMethod]
    public void Excluir_Com_ContaFechada_ExcluiGarcom()
    {
        Mock<IRepositorioGarcom> repositorioGarcom = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Garcom garcom = new Garcom(
            "João Silva"
        );

        repositorioGarcom
            .Setup(r => r.SelecionarPorId(garcom.Id))
            .Returns(garcom);
        repositorioConta
            .Setup(r => r.ExisteGarcomContaAberta(garcom.Id))
            .Returns(false);

        ServicoGarcom servicoGarcom = new ServicoGarcom(
            repositorioGarcom.Object,
            repositorioConta.Object
        );

        Result resultado = servicoGarcom.Excluir(garcom.Id);

        Assert.IsTrue(resultado.IsSuccess);
        repositorioGarcom.Verify(r => r.Excluir(garcom.Id), Times.Once);
    }
}