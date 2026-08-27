using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloGarcom;

public sealed class GarcomListarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Garcom/Listar";

    public ILocator Nome => page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Listagem de Garçons" }
    );

    public ILocator CadastarNovo => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Cadastrar Novo" }
    );

    public ILocator EstadoVazio => page.GetByText(
        "Nenhum garçom cadastrado.",
        new() { Exact = true }
    );

    public ILocator NomeDoGarcom(string nome) => page.GetByRole(
        AriaRole.Heading,
        new() { Name = nome, Exact = true }
    );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task EditarAsync(string nome)
    {
        await CardPorNome(nome).GetByRole(
            AriaRole.Link,
            new() { Name = "Editar", Exact = true }
        ).ClickAsync();
    }

    public async Task ExcluirAsync(string nome)
    {
        await CardPorNome(nome).GetByRole(
            AriaRole.Link,
            new() { Name = "Excluir", Exact = true }
        ).ClickAsync();
    }

    private ILocator CardPorNome(string nome)
    {
        ILocator nomeGarcom = NomeDoGarcom(nome);

        return page.Locator(".card").Filter(new() { Has = nomeGarcom });
    }
}