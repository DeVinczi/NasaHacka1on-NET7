using System.Reflection;

namespace NasaHacka1on.Common.Extensions;

public static class AssemblyExtensions
{
    public static void RegisterAllTypesEndsWith
        (this IServiceCollection services,
        string nameEndsWith,
        Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => !t.IsInterface)
            .Where(t => !t.IsAbstract)
            .Where(t => t.Name.EndsWith(nameEndsWith));

        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();

            foreach (var @interface in interfaces)
            {
                services.AddTransient(@interface, type);
            }
        }
    }
}
