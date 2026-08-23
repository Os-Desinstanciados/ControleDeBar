using System.ComponentModel.DataAnnotations;
using ControleDeBar.Aplicacao.Modulos.ModuloGarcom;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloPedido;

namespace ControleDeBar.WebApp.Modulos.ModuloConta;

public record ListarContasViewModel(
    Guid Id,
    Mesa Mesa,
    Garcom Garcom,
    DateTime DataAbertura,
    DateTime? DataFechamento,
    StatusConta Status
);

public record CadastrarContaViewModel(
    [Required(ErrorMessage = "A mesa deve ser selecionada.")]
    Guid? MesaId,

    [Required(ErrorMessage = "O garçom deve ser selecionado.")]
    Guid? GarcomId,

    List<ListarMesasDto>? Mesas,
    List<ListarGarconsDto>? Garcons
);

public record DetalhesContaViewModel(
    Guid Id,
    Mesa Mesa,
    Garcom Garcom,
    DateTime DataAbertura,
    DateTime? DataFechamento,
    StatusConta Status,
    List<Pedido> Pedidos
);