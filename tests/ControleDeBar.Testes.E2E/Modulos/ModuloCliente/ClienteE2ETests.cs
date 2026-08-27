using ControleDeBar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloCliente;

[TestClass]
public sealed class ClienteE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemClientes()
    {
        await RegistrarEEntrarAsync(
            "cliente.listagem@teste.local",
            "Senha123!"
        );

        ClienteListarPage listarPage = new(Page, UrlBase);

        await listarPage.IrParaAsync();

        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.Titulo).ToBeVisibleAsync();
        await Expect(listarPage.EstadoVazio).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveCadastrar_Cliente_ComDadosValidos()
    {
        await RegistrarEEntrarAsync(
            "cliente.cadastro@teste.local",
            "Senha123!"
        );

        ClienteFormPage formPage = new(Page, UrlBase);
        ClienteListarPage listarPage = new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();
        await formPage.PreencherNomeAsync("Junior Testes");
        await formPage.ConfirmarAsync();

        await Expect(Page).ToHaveURLAsync(listarPage.Url);
        await Expect(listarPage.NomeDoCliente("Junior Testes"))
            .ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveEditar_Cliente_ComDadosValidos()
    {
        await RegistrarEEntrarAsync(
            "cliente.edicao@teste.local",
            "Senha123!"
        );

        await CadastrarClienteAsync("Junior Testes");

        ClienteListarPage listarPage = new(Page, UrlBase);

        await listarPage.EditarAsync("Junior Testes");

        await Page.GetByLabel("Nome").FillAsync("João Testes");

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" }
        ).ClickAsync();

        await Expect(listarPage.NomeDoCliente("João Testes"))
            .ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExcluir_Cliente_SemVinculos()
    {
        await RegistrarEEntrarAsync(
            "cliente.exclusao@teste.local",
            "Senha123!"
        );

        await CadastrarClienteAsync("Junior Testes");

        ClienteListarPage listarPage = new(Page, UrlBase);

        await listarPage.ExcluirAsync("Junior Testes");

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" }
        ).ClickAsync();

        await Expect(listarPage.NomeDoCliente("Junior Testes"))
            .Not.ToBeVisibleAsync();
    }

    private async Task CadastrarClienteAsync(string nome)
    {
        await Page.GotoAsync($"{UrlBase}/Cliente/Cadastrar");

        await Page.GetByLabel("Nome").FillAsync(nome);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" }
        ).ClickAsync();
    }
}