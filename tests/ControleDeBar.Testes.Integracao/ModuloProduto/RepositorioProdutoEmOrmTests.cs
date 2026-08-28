using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;

namespace ControleDeBar.Testes.Integracao.ModuloProduto;

[TestClass]
public sealed class RepositorioProdutoEmOrmTests : RepositorioEmOrmBaseTests
{
    private ControleDeBarDbContext dbContext = null!;
    private RepositorioProdutoEmOrm repositorio = null!;

    [TestInitialize]
    public void InicializarRepositorio()
    {
        dbContext = CriarDbContext();

        repositorio = new RepositorioProdutoEmOrm(dbContext);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaProduto()
    {
        Produto produto = new Produto("Coca-Cola", 6);

        repositorio.Cadastrar(produto);
        dbContext.ChangeTracker.Clear();

        Produto? produtoSelecionado = repositorio.SelecionarPorId(produto.Id);

        Assert.IsNotNull(produtoSelecionado);
        Assert.AreEqual("Coca-Cola", produtoSelecionado.Nome);
        Assert.AreEqual(6, produtoSelecionado.Preco);
    }

    [TestMethod]
    public void Editar_AtualizaProdutoExistente()
    {
        Produto produto = new Produto("Coca-Cola", 6);
        repositorio.Cadastrar(produto);

        Produto produtoAtualizado = new Produto("Coca-Cola Zero", 8);

        bool conseguiuEditar = repositorio.Editar(produto.Id, produtoAtualizado);
        dbContext.ChangeTracker.Clear();

        Produto? produtoSelecionado = repositorio.SelecionarPorId(produto.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(produtoSelecionado);
        Assert.AreEqual("Coca-Cola Zero", produtoSelecionado.Nome);
        Assert.AreEqual(8, produtoSelecionado.Preco);
    }

    [TestMethod]
    public void Excluir_RemoveProdutoExistente()
    {
        Produto produto = new Produto("Coca-Cola", 6);
        repositorio.Cadastrar(produto);

        bool conseguiuExcluir = repositorio.Excluir(produto.Id);
        dbContext.ChangeTracker.Clear();

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(repositorio.SelecionarPorId(produto.Id));
    }

    [TestMethod]
    public void SelecionarTodos_RetornaProdutos()
    {
        Produto produto1 = new Produto("Coca-Cola", 6);
        Produto produto2 = new Produto("Cerveja", 10);
        Produto produto3 = new Produto("Batata Frita", 15);

        repositorio.Cadastrar(produto1);
        repositorio.Cadastrar(produto2);
        repositorio.Cadastrar(produto3);

        dbContext.ChangeTracker.Clear();

        List<Produto> produtos = repositorio.SelecionarTodos();

        Assert.HasCount(3, produtos);
    }
}