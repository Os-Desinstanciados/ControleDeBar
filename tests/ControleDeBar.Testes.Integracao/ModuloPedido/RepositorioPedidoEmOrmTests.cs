using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Infra.Modulos.ModuloPedido;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;

namespace ControleDeBar.Testes.Integracao.ModuloPedido;

[TestClass]
public sealed class RepositorioPedidoEmOrmTests : RepositorioEmOrmBaseTests
{
    private ControleDeBarDbContext dbContext = null!;
    private RepositorioPedidoEmOrm repositorio = null!;

    [TestInitialize]
    public void InicializarRepositorio()
    {
        dbContext = CriarDbContext();

        repositorio = new RepositorioPedidoEmOrm(dbContext);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaPedido()
    {
        Mesa mesa = new Mesa("1", "4");
        Garcom garcom = new Garcom("Junior Testes");
        Conta conta = new Conta(mesa, garcom);
        Produto produto = new Produto("Coca-Cola", 6);

        Pedido pedido = new Pedido(2, produto, conta);

        repositorio.Cadastrar(pedido);
        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado = repositorio.SelecionarPorId(pedido.Id);

        Assert.IsNotNull(pedidoSelecionado);
        Assert.AreEqual(2, pedidoSelecionado.Quantidade);
    }

    [TestMethod]
    public void Editar_AtualizaPedidoExistente()
    {
        Mesa mesa = new Mesa("1", "4");
        Garcom garcom = new Garcom("Junior Testes");
        Conta conta = new Conta(mesa, garcom);
        Produto produto = new Produto("Coca-Cola", 6);
        Pedido pedido = new Pedido(2, produto, conta);

        repositorio.Cadastrar(pedido);

        Pedido pedidoAtualizado = new Pedido(5, produto, conta);

        bool conseguiuEditar = repositorio.Editar(pedido.Id, pedidoAtualizado);
        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado = repositorio.SelecionarPorId(pedido.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(pedidoSelecionado);
        Assert.AreEqual(5, pedidoSelecionado.Quantidade);
    }

    [TestMethod]
    public void Excluir_RemovePedidoExistente()
    {
        Mesa mesa = new Mesa("1", "4");
        Garcom garcom = new Garcom("Junior Testes");
        Conta conta = new Conta(mesa, garcom);
        Produto produto = new Produto("Coca-Cola", 6);

        Pedido pedido = new Pedido(2, produto, conta);

        repositorio.Cadastrar(pedido);

        bool conseguiuExcluir = repositorio.Excluir(pedido.Id);
        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado = repositorio.SelecionarPorId(pedido.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(pedidoSelecionado);
    }

    [TestMethod]
    public void SelecionarTodos_RetornaPedidos()
    {
        Mesa mesa = new Mesa("1", "4");
        Garcom garcom = new Garcom("Junior Testes");
        Conta conta = new Conta(mesa, garcom);

        Produto produto1 = new Produto("Coca-Cola", 6);
        Produto produto2 = new Produto("Cerveja", 10);
        Produto produto3 = new Produto("Batata Frita", 15);

        Pedido pedido1 = new Pedido(1, produto1, conta);
        Pedido pedido2 = new Pedido(2, produto2, conta);
        Pedido pedido3 = new Pedido(3, produto3, conta);

        repositorio.Cadastrar(pedido1);
        repositorio.Cadastrar(pedido2);
        repositorio.Cadastrar(pedido3);

        dbContext.ChangeTracker.Clear();

        List<Pedido> pedidos = repositorio.SelecionarTodos();

        Assert.HasCount(3, pedidos);
    }
}