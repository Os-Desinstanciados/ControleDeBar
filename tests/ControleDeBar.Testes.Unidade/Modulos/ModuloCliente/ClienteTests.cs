using ControleDeBar.Dominio.Modulos.ModuloCliente;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloCliente;

[TestClass]
public sealed class ClienteTestes
{
    [TestMethod]
    public void Validar_ComNomeVazio_DeveRetornarErro()
    {
        Cliente cliente = new Cliente(string.Empty);

        List<string> erros = cliente.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve ser preenchido.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNomeCurto_DeveRetornarErro()
    {
        Cliente cliente = new Cliente(new string ('A', 1));

        List<string> erros = cliente.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve conter entre 2 e 100 caracteres.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNomeLongo_DeveRetornarErro()
    {
        Cliente cliente = new Cliente(new string ('A', 101));

        List<string> erros = cliente.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve conter entre 2 e 100 caracteres.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ClienteValido_DeveRetornarValido()
    {
        Cliente cliente = new Cliente("Alexandre");

        List<string> erros = cliente.Validar();

        Assert.HasCount(0, erros);
    }


}