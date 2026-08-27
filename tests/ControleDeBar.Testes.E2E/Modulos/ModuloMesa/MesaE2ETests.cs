using ControleDeBar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloMesa;

[TestClass]
public sealed class MesaE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemMesas()
    {
        // Arrange
        await RegistrarEEntrarAsync("mesa.listagem@teste.local", "Senha123!");
        MesaListarPage listarPage = new(Page, UrlBase);

        // Act
        await listarPage.IrParaAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);

        // Heading = h1, h2, h3, h4, h5, h6
        await Expect(listarPage.Numero).ToBeVisibleAsync();
        await Expect(listarPage.CadastarNova).ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveCadastrar_Mesa_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync("mesa.cadastro@teste.local", "Senha123!");

        await Page.GotoAsync($"{UrlBase}/Mesa/Listar");

        // Act
        await Page.GetByRole(AriaRole.Link, new() { Name = "Cadastrar Nova" })
            .ClickAsync();

        await Page.GetByLabel("Número", new() { Exact = true }).FillAsync("1");
        await Page.GetByLabel("Número de Lugares").FillAsync("2");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();

        // Assert
        Assert.AreEqual(
            "/Mesa/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(Page.GetByText("1", new() { Exact = true }))
            .ToBeVisibleAsync();
        
        await Expect(Page.GetByText("2", new() { Exact = true }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByText("Nenhuma mesa cadastrada.", new() { Exact = true }))
            .Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveEditar_Mesa_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync("mesa.edicao@teste.local", "Senha123!");
        await CadastrarMesaAsync("1","2");

        ILocator card = Page.Locator(".card").Filter(new() { HasText = "1" });
        await card.GetByRole(AriaRole.Link, new() { Name = "Editar", Exact = true }).ClickAsync();

        // Act
        await Page.GetByLabel("Número", new() { Exact = true }).FillAsync("3");
        await Page.GetByLabel("Número de Lugares").FillAsync("4");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();

        // Assert
        Assert.AreEqual(
            "/Mesa/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(Page.GetByText("3", new() { Exact = true }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByText("4", new() { Exact = true }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByText("Nenhuma mesa cadastrada.", new() { Exact = true }))
            .Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExcluir_Mesa_SemVinculos()
    {
        // Arrange
        await RegistrarEEntrarAsync("mesa.exclusao@teste.local", "Senha123!");
        await CadastrarMesaAsync("1", "2");

        ILocator card = Page.Locator(".card").Filter(new() { HasText = "1" });
        await card.GetByRole(AriaRole.Link, new() { Name = "Excluir", Exact = true }).ClickAsync();

        // Act
        await Expect(Page.GetByText("Deseja realmente excluir esta mesa?", new() { Exact = true }))
            .ToBeVisibleAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        // Assert
        Assert.AreEqual(
            "/Mesa/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(Page.GetByText("1", new() { Exact = true }))
            .Not.ToBeVisibleAsync();

        await Expect(Page.GetByText("Nenhuma mesa cadastrada.", new() { Exact = true }))
            .ToBeVisibleAsync();
    }

    private async Task CadastrarMesaAsync(string numero, string numeroLugares)
    {
        await Page.GotoAsync($"{UrlBase}/Mesa/Cadastrar");

        await Page.GetByLabel("Número", new() { Exact = true }).FillAsync(numero);
        await Page.GetByLabel("Número de Lugares").FillAsync(numeroLugares);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();

        await Expect(Page.GetByText(numero, new() { Exact = true }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByText(numeroLugares, new() { Exact = true }))
            .ToBeVisibleAsync();
    }
}