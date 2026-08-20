using AutoMapper;
using ControleDeBar.WebApp.Modulos.ModuloConta;

namespace ControleDeBar.WebApp.Config.AutoMapper.Profiles;

public class ContaProfile : Profile
{
    public ContaProfile()
    {
        CreateMap<ListarContasDto, ListarContasViewModel>();

        CreateMap<DetalhesContaDto, DetalhesContaViewModel>();
    }
}