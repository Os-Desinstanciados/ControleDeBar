using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Testes.Unidade.Modulos.ModuloConta;

[TestClass]
public sealed class ContaTestes
{
    [TestMethod]
    public void Validar_ComMesaNaoSelecionada_DeveRetornarErro()
    {
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(
            null,
            garcom
        );

        List<string> erros = conta.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "A mesa deve ser selecionada.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComGarcomNaoSelecionado_DeveRetornarErro()
    {
        Mesa mesa = new Mesa();

        Conta conta = new Conta(
            mesa,
            null
        );

        List<string> erros = conta.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O garçom deve ser selecionado.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComDataAberturaVazia_DeveRetornarErro()
    {
        Mesa mesa = new Mesa();
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(
            mesa,
            garcom
        );

        conta.DataAbertura = default;

        List<string> erros = conta.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "A data de abertura deve ser preenchida.",
            erros.First()
        );
    }

    [TestMethod]
    public void Validar_ComDataFechamentoInferior_DeveRetornarErro()
    {
        Mesa mesa = new Mesa();
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(
            mesa,
            garcom
        );

        conta.DataAbertura = new DateTime(2026, 8, 20);
        conta.DataFechamento = new DateTime(2026, 3, 20);

        List<string> erros = conta.Validar();

        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "A data de fechamento deve ser posterior à data de abertura.",
            erros.First()
        );
    }

    [TestMethod]
    public void Fechar_ContaAberta_DeveFecharConta()
    {
        Mesa mesa = new Mesa();
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(mesa, garcom);

        conta.Fechar();

        Assert.AreEqual(StatusConta.Fechada, conta.Status);
        Assert.IsNotNull(conta.DataFechamento);
    }
    
    [TestMethod]
    public void Validar_ContaValida_DeveRetornarValida()
    {
        Mesa mesa = new Mesa();
        Garcom garcom = new Garcom("Junior Testes");

        Conta conta = new Conta(
            mesa,
            garcom
        );

        List<string> erros = conta.Validar();

        Assert.HasCount(0, erros);
    }
}