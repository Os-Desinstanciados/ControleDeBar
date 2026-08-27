using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloGarcom;

public sealed class GarcomFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Garcom/Cadastrar";
    public string UrlEditar => $"{urlBase}/Garcom/Editar";
    public ILocator Nome => page.GetByLabel("Nome");
    

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task IrParaEdicaoAsync()
    {
        await page.GotoAsync(UrlEditar);
    }

    public async Task PreencherNomeAsync(string nome)
    {
        await Nome.FillAsync(nome);
    }

    public async Task ConfirmarAsync()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" })
            .ClickAsync();
    }
}