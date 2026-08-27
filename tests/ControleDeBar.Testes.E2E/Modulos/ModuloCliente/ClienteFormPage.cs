using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloCliente;

public sealed class ClienteFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Cliente/Cadastrar";
    public string UrlEditar => $"{urlBase}/Cliente/Editar";

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task PreencherNomeAsync(string nome)
    {
        await page.GetByLabel("Nome").FillAsync(nome);
    }

    public async Task ConfirmarAsync()
    {
        await page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" }
        ).ClickAsync();
    }
}