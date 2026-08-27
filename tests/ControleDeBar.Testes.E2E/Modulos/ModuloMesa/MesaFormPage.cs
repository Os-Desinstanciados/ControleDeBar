using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloMesa;

public sealed class MesaFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Mesa/Cadastrar";
    public string UrlEditar => $"{urlBase}/Mesa/Editar";
    public ILocator Numero => page.GetByLabel("Número", new() { Exact = true});
    public ILocator NumeroLugares => page.GetByLabel("Número de Lugares");

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
        await Numero.FillAsync(numero);
        await NumeroLugares.FillAsync(numeroLugares);
    }

    public async Task ConfirmarAsync()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();
    }
}