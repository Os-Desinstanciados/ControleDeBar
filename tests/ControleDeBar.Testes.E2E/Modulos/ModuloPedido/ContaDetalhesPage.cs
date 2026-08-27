using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloPedido;

public sealed class ContaDetalhesPage(IPage page)
{
    public ILocator Titulo => page.GetByRole(AriaRole.Main)
        .GetByRole(
        AriaRole.Heading,
        new() { Name = "Detalhes da Conta", Exact = true }
    );

    public ILocator EstadoSemPedidos => page.GetByText(
        "Nenhum pedido adicionado à conta.",
        new() { Exact = true }
    );

    public async Task SelecionarProdutoAsync(string produto)
    {
        await page.Locator("select[name='ProdutoId']")
            .SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = produto
                }
            );
    }

    public async Task InformarQuantidadeAsync(string quantidade)
    {
        await page.Locator("input[name='Quantidade']")
            .FillAsync(quantidade);
    }

    public async Task AdicionarPedidoAsync()
    {
        await page.GetByRole(
            AriaRole.Button,
            new() { Name = "Adicionar Pedido", Exact = true })
            .ClickAsync();
    }

    public ILocator ProdutoNaComanda(string nome)
    {
        return page.GetByText(
            nome,
            new() { Exact = true }
        );
    }
}