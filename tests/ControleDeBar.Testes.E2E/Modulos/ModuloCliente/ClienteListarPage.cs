using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloCliente;

public sealed class ClienteListarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Cliente/Listar";

    public ILocator Titulo => page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Listagem de Clientes" }
    );

    public ILocator CadastrarNovo => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Cadastrar Novo" }
    );

    public ILocator EstadoVazio => page.GetByText(
        "Nenhum cliente cadastrado.",
        new() { Exact = true }
    );

    public ILocator NomeDoCliente(string nome) => page.GetByRole(
        AriaRole.Heading,
        new() { Name = nome, Exact = true }
    );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    private ILocator CardPorNome(string nome)
    {
        ILocator cliente = NomeDoCliente(nome);

        return page.Locator(".card").Filter(new() { Has = cliente });
    }

    public async Task EditarAsync(string nome)
    {
        await CardPorNome(nome)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Editar", Exact = true }
            )
            .ClickAsync();
    }

    public async Task ExcluirAsync(string nome)
    {
        await CardPorNome(nome)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Excluir", Exact = true }
            )
            .ClickAsync();
    }
}