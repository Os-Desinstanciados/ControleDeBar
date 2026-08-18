using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Aplicacao.Modulos.ModuloMesa;

public record ListarMesasDto(
    string Numero,
    string NumeroLugares,
    StatusMesa StatusMesa,
    Guid Id
);

public record CadastrarMesaDto(
    string Numero,
    string NumeroLugares,
    StatusMesa StatusMesa
);

public record EditarMesaDto(
    string Numero,
    string NumeroLugares,
    StatusMesa StatusMesa,
    Guid Id
);

public record DetalhesMesaDto(
    string Numero,
    string NumeroLugares,
    StatusMesa StatusMesa,
    Guid Id
);