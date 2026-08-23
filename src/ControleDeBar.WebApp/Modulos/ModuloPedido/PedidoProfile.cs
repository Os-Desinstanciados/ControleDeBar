using AutoMapper;
using ControleDeBar.Aplicacao.ModuloPedido;

namespace ControleDeBar.WebApp.Modulos.ModuloPedido;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<AdicionarPedidoViewModel, PedidoDto>();
    }
}