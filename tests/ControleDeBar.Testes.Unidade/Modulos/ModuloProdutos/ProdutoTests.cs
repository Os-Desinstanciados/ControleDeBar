using ControleDeBar.Dominio.Modulos.ModuloProduto;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloProdutos;

[TestClass]
public sealed class ProdutoTestes
{
    [TestMethod]
    public void Validar_ComNomeVazio_DeveRetornarErro()
    {
        Produto produto = new Produto(
            string.Empty,
            10
        );

        List<string> erros = produto.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve ser preenchido.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNomeLongo_DeveRetornarErro()
    {
        Produto produto = new Produto(
            new string('A', 101),
            10
        );

        List<string> erros = produto.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve conter entre 2 e 100 caracteres.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNomeCurto_DeveRetornarErro()
    {
        Produto produto = new Produto(
            new string('A', 1),
            10
        );

        List<string> erros = produto.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve conter entre 2 e 100 caracteres.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComPrecoZerado_DeveRetornarErro()
    {
        Produto produto = new Produto(
            "Coca-Zero",
            0
        );

        List<string> erros = produto.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Preço\" deve ser maior que zero.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComPrecoNegativo_DeveRetornarErro()
    {
        Produto produto = new Produto(
            "Coca-Zero",
            -1
        );

        List<string> erros = produto.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Preço\" deve ser maior que zero.",
            erros.First()
        );
    }
    
    [TestMethod]
    public void Validar_ProdutoValido_DeveRetornarValido()
    {
        Produto produto = new Produto(
            "Coca-Zero",
            8
        );

        List<string> erros = produto.Validar();

        Assert.HasCount(0, erros);
    }

}