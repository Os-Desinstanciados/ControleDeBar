namespace ControleDeBar.Aplicacao.Modulos.ModuloCliente;

public record ListarClientesDto(
    string Nome
);

public record CadastrarClienteDto(
    string Nome
);

public record EditarClienteDto(
    Guid Id,
    string Nome
);