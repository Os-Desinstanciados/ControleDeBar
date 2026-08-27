using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloGarcom;

[TestClass]
public sealed class GarcomE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemGarcons()
    {
        // Arrange
        await RegistrarEEntrarAsync("garcom.listagem@teste.local", "Senha123!");
        GarcomListarPage listarPage = new(Page, UrlBase);        

        // Act
        await listarPage.IrParaAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);

        // Heading = h1, h2, h3, h4, h5, h6
        await Expect(listarPage.Nome).ToBeVisibleAsync();        
        await Expect(listarPage.EstadoVazio).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveCadastrar_Garcom_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync("garcom.cadastro@teste.local", "Senha123!");

        GarcomFormPage formPage = new(Page, UrlBase);
        GarcomListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();
        await formPage.PreencherNomeAsync("João Alves");
        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.NomeDoGarcom("João Alves")).ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveEditar_Garcom_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync("garcom.edicao@teste.local", "Senha123!");
        await CadastrarGarcomAsync("João Alves");

        ILocator card = Page.Locator(".card").Filter(new() { HasText = "João Alves" });
        await card.GetByRole(AriaRole.Link, new() { Name = "Editar", Exact = true }).ClickAsync();

        // Act
        await Page.GetByLabel("Nome").FillAsync("José Silva");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();

        // Assert
        Assert.AreEqual(
            "/Garcom/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(Page.GetByText("José Silva", new() { Exact = true }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByText("João Alves", new() { Exact = true }))
            .Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExcluir_Garcom_SemVinculos()
    {
        // Arrange
        await RegistrarEEntrarAsync("garcom.exclusao@teste.local", "Senha123!");
        await CadastrarGarcomAsync("João Alves");

        ILocator card = Page.Locator(".card").Filter(new() { HasText = "João Alves" });
        await card.GetByRole(AriaRole.Link, new() { Name = "Excluir", Exact = true }).ClickAsync();

        // Act
        await Expect(Page.GetByText("Deseja realmente excluir este garçom?", new() { Exact = true }))
            .ToBeVisibleAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        // Assert
        Assert.AreEqual(
            "/Garcom/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(Page.GetByText("João Alves", new() { Exact = true }))
            .Not.ToBeVisibleAsync();

        await Expect(Page.GetByText("Nenhum garçom cadastrado.", new() { Exact = true }))
            .ToBeVisibleAsync();
    }

    private async Task CadastrarGarcomAsync(string nome)
    {
        await Page.GotoAsync($"{UrlBase}/Garcom/Cadastrar");

        await Page.GetByLabel("Nome").FillAsync(nome);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();

        await Expect(Page.GetByText(nome, new() { Exact = true }))
            .ToBeVisibleAsync();
    }
}