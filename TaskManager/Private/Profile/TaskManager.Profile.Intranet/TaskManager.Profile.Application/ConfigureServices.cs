using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManager.Profile.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;



namespace TaskManager.Profile.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddProfileApp(this IServiceCollection services, Action<DbContextOptionsBuilder> dbSetup)
    {
        services.AddDbContext<UserProfileDbContext>(options =>
        {
            dbSetup(options);
        });
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly);
        });

        //NEED?
        return services;
    }
}
