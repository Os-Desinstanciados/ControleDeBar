using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloProdutos;

[TestClass]
public sealed class PedidoTestes
{
    [TestMethod]
    public void Validar_ComQuantidadeZerada_DeveRetornarErro()
    {
        Produto produto = new Produto("Coca-Zero", 5);
        Mesa mesa = new Mesa("1", "5");
        Garcom garcom = new Garcom("Junior Tests");
        Conta conta = new Conta(mesa, garcom);

        Pedido pedido = new Pedido(
            0,
            produto,
            conta
        );

        List<string> erros = pedido.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "A quantidade do pedido deve ser maior que zero.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComProdutoVazio_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("1", "5");
        Garcom garcom = new Garcom("Junior Tests");
        Conta conta = new Conta(mesa, garcom);

        Pedido pedido = new Pedido();

        pedido.Quantidade = 1;
        pedido.Produto = null;
        pedido.Conta = conta;

        List<string> erros = pedido.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Produto\" deve ser preenchido.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComContaVazia_DeveRetornarErro()
    {
        Produto produto = new Produto("Coca-Zero", 7);
        Mesa mesa = new Mesa("1", "5");
        Garcom garcom = new Garcom("Junior Tests");
        Conta conta = new Conta(mesa, garcom);

        Pedido pedido = new Pedido();

        pedido.Quantidade = 1;
        pedido.Produto = produto;
        pedido.Conta = null;

        List<string> erros = pedido.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Conta\" deve ser preenchido.",
            erros.First()
        );
    }
}