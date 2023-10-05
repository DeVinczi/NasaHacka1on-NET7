using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace NasaHacka1on.Infrastracture.Authentication;

public static class AuthenticationEvents
{
    public static Task OnRedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            return Task.CompletedTask;
        }

        context.Response.Redirect(context.RedirectUri);

        return Task.CompletedTask;
    }

    public static async Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.CompleteAsync();
            return;
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }
    }

    public static Task OnRemoteFailure(RemoteFailureContext context)
    {
        context.Response.Redirect("/authentication-server-not-responding");

        if (context.Failure != null)
        {
            GetLogger().LogError(context.Failure, "Authentication Server Failure");
        }

        return Task.CompletedTask;
    }

    public static Task OnSigningOut(CookieSigningOutContext ctx)
    {
        ctx.HttpContext.Response.Cookies.Delete("CommunityCodeHub");
        return Task.CompletedTask;
    }

    public static async Task OnValidatePrincipal(CookieValidatePrincipalContext cookieContext)
    {
        if (!cookieContext.Principal.Identity.IsAuthenticated)
        {
            return;
        }

        var userIdentityValidator = cookieContext.HttpContext.RequestServices.GetRequiredService<IUserIdentityValidator>();
        var validationResult = await userIdentityValidator.ValidateAsync(cookieContext.Principal);

        if (!validationResult.IsValid)
        {
            if (validationResult.ValidClaimsIdentity == null)
            {
                return;
            }

            ClearClaimsIfNeeded(cookieContext.Principal, validationResult.ValidClaimsIdentity);

            var missingClaims = GetUniqueClaims(cookieContext.Principal, validationResult.ValidClaimsIdentity);

            cookieContext.Principal.AddIdentity(new ClaimsIdentity(missingClaims));
        }
    }

    public static async Task OnSigningIn(CookieSigningInContext ctx)
    {
        await HandleSigningIn(ctx.HttpContext.RequestServices, ctx.Principal, ctx.Properties);
    }


    private static async Task HandleSigningIn(IServiceProvider serviceProvider, ClaimsPrincipal principal, AuthenticationProperties properties)
    {
        var identityClaimsFactory = serviceProvider.GetRequiredService<IClaimsIdentityFactory>();
        var options = serviceProvider.GetRequiredService<IOptions<OAuthModel>>();

        var emailClaim = principal.Claims.FirstOrDefault(x => x.Type == options.Value.EmailClaim);

        if (emailClaim is null)
        {
            var loginClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            var claimsIdentityLogin = await identityClaimsFactory.CreateAsync(loginClaim.Value);

            ClearClaimsIfNeeded(principal, claimsIdentityLogin);

            var missingClaimsLogin = GetUniqueClaims(principal, claimsIdentityLogin);

            var identityLogin = principal.Identities.First();
            identityLogin.AddClaims(missingClaimsLogin);

            return;
        }

        var claimsIdentity = await identityClaimsFactory.CreateAsync(emailClaim.Value);

        ClearClaimsIfNeeded(principal, claimsIdentity);

        var missingClaims = GetUniqueClaims(principal, claimsIdentity);

        var identity = principal.Identities.First();
        identity.AddClaims(missingClaims);
    }

    private static List<Claim> GetUniqueClaims(ClaimsPrincipal claimsPrincipal, ClaimsIdentity claimsIdentity)
    {
        var missingClaims = new List<Claim>();

        foreach (var claim in claimsIdentity.Claims)
        {
            if (!claimsPrincipal.HasClaim(claim.Type, claim.Value))
            {
                missingClaims.Add(claim);
            }
        }

        return missingClaims;
    }

    private static void ClearClaimsIfNeeded(ClaimsPrincipal claimsPrincipal, ClaimsIdentity newClaimsIdentity)
    {
        var claimTypes = new[] { ClaimTypes.Role, ClaimTypes.Name };
        var hasTheSameClaims = claimsPrincipal.Claims
            .Where(x => claimTypes.Contains(x.Type))
            .All(x => newClaimsIdentity.HasClaim(x.Type, x.Value));

        if (hasTheSameClaims)
        {
            return;
        }

        var identities = claimsPrincipal.Identities.ToList();

        for (var i = 0; i < identities.Count; i++)
        {
            var claimsIdentity = identities[i];
            var claims = claimsIdentity.Claims.ToList();

            for (var j = claims.Count - 1; j >= 0; j--)
            {
                var claim = claims[j];

                if (claimTypes.Any(claimType => claimType == claim.Type))
                {
                    claimsIdentity.RemoveClaim(claims[j]);
                }
            }
        }
    }

    private static ILogger GetLogger()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        return loggerFactory.CreateLogger(string.Empty);
    }
}
