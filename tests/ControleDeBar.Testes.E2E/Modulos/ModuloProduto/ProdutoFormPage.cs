using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloProduto;

public sealed class ProdutoFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Produto/Cadastrar";
    public string UrlEditar => $"{urlBase}/Produto/Editar";

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task PreencherAsync(string nome, string preco)
    {
        await page.GetByLabel("Nome").FillAsync(nome);
        await page.GetByLabel("Preço").FillAsync(preco);
    }

    public async Task ConfirmarAsync()
    {
        await page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar" }
        ).ClickAsync();
    }
}