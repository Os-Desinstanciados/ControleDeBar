using System.Text.RegularExpressions;
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
        await Expect(listarPage.NumeroLugares).ToBeVisibleAsync();
        await Expect(listarPage.CadastarNova).ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveCadastrar_Mesa_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync("mesa.cadastro@teste.local", "Senha123!");        

        MesaFormPage formPage = new(Page, UrlBase);
        MesaListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();
        await formPage.PreencherAsync("1", "2");
        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.NumeroDaMesa("1")).ToBeVisibleAsync();
        await Expect(listarPage.LugaresDaMesa("2")).ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveEditar_Mesa_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync("mesa.edicao@teste.local", "Senha123!");
        await CadastrarMesaAsync("1","2");

        MesaFormPage formPage = new(Page, UrlBase);
        MesaListarPage listarPage = new(Page, UrlBase);

        // Act
        await listarPage.EditarAsync("1");       
        await formPage.PreencherAsync("3", "4");
        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.NumeroDaMesa("3")).ToBeVisibleAsync();
        await Expect(listarPage.NumeroDaMesa("1")).Not.ToBeVisibleAsync();
        await Expect(listarPage.LugaresDaMesa("4")).ToBeVisibleAsync();
        await Expect(listarPage.LugaresDaMesa("2")).Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExcluir_Mesa_SemVinculos()
    {
        // Arrange
        await RegistrarEEntrarAsync("mesa.exclusao@teste.local", "Senha123!");
        await CadastrarMesaAsync("1", "2");

        MesaListarPage listarPage = new(Page, UrlBase);
        MesaExcluirPage excluirPage = new(Page);

        // Act
        await listarPage.ExcluirAsync("1");

        // Act
        await Expect(Page).ToHaveURLAsync(
            new Regex($"{Regex.Escape(UrlBase)}/Mesa/Excluir/.*")
        );

        await Expect(excluirPage.MensagemConfirmacao).ToBeVisibleAsync();

        await excluirPage.ConfirmarAsync();

        // Assert
        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.NumeroDaMesa("1")).Not.ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).ToBeVisibleAsync();
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