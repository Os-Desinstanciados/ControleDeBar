namespace ControleDeBar.Aplicacao.ModuloPedido;

public record PedidoDto(
    Guid ProdutoId,
    int Quantidade
);