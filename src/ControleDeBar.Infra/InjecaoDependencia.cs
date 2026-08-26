using ControleDeBar.Dominio.Modulos.ModuloGarcom;
using ControleDeBar.Infra.Modulos.ModuloGarcom;
using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Infra.Compartilhado.Logging;
using ControleDeBar.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Infra.Modulos.ModuloMesa;
using Microsoft.AspNetCore.Identity;
using ControleDeBar.Dominio.Modulos.ModuloConta;
using ControleDeBar.Infra.Modulos.ModuloConta;
using ControleDeBar.Infra.Modulos.ModuloPedido;
using Microsoft.Extensions.Hosting;

namespace ControleDeBar.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging,
        IHostEnvironment environment
    )
    {
        // Injeta logs do Serilog
        Log.Logger = SerilogFactory.Create(configuration, environment);

        logging.ClearProviders();

        services.AddSerilog(Log.Logger);

        // Injeta o DbContext do EF
        services.AddDbContext<ControleDeBarDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServerEF");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"A connection string \"SqlServerEF\" não foi encontrada."
                );
            }

            options.UseSqlServer(connectionString, opt =>
            {
                opt.EnableRetryOnFailure(3);
            });
        });

        services.AddIdentityCore<IdentityUser<Guid>>(options => {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ControleDeBarDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        services.AddScoped<IRepositorioGarcom, RepositorioGarcomEmOrm>(); 
        services.AddScoped<IRepositorioMesa, RepositorioMesaEmOrm>(); 
        services.AddScoped<IRepositorioCliente, RepositorioClienteEmOrm>();
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmOrm>();
        services.AddScoped<IRepositorioConta, RepositorioContaEmOrm>();
        services.AddScoped<IRepositorioPedido, RepositorioPedidoEmOrm>();
    }
}