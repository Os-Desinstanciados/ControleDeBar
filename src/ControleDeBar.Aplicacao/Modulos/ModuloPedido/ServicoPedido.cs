using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Dominio.Modulos.ModuloCliente;

namespace ControleDeBar.Aplicacao.ModuloPedido;

public class ServicoPedido
{
    private readonly IRepositorioPedido repositorioPedido;
    private readonly IRepositorioProduto repositorioProduto;
    private readonly IRepositorioConta repositorioConta;

    public ServicoPedido(
        IRepositorioPedido repositorioPedido,
        IRepositorioProduto repositorioProduto,
        IRepositorioConta repositorioConta)
    {
        this.repositorioPedido = repositorioPedido;
        this.repositorioProduto = repositorioProduto;
        this.repositorioConta = repositorioConta;
    }

    public void Adicionar(Guid contaId, PedidoDto pedidoDto)
    {
        Conta? conta = repositorioConta.SelecionarPorId(contaId);

        Produto? produto = repositorioProduto.SelecionarPorId(
            pedidoDto.ProdutoId
        );

        Pedido pedido = new(
            pedidoDto.Quantidade,
            produto!,
            conta!
        );

        List<string> erros = pedido.Validar();

        if (erros.Count > 0)
            throw new ArgumentException(string.Join("\n", erros));

        repositorioPedido.Cadastrar(pedido);
    }
}