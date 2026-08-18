namespace ControleDeBar.Aplicacao.Modulos.ModuloGarcom;

public record ListarGarconsDto(
    Guid Id,
    string Nome
);

public record CadastrarGarcomDto(string Nome);

public record EditarGarcomDto(
    Guid Id,
    string Nome
);

public record DetalhesGarcomDto(
    Guid Id,
    string Nome
);