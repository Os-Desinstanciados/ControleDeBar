using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Infra.Modulos.ModuloGarcom;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;

namespace ControleDeBar.Testes.Integracao.ModuloGarcom;

[TestClass]
public sealed class RepositorioGarcomEmOrmTests : RepositorioEmOrmBaseTests
{
    private ControleDeBarDbContext dbContext = null!;
    private RepositorioGarcomEmOrm repositorio = null!;

    [TestInitialize]
    public void InicializarRepositorio()
    {
        dbContext = CriarDbContext();

        repositorio = new RepositorioGarcomEmOrm(dbContext);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaGarcom()
    {
        Garcom garcom = new Garcom("Junior Testes");

        repositorio.Cadastrar(garcom);
        dbContext.ChangeTracker.Clear();

        Garcom? garcomSelecionado = repositorio.SelecionarPorId(garcom.Id);

        Assert.IsNotNull(garcomSelecionado);
        Assert.AreEqual("Junior Testes", garcomSelecionado.Nome);
    }

    [TestMethod]
    public void Editar_AtualizaGarcomExistente()
    {
        Garcom garcom = new Garcom("Junior Testes");
        repositorio.Cadastrar(garcom);

        Garcom garcomAtualizado = new Garcom("João Testes");

        bool conseguiuEditar = repositorio.Editar(garcom.Id, garcomAtualizado);
        dbContext.ChangeTracker.Clear();

        Garcom? garcomSelecionado = repositorio.SelecionarPorId(garcom.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(garcomSelecionado);
        Assert.AreEqual("João Testes", garcomSelecionado.Nome);
    }

    [TestMethod]
    public void Excluir_RemoveGarcomExistente()
    {
        Garcom garcom = new Garcom("Junior Testes");
        repositorio.Cadastrar(garcom);

        bool conseguiuExcluir = repositorio.Excluir(garcom.Id);
        dbContext.ChangeTracker.Clear();

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(repositorio.SelecionarPorId(garcom.Id));
    }

    [TestMethod]
    public void SelecionarTodos_RetornaGarcons()
    {
        Garcom garcom1 = new Garcom("Junior Testes");
        Garcom garcom2 = new Garcom("João Testes");
        Garcom garcom3 = new Garcom("Maria Testes");

        repositorio.Cadastrar(garcom1);
        repositorio.Cadastrar(garcom2);
        repositorio.Cadastrar(garcom3);

        dbContext.ChangeTracker.Clear();

        List<Garcom> garcons = repositorio.SelecionarTodos();

        Assert.HasCount(3, garcons);
    }
}