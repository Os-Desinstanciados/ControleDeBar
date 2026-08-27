using ControleDeBar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloConta;

[TestClass]
public sealed class ContaE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemContas()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "conta.listagem@teste.local",
            "Senha123!"
        );

        ContaListarPage listarPage = new(Page, UrlBase);

        // Act
        await listarPage.IrParaAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.Titulo).ToBeVisibleAsync();
        await Expect(listarPage.AbrirConta).ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveAbrir_Conta_ComMesaEGarcomValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "conta.cadastro@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync("1", "4");
        await CadastrarGarcomAsync("Junior Testes");

        ContaFormPage formPage = new(Page, UrlBase);
        ContaListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            "Mesa 1",
            "Junior Testes"
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);

        await Expect(listarPage.Mesa("1"))
            .ToBeVisibleAsync();

        await Expect(Page.GetByText("Aberta", new() { Exact = true }))
            .ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveFechar_Conta_Aberta()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "conta.fechar@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync("1", "4");
        await CadastrarGarcomAsync("Junior Testes");
        await AbrirContaAsync("Mesa 1", "Junior Testes");

        ContaListarPage listarPage = new(Page, UrlBase);

        await listarPage.DetalhesAsync("1");

        // Act
        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Fechar Conta" }
        ).ClickAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);

        await Expect(Page.GetByText(
            "Fechada",
            new() { Exact = true }
        )).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExcluir_Conta_Fechada()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "conta.exclusao@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync("1", "4");
        await CadastrarGarcomAsync("Junior Testes");
        await AbrirContaAsync("Mesa 1", "Junior Testes");

        ContaListarPage listarPage = new(Page, UrlBase);

        await listarPage.DetalhesAsync("1");

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Fechar Conta" }
        ).ClickAsync();

        await listarPage.DetalhesAsync("1");

        await Page.GetByRole(
            AriaRole.Link,
            new() { Name = "Excluir", Exact = true }
        ).ClickAsync();

        // Act
        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar Exclusão" }
        ).ClickAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);

        await Expect(listarPage.Mesa("1"))
            .Not.ToBeVisibleAsync();

        await Expect(listarPage.EstadoVazio)
            .ToBeVisibleAsync();
    }

    private async Task AbrirContaAsync(
        string mesa,
        string garcom
    )
    {
        ContaFormPage formPage = new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();
        await formPage.PreencherAsync(mesa, garcom);
        await formPage.ConfirmarAsync();
    }

    private async Task CadastrarMesaAsync(
    string numero,
    string lugares
)
    {
        await Page.GotoAsync($"{UrlBase}/Mesa/Cadastrar");

        await Page.GetByRole(
            AriaRole.Textbox,
            new() { Name = "Número", Exact = true })
            .FillAsync(numero);

        await Page.GetByRole(
            AriaRole.Textbox,
            new() { Name = "Número de Lugares", Exact = true })
            .FillAsync(lugares);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" })
            .ClickAsync();
    }

    private async Task CadastrarGarcomAsync(string nome)
    {
        await Page.GotoAsync($"{UrlBase}/Garcom/Cadastrar");

        await Page.GetByLabel("Nome")
            .FillAsync(nome);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" }
        ).ClickAsync();
    }
}