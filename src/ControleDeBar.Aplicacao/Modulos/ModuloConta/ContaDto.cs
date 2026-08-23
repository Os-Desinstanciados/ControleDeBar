using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloPedido;

public record ListarContasDto(
    Guid Id,
    Mesa Mesa,
    Garcom Garcom,
    DateTime DataAbertura,
    DateTime? DataFechamento,
    StatusConta Status
);

public record CadastrarContaDto(
    Guid MesaId,
    Guid GarcomId
);

public record DetalhesContaDto(
    Guid Id,
    Mesa Mesa,
    Garcom Garcom,
    DateTime DataAbertura,
    DateTime? DataFechamento,
    StatusConta Status,
    List<Pedido> Pedidos
);