using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace ControleDeBar.Testes.E2E;

[TestClass]
public sealed class AutenticacaoE2ETests : PageTest
{
    private string UrlBase = "http:localhost:5026";

    [TestMethod]
    public async Task Deve_Exibir_TelaDeLogin_ParaUsuarioAnonimo()
    {
        // Arrange
        

        // Act
        await Page.GotoAsync($"{UrlBase}/");

        // Assert
        await Expect(Page).ToHaveTitleAsync(new Regex("Entrar"));
    }

    [TestMethod]
    public async Task Deve_RegistrarEAutenticar_Usuario()
    {
        // Arrange        
        const string email = "novo.usuario@teste.local";
        const string senha = "Senha123!";

        await Page.GotoAsync($"{UrlBase}/Autenticacao/Registrar");

        // Act
        await Page.GetByLabel("E-mail").FillAsync(email);
        await Page.GetByLabel("Senha", new() { Exact = true }).FillAsync(senha);
        await Page.GetByLabel("Confirmar Senha").FillAsync(senha);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Criar Conta" }).ClickAsync();

        // Assert
        string rotaAbsoluta = new Uri(Page.Url).AbsolutePath;

        Assert.AreEqual("/", rotaAbsoluta);
    }
}
