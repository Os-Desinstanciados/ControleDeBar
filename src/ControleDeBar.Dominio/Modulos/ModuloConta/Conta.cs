using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloPedido;

namespace ControleDeBar.Dominio.Modulos.ModuloConta;

public class Conta : EntidadeBase<Conta>
{
    public Mesa Mesa { get; set; }
    public Garcom Garcom { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime? DataFechamento { get; set; }
    public StatusConta Status { get; set; }
    public List<Pedido> Pedidos { get; set; } = [];

    public Conta()
    {
    }

    public Conta(Mesa mesa, Garcom garcom) : this()
    {
        Mesa = mesa;
        Garcom = garcom;

        DataAbertura = DateTime.Now;
        DataFechamento = null;

        Status = StatusConta.Aberta;
    }

    public override void Atualizar(Conta entidadeAtualizada)
    {
        Mesa = entidadeAtualizada.Mesa;
        Garcom = entidadeAtualizada.Garcom;
        Status = entidadeAtualizada.Status;
        DataFechamento = entidadeAtualizada.DataFechamento;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Mesa == null)
            erros.Add("A mesa deve ser selecionada.");

        if (Garcom == null)
            erros.Add("O garçom deve ser selecionado.");

        if (DataAbertura == default)
            erros.Add("A data de abertura deve ser preenchida.");

        if (DataFechamento.HasValue && DataFechamento < DataAbertura)
            erros.Add("A data de fechamento deve ser posterior à data de abertura.");

        return erros;
    }

    public void Fechar()
    {
        Status = StatusConta.Fechada;
        DataFechamento = DateTime.Now;
    }
}