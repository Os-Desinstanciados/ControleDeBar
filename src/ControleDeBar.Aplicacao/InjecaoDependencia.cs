using ControleDeBar.Aplicacao.Modulos.ModuloGarcom;
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
    }
}
