namespace ControleDeBar.Aplicacao.Modulos.ModuloCliente;

public record ListarClientesDto(
    Guid Id,
    string Nome
);

public record CadastrarClienteDto(
    string Nome
);

public record EditarClienteDto(
    Guid Id,
    string Nome
);

public record DetalhesClienteDto(
    Guid Id,
    string Nome
);