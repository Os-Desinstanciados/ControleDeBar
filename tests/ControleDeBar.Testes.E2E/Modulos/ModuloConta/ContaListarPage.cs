using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloConta;

public sealed class ContaListarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Conta/Listar";


    public ILocator Titulo => page.Locator("h1")
       .Filter(new() { HasTextString = "Contas" });
       
    public ILocator AbrirConta => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Abrir Conta", Exact = true }
    );

    public ILocator EstadoVazio => page.GetByText(
        "Nenhuma conta cadastrada.",
        new() { Exact = true }
    );

    public ILocator Mesa(string numero) => page.GetByRole(
        AriaRole.Heading,
        new() { Name = $"Mesa {numero}", Exact = true }
    );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task DetalhesAsync(string numeroMesa)
    {
        await CardPorMesa(numeroMesa)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Detalhes", Exact = true }
            )
            .ClickAsync();
    }

    private ILocator CardPorMesa(string numeroMesa)
    {
        ILocator mesa = Mesa(numeroMesa);

        return page.Locator(".card")
            .Filter(new() { Has = mesa });
    }
}