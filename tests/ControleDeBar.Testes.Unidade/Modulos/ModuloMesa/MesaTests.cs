using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloMesa;

[TestClass]
public sealed class MesaTestes
{
    [TestMethod]
    public void Validar_ComNumeroVazio_DeveRetornarErro()
    {
        Mesa mesa = new Mesa(string.Empty, "1");

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número\" deve ser preenchido.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNumeroAcimaDoMaximo_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("1111", "1");

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número\" deve conter até 3 caracteres.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNumeroIgualaZero_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("0", "1");

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número\" não deve ser igual a zero.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNumeroComLetras_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("aaa", "1");

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número\" deve conter apenas números.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNumeroLugaresVazio_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("1", string.Empty);

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número de Lugares\" deve ser preenchido.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNumeroLugaresAcimaDoMaximo_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("1", "111");

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número de Lugares\" deve conter até 2 caracteres.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNumeroLugaresIgualaZero_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("1", "0");

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número de Lugares\" não deve ser igual a zero.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComNumeroLugaresComLetras_DeveRetornarErro()
    {
        Mesa mesa = new Mesa("1", "aa");

        List<string> erros = mesa.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Número de Lugares\" deve conter apenas números.",
            erros.First()
        );
    }
    
}