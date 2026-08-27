using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloConta;

public sealed class ContaFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Conta/Cadastrar";

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task PreencherAsync(
        string mesa,
        string garcom
    )
    {
        await page.GetByLabel("Mesa")
            .SelectOptionAsync(new SelectOptionValue
            {
                Label = mesa
            });

        await page.GetByLabel("Garçom")
            .SelectOptionAsync(new SelectOptionValue
            {
                Label = garcom
            });
    }

    public async Task ConfirmarAsync()
    {
        await page.GetByRole(
            AriaRole.Button,
            new() { Name = "Abrir Conta" }
        ).ClickAsync();
    }
}