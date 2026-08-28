using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Infra.Modulos.ModuloConta;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;

namespace ControleDeBar.Testes.Integracao.ModuloConta;

[TestClass]
public sealed class RepositorioContaEmOrmTests : RepositorioEmOrmBaseTests
{
    private ControleDeBarDbContext dbContext = null!;
    private RepositorioContaEmOrm repositorio = null!;

    [TestInitialize]
    public void InicializarRepositorio()
    {
        dbContext = CriarDbContext();

        repositorio = new RepositorioContaEmOrm(dbContext);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaConta()
    {
        Mesa mesa = new Mesa("1", "4");
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(mesa, garcom);

        repositorio.Cadastrar(conta);
        dbContext.ChangeTracker.Clear();

        Conta? contaSelecionada = repositorio.SelecionarPorId(conta.Id);

        Assert.IsNotNull(contaSelecionada);
        Assert.AreEqual("1", contaSelecionada.Mesa.Numero);
        Assert.AreEqual("Junior Testes", contaSelecionada.Garcom.Nome);
        Assert.AreEqual(StatusConta.Aberta, contaSelecionada.Status);
    }

    [TestMethod]
    public void Editar_AtualizaContaExistente()
    {
        Mesa mesa = new Mesa("1", "4");
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(mesa, garcom);

        repositorio.Cadastrar(conta);

        conta.Fechar();

        bool conseguiuEditar = repositorio.Editar(conta.Id, conta);
        dbContext.ChangeTracker.Clear();

        Conta? contaSelecionada = repositorio.SelecionarPorId(conta.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(contaSelecionada);
        Assert.AreEqual(StatusConta.Fechada, contaSelecionada.Status);
        Assert.IsNotNull(contaSelecionada.DataFechamento);
    }

    [TestMethod]
    public void Excluir_RemoveContaExistente()
    {
        Mesa mesa = new Mesa("1", "4");
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(mesa, garcom);

        repositorio.Cadastrar(conta);

        bool conseguiuExcluir = repositorio.Excluir(conta.Id);
        dbContext.ChangeTracker.Clear();

        Conta? contaSelecionada = repositorio.SelecionarPorId(conta.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(contaSelecionada);
    }

    [TestMethod]
    public void SelecionarTodos_RetornaContas()
    {
        Mesa mesa1 = new Mesa("1", "4");
        Mesa mesa2 = new Mesa("2", "4");
        Mesa mesa3 = new Mesa("3", "4");

        Garcom garcom = new Garcom("Junior Testes");

        Conta conta1 = new Conta(mesa1, garcom);
        Conta conta2 = new Conta(mesa2, garcom);
        Conta conta3 = new Conta(mesa3, garcom);

        repositorio.Cadastrar(conta1);
        repositorio.Cadastrar(conta2);
        repositorio.Cadastrar(conta3);

        dbContext.ChangeTracker.Clear();

        List<Conta> contas = repositorio.SelecionarTodos();

        Assert.HasCount(3, contas);
    }
}