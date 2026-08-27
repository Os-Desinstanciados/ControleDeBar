using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloMesa;

public sealed class MesaFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Mesa/Cadastrar";
    public string UrlEditar => $"{urlBase}/Mesa/Editar";

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task IrParaEdicaoAsync()
    {
        await page.GotoAsync(UrlEditar);
    }

    public async Task PreencherAsync(string numero, string numeroLugares)
    {
        await page.GetByLabel("Número", new() { Exact = true }).FillAsync(numero);
        await page.GetByLabel("Número de Lugares").FillAsync(numeroLugares);
    }

    public async Task ConfirmarAsync()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();
    }
}