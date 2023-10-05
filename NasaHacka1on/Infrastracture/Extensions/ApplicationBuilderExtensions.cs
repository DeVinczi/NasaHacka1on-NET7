using Microsoft.AspNetCore.Authentication;

namespace NasaHacka1on.Models.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCommunityCodeHubEndpoints(this IApplicationBuilder app)
    {
        return app.UseEndpoints(e =>
        {
            e.MapControllerRoute(
                name: "Api",
                pattern: "api/{controller}/{action}/{id?}");
        });
    }

    public static IApplicationBuilder UseSpaAuthentication(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            if (context.User.Identity.IsAuthenticated)
            {
                await next();

                return;
            }

            if (IsAccountFile(context.Request.Path)
                || context.Request.Path.StartsWithSegments("/public")
                || IsStaticPublicFile(context.Request.Path))
            {
                await next();

                return;
            }

            await context.ChallengeAsync();
        });
    }

    private static bool IsStaticPublicFile(PathString path)
    {
        if (!path.HasValue)
        {
            return false;
        }

        return path.Value.StartsWith("/audio")
            || path.Value.StartsWith("/backgrounds")
            || path.Value.StartsWith("/favicons")
            || path.Value.StartsWith("/fonts")
            || path.Value.StartsWith("/icons")
            || path.Value.StartsWith("/images")
            || path.Value.StartsWith("/logotype")
            || path.Value.StartsWith("/shared")
            || path.Value.Contains("/bootstrap")
            || path.Value.Contains("/common")
            || path.Value.EndsWith("moment.js")
            || path.Value.EndsWith("favicon.ico")
            || path.Value.StartsWith("/log");
    }

    private static bool IsAccountFile(PathString path)
    {
        if (!path.HasValue)
        {
            return false;
        }

        return path.Value.StartsWith("/sign-in")
            || path.Value.StartsWith("/sign-up")
            || path.Value.StartsWith("/forgot-password")
            || path.Value.StartsWith("/account/confirm-email")
            || path.Value.StartsWith("/account/change-email")
            || path.Value.StartsWith("/reset-password")
            || path.Value.StartsWith("/@vite")
            || path.Value.StartsWith("/@id")
            || path.Value.StartsWith("/node_modules")
            || path.Value.StartsWith("/src");
    }
}
