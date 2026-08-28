using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Infra.Modulos.ModuloMesa;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;

namespace ControleDeBar.Testes.Integracao.ModuloMesa;

[TestClass]
public sealed class RepositorioMesaEmOrmTests : RepositorioEmOrmBaseTests
{
    private ControleDeBarDbContext dbContext = null!;
    private RepositorioMesaEmOrm repositorio = null!;

    [TestInitialize]
    public void InicializarRepositorio()
    {
        dbContext = CriarDbContext();

        repositorio = new RepositorioMesaEmOrm(dbContext);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaMesa()
    {
        Mesa mesa = new Mesa("1", "4");

        repositorio.Cadastrar(mesa);
        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionada = repositorio.SelecionarPorId(mesa.Id);

        Assert.IsNotNull(mesaSelecionada);
        Assert.AreEqual("1", mesaSelecionada.Numero);
        Assert.AreEqual("4", mesaSelecionada.NumeroLugares);
        Assert.AreEqual(StatusMesa.Livre, mesaSelecionada.StatusMesa);
    }

    [TestMethod]
    public void Editar_AtualizaMesaExistente()
    {
        Mesa mesa = new Mesa("1", "4");
        repositorio.Cadastrar(mesa);

        Mesa mesaAtualizada = new Mesa("2", "6");

        bool conseguiuEditar = repositorio.Editar(mesa.Id, mesaAtualizada);
        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionada = repositorio.SelecionarPorId(mesa.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(mesaSelecionada);
        Assert.AreEqual("2", mesaSelecionada.Numero);
        Assert.AreEqual("6", mesaSelecionada.NumeroLugares);
        Assert.AreEqual(StatusMesa.Livre, mesaSelecionada.StatusMesa);
    }

    [TestMethod]
    public void Excluir_RemoveMesaExistente()
    {
        Mesa mesa = new Mesa("1", "4");
        repositorio.Cadastrar(mesa);

        bool conseguiuExcluir = repositorio.Excluir(mesa.Id);
        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionada = repositorio.SelecionarPorId(mesa.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(mesaSelecionada);
    }

    [TestMethod]
    public void SelecionarTodos_RetornaMesas()
    {
        Mesa mesa1 = new Mesa("1", "4");
        Mesa mesa2 = new Mesa("2", "6");
        Mesa mesa3 = new Mesa("3", "8");

        repositorio.Cadastrar(mesa1);
        repositorio.Cadastrar(mesa2);
        repositorio.Cadastrar(mesa3);

        dbContext.ChangeTracker.Clear();

        List<Mesa> mesas = repositorio.SelecionarTodos();

        Assert.HasCount(3, mesas);
    }
}