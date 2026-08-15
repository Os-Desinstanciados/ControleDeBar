using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ControleDeBar.Infra.Compartilhado.Orm;

public class ControleDeBarDbContextFactory : IDesignTimeDbContextFactory<ControleDeBarDbContext>
{
    public ControleDeBarDbContext CreateDbContext(string[] args)
    {
        // 1. Busca o arquivo appsettings.json do projeto web para pegar a string real automaticamente
        // Ajuste o caminho relativo se a pasta do projeto Web tiver outro nome (ex: ControleDeBar.API)
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ControleDeBar.WebApp");
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Server=(localdb)\\mssqllocaldb;Database=ControleDeBarDb;Trusted_Connection=True;";

        var optionsBuilder = new DbContextOptionsBuilder<ControleDeBarDbContext>();

        // 2. 💡 Ajuste aqui para o provedor do seu banco (UseNpgsql, UseMySql, etc.)
        optionsBuilder.UseSqlServer(connectionString);

        return new ControleDeBarDbContext(optionsBuilder.Options);
    }
}