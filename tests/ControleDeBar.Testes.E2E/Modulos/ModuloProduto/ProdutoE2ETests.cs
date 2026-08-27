
using ControleDeBar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloProduto;

[TestClass]
public sealed class ProdutoE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemProdutos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "produto.listagem@teste.local",
            "Senha123!"
        );

        ProdutoListarPage listarPage = new(Page, UrlBase);

        // Act
        await listarPage.IrParaAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.Titulo).ToBeVisibleAsync();
        await Expect(listarPage.CadastrarNovo).ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveCadastrar_Produto_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "produto.cadastro@teste.local",
            "Senha123!"
        );

        ProdutoFormPage formPage = new(Page, UrlBase);
        ProdutoListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();
        await formPage.PreencherAsync("Coca-Cola", "6");
        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.NomeDoProduto("Coca-Cola"))
            .ToBeVisibleAsync();

        await Expect(listarPage.EstadoVazio).Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveEditar_Produto_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "produto.edicao@teste.local",
            "Senha123!"
        );

        await CadastrarProdutoAsync("Coca-Cola", "6");

        ProdutoListarPage listarPage = new(Page, UrlBase);

        await listarPage.EditarAsync("Coca-Cola");

        // Act
        await Page.GetByLabel("Nome")
            .FillAsync("Coca-Cola Zero");

        await Page.GetByLabel("Preço")
            .FillAsync("8");

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" }
        ).ClickAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);

        await Expect(listarPage.NomeDoProduto("Coca-Cola Zero"))
            .ToBeVisibleAsync();

        await Expect(listarPage.NomeDoProduto("Coca-Cola"))
            .Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExcluir_Produto_SemVinculos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "produto.exclusao@teste.local",
            "Senha123!"
        );

        await CadastrarProdutoAsync("Coca-Cola", "6");

        ProdutoListarPage listarPage = new(Page, UrlBase);

        await listarPage.ExcluirAsync("Coca-Cola");

        // Act
        await Page.GetByRole(AriaRole.Button, new() { Name = "Excluir" }).ClickAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);

        await Expect(
            listarPage.NomeDoProduto("Coca-Cola"))
            .Not.ToBeVisibleAsync();

        await Expect(listarPage.EstadoVazio)
            .ToBeVisibleAsync();
    }

    private async Task CadastrarProdutoAsync(
        string nome,
        string preco
    )
    {
        await Page.GotoAsync($"{UrlBase}/Produto/Cadastrar");

        await Page.GetByLabel("Nome").FillAsync(nome);
        await Page.GetByLabel("Preço").FillAsync(preco);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" })
            .ClickAsync();

        await Expect(Page.GetByText(nome, new() { Exact = true }))
            .ToBeVisibleAsync();
    }
}