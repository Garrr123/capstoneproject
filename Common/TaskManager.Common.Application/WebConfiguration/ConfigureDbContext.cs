using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TaskManager.Common.Application.WebConfiguration;
public static class ConfigureCommonDbContext
{
    public static void AddCommonDbContext<T>(this WebApplicationBuilder webBuilder, string key, Action<DbContextOptionsBuilder>? builder) where T : DbContext
    {
        webBuilder.Services.AddDbContext<T>(dbSetup =>
        {
            ConfigureDb(webBuilder, key)(dbSetup);
            builder?.Invoke(dbSetup);
        });
    }

    public static Action<DbContextOptionsBuilder> ConfigureDb(WebApplicationBuilder webBuilder, string key)
    {
        return (dbSetup) =>
        {
            if (webBuilder.Environment.IsProduction() || webBuilder.Configuration.GetSection("ConnectionStrings:Default").Exists())
            {
                dbSetup.UseSqlServer(webBuilder.Configuration.GetConnectionString("Default")!);
            }
            else if (webBuilder.Configuration.GetSection($"connectionstrings:mssql:{key}").Exists())
            {
                dbSetup.UseSqlServer(webBuilder.Configuration[$"connectionstrings:mssql:{key}"]!);
            }
            else
            {
                Log.Warning($"Using in-memory mode for db: {key}");
                dbSetup.UseInMemoryDatabase(key)
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            }
        };
    }

    public static WebApplication EnsureDbCreatedForDevelopment<T>(this WebApplication app, bool recreate = false) where T : DbContext
    {
        using var scope = app.Services.CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        if (env.IsDevelopment())
        { 
            using var db = scope.ServiceProvider.GetRequiredService<T>();
            if (recreate)
                db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        return app;
    }
}

