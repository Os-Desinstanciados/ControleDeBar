using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloProduto;

namespace ControleDeBar.Dominio.Modulos.ModuloPedido;

public class Pedido : EntidadeBase<Pedido>
{
    public int Quantidade { get; set; }
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public Conta Conta { get; set; }
    public Guid ContaId { get; set; }
    public DateTime DataHora { get; set; }
    public Pedido()
    {
    }

    public Pedido(
        int quantidade,
        Produto produto,
        Conta conta
    ) : this()
    {
        Quantidade = quantidade;
        Produto = produto;
        ProdutoId = produto.Id;
        Conta = conta;
        ContaId = conta.Id;
        DataHora = DateTime.Now;
    }

    public decimal CalcularSubtotal()
    {
        return Produto.Preco * Quantidade;
    }

    public override void Atualizar(Pedido entidadeAtualizada)
    {
        Quantidade = entidadeAtualizada.Quantidade;
        Produto = entidadeAtualizada.Produto;
        ProdutoId = entidadeAtualizada.ProdutoId;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Quantidade <= 0)
            erros.Add("A quantidade do pedido deve ser maior que zero.");

        if (Produto is null)
            erros.Add("O campo \"Produto\" deve ser preenchido.");

        if (Conta is null)
            erros.Add("O campo \"Conta\" deve ser preenchido.");

        return erros;
    }
}