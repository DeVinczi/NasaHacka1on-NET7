using NasaHacka1on.BusinessLogic.Providers;
using NasaHacka1on.Common.Extensions;

namespace NasaHacka1on;

public sealed class ModuleBootstrapper
{

    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        RegisterServices(services);
        RegisterStores(services);
        RegisterCommandHandlers(services);
    }

    private void RegisterServices(IServiceCollection services)
    {
        AssemblyExtensions.RegisterAllTypesEndsWith(services, "Service", GetType().Assembly);
    }
    private void RegisterStores(IServiceCollection services)
    {
        AssemblyExtensions.RegisterAllTypesEndsWith(services, "Storage", GetType().Assembly);
    }
    private void RegisterCommandHandlers(IServiceCollection services)
    {
        AssemblyExtensions.RegisterAllTypesEndsWith(services, "CommandHandler", GetType().Assembly);
    }
    private void RegisterFactories(IServiceCollection services)
    {
        AssemblyExtensions.RegisterAllTypesEndsWith(services, "Factory", GetType().Assembly);
    }
}
