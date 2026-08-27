using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloMesa;

public sealed class MesaListarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Mesa/Listar";

    public ILocator Numero => page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Listagem de Mesas" }
    );
    
    public ILocator NumeroLugares => page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Listagem de Mesas" }
    );

    public ILocator CadastarNova => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Cadastrar Nova" }
    );

    public ILocator EstadoVazio => page.GetByText(
        "Nenhuma mesa cadastrada.",
        new() { Exact = true }
    );

    public ILocator NumeroDaMesa(string numero) => page.GetByRole(
        AriaRole.Heading,
        new() { Name = numero, Exact = true }
    );

    public ILocator LugaresDaMesa(string numeroLugares) => page.GetByRole(
        AriaRole.Heading,
        new() { Name = numeroLugares, Exact = true }
    );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task EditarAsync(string numero)
    {
        await CardPorNumero(numero).GetByRole(
            AriaRole.Link,
            new() { Name = "Editar", Exact = true }
        ).ClickAsync();
        
    }

    public async Task ExcluirAsync(string numero)
    {
        await CardPorNumero(numero).GetByRole(
            AriaRole.Link,
            new() { Name = "Excluir", Exact = true }
        ).ClickAsync();
    }

    private ILocator CardPorNumero(string numero)
    {
        ILocator numeroMesa = NumeroDaMesa(numero);

        return page.Locator(".card").Filter(new() { Has = numeroMesa });
    }
    
}