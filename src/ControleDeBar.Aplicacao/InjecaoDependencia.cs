using ControleDeBar.Aplicacao.Modulos.ModuloGarcom;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
using ControleDeBar.Aplicacao.Modulos.ModuloCliente;
using ControleDeBar.Aplicacao.Modulos.ModuloProduto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeBar.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {          
        services.AddScoped<ServicoGarcom>();
        services.AddScoped<ServicoMesa>();
        services.AddScoped<ServicoCliente>();
        services.AddScoped<ServicoProduto>();
    }
}
