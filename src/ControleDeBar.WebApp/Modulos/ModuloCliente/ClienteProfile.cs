using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloCliente;
using ControleDeBar.WebApp.Modulos.ModuloCliente;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<ListarClientesDto, ListarClientesViewModel>();
        CreateMap<CadastrarClienteViewModel, CadastrarClienteDto>();
        CreateMap<EditarClienteViewModel, EditarClienteDto>();
        CreateMap<DetalhesClienteDto, EditarClienteViewModel>();
        CreateMap<DetalhesClienteDto, ExcluirClienteViewModel>();
    }
}