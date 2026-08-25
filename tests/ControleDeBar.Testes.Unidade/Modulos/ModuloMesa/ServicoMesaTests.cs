using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentResults;
using Moq;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloMesa;

[TestClass]
public sealed class ServicoMesaTests
{
    [TestMethod]
    public void Cadastrar_ComTodosCampos_PersisteMesa()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        repositorioMesa.Setup(r => r.SelecionarTodos()).Returns([]);

        Mesa? mesaCadastrada = null;

        repositorioMesa
            .Setup(r => r.Cadastrar(It.IsAny<Mesa>()))
            .Callback<Mesa>(
                mesa => mesaCadastrada = mesa
            );

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        Result resultado = servicoMesa.Cadastrar(new CadastrarMesaDto(
            "1",
            "2",
            StatusMesa.Livre
        ));

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(mesaCadastrada);
        Assert.AreEqual("1", mesaCadastrada.Numero);        
        Assert.AreEqual("2", mesaCadastrada.NumeroLugares);        

        repositorioMesa.Verify(r => r.Cadastrar(It.IsAny<Mesa>()), Times.Once);
    }

    [TestMethod]
    public void Cadastrar_ComNumeroVazio_RetornaErro()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        repositorioMesa.Setup(r => r.SelecionarTodos()).Returns([]);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        Result resultado = servicoMesa.Cadastrar(new CadastrarMesaDto(
            string.Empty,
            "2",
            StatusMesa.Livre
        ));

        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual("O campo \"Número\" deve ser preenchido.", resultado.Errors.First().Message);

        repositorioMesa.Verify(r => r.Cadastrar(It.IsAny<Mesa>()), Times.Never);
    }      

    [TestMethod]
    public void Cadastrar_NumeroDuplicado_RetornaFalha()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        repositorioMesa.Setup(r => r.SelecionarTodos())
        .Returns([new Mesa(
            "1",
            "2"
        )]);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        Result resultado = servicoMesa.Cadastrar(new CadastrarMesaDto(
            "1",
            "2",
            StatusMesa.Livre
        ));

        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual("Numero", resultado.Errors.Single().Metadata["Campo"]);
        Assert.Contains("Já existe", resultado.Errors.Single().Message);

        repositorioMesa.Verify(r => r.Cadastrar(It.IsAny<Mesa>()), Times.Never);
    }

    [TestMethod]
    public void Editar_ComDadosValidos_PersisteMesa()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Mesa mesaExistente = new Mesa(
            "1",
            "2"
        );

        List<Mesa> mesa = new() { mesaExistente };

        repositorioMesa.Setup(r => r.SelecionarTodos()).Returns(() => mesa);
        repositorioMesa
            .Setup(r => r.Editar(It.IsAny<Guid>(), It.IsAny<Mesa>()))
            .Callback<Guid, Mesa>((id, mesaAtualizado) =>
            {
                mesaAtualizado.Id = id;
                int index = mesa.FindIndex(g => g.Id == id);
                if (index >= 0)
                    mesa[index].Atualizar(mesaAtualizado);
            })
            .Returns<Guid, Mesa>((id, mesaAtualizado) => mesa.Any(g => g.Id == id));

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        Result resultado = servicoMesa.Editar(new EditarMesaDto(
            "2",
            "2",
            StatusMesa.Livre,
            mesaExistente.Id
           
        ));

        Assert.IsTrue(resultado.IsSuccess);
        repositorioMesa.Verify(r => r.Editar(mesaExistente.Id, It.IsAny<Mesa>()), Times.Once);

        List<ListarMesasDto> mesasListados = servicoMesa.SelecionarTodos();

        Assert.HasCount(1, mesasListados);
        Assert.AreEqual("2", mesasListados[0].Numero);
        
    }  
    

    [TestMethod]
    public void SelecionarPorId_RetornaMesa()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Mesa mesaExistente = new Mesa(
            "1",
            "2"
        );

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesaExistente.Id))
            .Returns(mesaExistente);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        Result<DetalhesMesaDto> resultado = servicoMesa.SelecionarPorId(mesaExistente.Id);

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(resultado.Value);
        Assert.AreEqual(mesaExistente.Id, resultado.Value.Id);
        Assert.AreEqual("1", resultado.Value.Numero);        

    }

    [TestMethod]
    public void SelecionarTodos_RetornaMesasCadastradas()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        List<Mesa> mesas = new()
        {
            new Mesa(
                "1",
                "2"
                
            ),
            new Mesa(
                "2",
                "2"
            )
        };

        repositorioMesa.Setup(r => r.SelecionarTodos()).Returns(() => mesas);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        List<ListarMesasDto> mesasListados = servicoMesa.SelecionarTodos();

        Assert.HasCount(2, mesasListados);
        Assert.AreEqual("1", mesasListados[0].Numero);  

        Assert.AreEqual("2", mesasListados[1].Numero);        
    }

    [TestMethod]
    public void Excluir_SemContasVinculadas_ExcluiMesa()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Mesa mesa = new Mesa(
            "1",
            "2"
        );

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesa.Id))
            .Returns(mesa);
        repositorioConta
            .Setup(r => r.SelecionarTodos())
            .Returns(new List<Conta>());

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        Result resultado = servicoMesa.Excluir(mesa.Id);

        Assert.IsTrue(resultado.IsSuccess);
        repositorioMesa.Verify(r => r.Excluir(mesa.Id), Times.Once);
    }

    [TestMethod]
    public void Excluir_Com_ContaEmAberto_RetornaFalha()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mesa mesa = new Mesa(
            "1",
            "2"
        );
        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesa.Id))
            .Returns(mesa);

        repositorioConta
            .Setup(r => r.ExisteMesaContaAberta(mesa.Id))
            .Returns(true);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );
        Result resultado = servicoMesa.Excluir(mesa.Id);
        Assert.IsTrue(resultado.IsFailed);
        Assert.Contains("Não é possível excluir esta mesa, pois ela possui contas vinculadas.", resultado.Errors.Single().Message);
        repositorioMesa.Verify(r => r.Excluir(mesa.Id), Times.Never);
    }

    [TestMethod]
    public void Excluir_Com_ContaFechada_ExcluiMesa()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();

        Mesa mesa = new Mesa(
            "1",
            "2"
        );

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesa.Id))
            .Returns(mesa);
        repositorioConta
            .Setup(r => r.ExisteMesaContaAberta(mesa.Id))
            .Returns(false);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        Result resultado = servicoMesa.Excluir(mesa.Id);

        Assert.IsTrue(resultado.IsSuccess);
        repositorioMesa.Verify(r => r.Excluir(mesa.Id), Times.Once);
    }
}