using ControleDeBar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloPedido;

[TestClass]
public sealed class PedidoE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ContaSemPedidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "pedido.vazio@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync("1", "4");
        await CadastrarGarcomAsync("Junior Testes");
        await AbrirContaAsync("Mesa 1", "Junior Testes");
        await AbrirDetalhesDaContaAsync("1");

        ContaDetalhesPage detalhesPage = new(Page);

        // Act / Assert
        await Expect(detalhesPage.Titulo)
            .ToBeVisibleAsync();

        await Expect(detalhesPage.EstadoSemPedidos)
            .ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveAdicionar_Pedido_ComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "pedido.cadastro@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync("1", "4");
        await CadastrarGarcomAsync("Junior Testes");
        await CadastrarProdutoAsync("Coca-Cola", "6");

        await AbrirContaAsync("Mesa 1", "Junior Testes");
        await AbrirDetalhesDaContaAsync("1");

        ContaDetalhesPage detalhesPage = new(Page);

        // Act
        await detalhesPage.SelecionarProdutoAsync(
            "Coca-Cola - R$ 6,00"
        );

        await detalhesPage.InformarQuantidadeAsync("2");

        await detalhesPage.AdicionarPedidoAsync();

        // Assert
        await Expect(
            detalhesPage.ProdutoNaComanda("Coca-Cola")
        ).ToBeVisibleAsync();

        await Expect(detalhesPage.EstadoSemPedidos)
            .Not.ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExibir_QuantidadeDoPedido_Adicionado()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "pedido.quantidade@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync("1", "4");
        await CadastrarGarcomAsync("Junior Testes");
        await CadastrarProdutoAsync("Coca-Cola", "6");

        await AbrirContaAsync("Mesa 1", "Junior Testes");
        await AbrirDetalhesDaContaAsync("1");

        ContaDetalhesPage detalhesPage = new(Page);

        // Act
        await detalhesPage.SelecionarProdutoAsync(
            "Coca-Cola - R$ 6,00"
        );

        await detalhesPage.InformarQuantidadeAsync("3");

        await detalhesPage.AdicionarPedidoAsync();

        // Assert
        await Expect(
            Page.GetByText("3 x R$ 6,00", new() { Exact = true }))
            .ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveExibir_TotalDaConta_AposAdicionarPedido()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "pedido.total@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync("1", "4");
        await CadastrarGarcomAsync("Junior Testes");
        await CadastrarProdutoAsync("Coca-Cola", "6");

        await AbrirContaAsync("Mesa 1", "Junior Testes");
        await AbrirDetalhesDaContaAsync("1");

        ContaDetalhesPage detalhesPage = new(Page);

        // Act
        await detalhesPage.SelecionarProdutoAsync(
            "Coca-Cola - R$ 6,00"
        );

        await detalhesPage.InformarQuantidadeAsync("2");

        await detalhesPage.AdicionarPedidoAsync();

        // Assert
        await Expect(
            Page.GetByText(
                "Total: R$ 12,00",
                new() { Exact = true }
            )
        ).ToBeVisibleAsync();
    }

    private async Task AbrirDetalhesDaContaAsync(string numeroMesa)
    {
        await Page.GotoAsync($"{UrlBase}/Conta/Listar");

        ILocator tituloMesa = Page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = $"Mesa {numeroMesa}",
                Exact = true
            }
        );

        ILocator card = Page.Locator(".card")
            .Filter(new() { Has = tituloMesa });

        await card.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Detalhes",
                Exact = true
            }
        ).ClickAsync();
    }

    private async Task AbrirContaAsync(string mesa, string garcom)
    {
        await Page.GotoAsync($"{UrlBase}/Conta/Cadastrar");

        await Page.GetByLabel("Mesa")
            .SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = mesa
                }
            );

        await Page.GetByLabel("Garçom")
            .SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = garcom
                }
            );

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Abrir Conta" })
            .ClickAsync();
    }

    private async Task CadastrarProdutoAsync(string nome, string preco)
    {
        await Page.GotoAsync($"{UrlBase}/Produto/Cadastrar");

        await Page.GetByLabel("Nome")
            .FillAsync(nome);

        await Page.GetByLabel("Preço")
            .FillAsync(preco);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" })
            .ClickAsync();
    }

    private async Task CadastrarGarcomAsync(
        string nome
    )
    {
        await Page.GotoAsync(
            $"{UrlBase}/Garcom/Cadastrar"
        );

        await Page.GetByLabel("Nome")
            .FillAsync(nome);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" })
            .ClickAsync();
    }

    private async Task CadastrarMesaAsync(string numero, string lugares)
    {
        await Page.GotoAsync(
            $"{UrlBase}/Mesa/Cadastrar"
        );

        await Page.GetByRole(
            AriaRole.Textbox,
            new()
            {
                Name = "Número",
                Exact = true
            }
        ).FillAsync(numero);

        await Page.GetByRole(
            AriaRole.Textbox,
            new()
            {
                Name = "Número de Lugares",
                Exact = true
            }
        ).FillAsync(lugares);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" })
            .ClickAsync();
    }
}