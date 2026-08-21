using ControleDeBar.Dominio.Modulos.ModuloGarcom;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloGarcom;

[TestClass]
public sealed class GarcomTestes
{
    [TestMethod]
    public void Validar_ComNomeVazio_DeveRetornarErro()
    {
        Garcom garcom = new Garcom(string.Empty);

        List<string> erros = garcom.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve ser preenchido.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNomeCurto_DeveRetornarErro()
    {
        Garcom garcom = new Garcom(new string ('A', 1));

        List<string> erros = garcom.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve conter entre 2 e 50 caracteres.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNomeLongo_DeveRetornarErro()
    {
        Garcom garcom = new Garcom(new string ('A', 51));

        List<string> erros = garcom.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve conter entre 2 e 50 caracteres.",
            erros.First()
        );
    }
}