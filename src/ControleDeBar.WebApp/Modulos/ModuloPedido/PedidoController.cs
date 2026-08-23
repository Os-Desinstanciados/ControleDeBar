using AutoMapper;
using ControleDeBar.Aplicacao.ModuloPedido;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloPedido;

public class PedidoController : Controller
{
    private readonly ServicoPedido servicoPedido;
    private readonly IMapper mapeador;

    public PedidoController(
        ServicoPedido servicoPedido,
        IMapper mapeador)
    {
        this.servicoPedido = servicoPedido;
        this.mapeador = mapeador;
    }

    [HttpPost]
    public ActionResult Adicionar(
        Guid contaId,
        AdicionarPedidoViewModel viewModel
    )
    {
        PedidoDto pedidoDto = mapeador.Map<PedidoDto>(viewModel);

        servicoPedido.Adicionar(contaId, pedidoDto);

        return RedirectToAction(
            "Detalhes",
            "Conta",
            new { id = contaId }
        );
    }
}