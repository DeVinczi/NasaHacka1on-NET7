using BackendHackaton.Infrastracture.NamingStrategies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NasaHacka1on.Database;
using NasaHacka1on.Database.Models;
using NasaHacka1on.Infrastracture.Authentication;
using NasaHacka1on.Models.Constants;
using NasaHacka1on.Models.JsonConverters;
using NasaHacka1on.Models.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BackendHackaton.Infrastracture.Extensions;

internal static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddBackendMvc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISignInManager, SignInManager>();
        services.AddTransient<IUserManager, UserManager>();

        services.AddMvc(mvcOptions =>
        {
            var cacheProfilesSettings = new Dictionary<string, CacheProfile>();
            var section = configuration.GetSection("CacheProfiles");
            section.Bind(cacheProfilesSettings);

            foreach (var cacheProfileSetting in cacheProfilesSettings)
            {
                mvcOptions.CacheProfiles.Add(cacheProfileSetting);
            }
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new KebabCaseNamingStrategy(), false));
            options.JsonSerializerOptions.Converters.Add(new UtcDateTimeJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new SanitizeHtmlJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new StringToIntJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new StringToGuidConverter());
        });

        services.AddIdentityCore<ApplicationUser>(opt =>
        {
            opt.Password.RequireDigit = true;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequiredLength = 9;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;

            opt.User.RequireUniqueEmail = true;

            opt.SignIn.RequireConfirmedAccount = false;
            opt.SignIn.RequireConfirmedEmail = false;
            opt.SignIn.RequireConfirmedPhoneNumber = false;

            //opt.Stores.ProtectPersonalData = true;

            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<CommunityCodeHubDataContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        services.AddAntiforgery(options =>
        {
            options.Cookie = new CookieBuilder
            {
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Name = "X-RT-VerificationToken",
                IsEssential = true,
            };
            options.SuppressXFrameOptionsHeader = false;
            options.FormFieldName = "RequestVerificationToken";
            options.HeaderName = "X-RT-VerificationToken";
        });

        return services;
    }

    public static IServiceCollection AddBackendAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, c =>
            {
                c.AccessDeniedPath = "/AccessDenied";
                c.LogoutPath = "/log-out";
                c.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                c.Cookie.Name = "CommunityCodeHub";
                c.Cookie.HttpOnly = false;
                c.Cookie.SameSite = SameSiteMode.Strict;
                c.LoginPath = "/sign-in";
                c.LogoutPath = "/sign-out";
                c.Events.OnRedirectToLogin = AuthenticationEvents.OnRedirectToLogin;
                c.Events.OnSigningIn = AuthenticationEvents.OnSigningIn;
                c.Events.OnRedirectToAccessDenied = AuthenticationEvents.OnRedirectToAccessDenied;
                c.Events.OnValidatePrincipal = AuthenticationEvents.OnValidatePrincipal;
                c.Events.OnSigningOut = AuthenticationEvents.OnSigningOut;
            })
            .AddOAuth(OAuthProviders.Github, o =>
            {
                o.SignInScheme = IdentityConstants.ApplicationScheme;

                o.ClientId = "623cd33a318d54648bf1";
                o.ClientSecret = "b8ee1c0f51d94505508904c4fbb1d32976aed567";

                o.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                o.AuthorizationEndpoint += "?" + Prompt.ForceLogin;
                o.TokenEndpoint = "https://github.com/login/oauth/access_token";
                o.CallbackPath = "/oauth/github-cb";
                o.SaveTokens = true;

                o.UserInformationEndpoint = "https://api.github.com/user";

                o.ClaimActions.MapJsonKey("sub", "id");
                o.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");

                o.Events = new OAuthEvents
                {
                    OnRemoteFailure = AuthenticationEvents.OnRemoteFailure,
                    OnCreatingTicket = async ctx =>
                    {
                        using var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);
                        using var result = await ctx.Backchannel.SendAsync(request);
                        var user = await result.Content.ReadFromJsonAsync<JsonElement>();
                        ctx.RunClaimActions(user);
                    }
                };
            })
             .AddGoogle(GoogleDefaults.AuthenticationScheme, o =>
            {
                o.SignInScheme = IdentityConstants.ApplicationScheme;

                o.ClientId = configuration["OAuth:ClientId"];
                o.ClientSecret = configuration["OAuth:ClientSecret"];

                o.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
                o.AuthorizationEndpoint += "?" + Prompt.ForceLogin;
                o.TokenEndpoint = "https://oauth2.googleapis.com/token";
                o.CallbackPath = "/signin-google";
                o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";

                o.SaveTokens = true;

                o.Events = new OAuthEvents
                {
                    OnRemoteFailure = AuthenticationEvents.OnRemoteFailure,
                    OnCreatingTicket = async ctx =>
                    {
                        using var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);
                        using var result = await ctx.Backchannel.SendAsync(request);
                        var user = await result.Content.ReadFromJsonAsync<JsonElement>();
                        ctx.RunClaimActions(user);
                    }
                };
            })
            .AddFacebook(FacebookDefaults.AuthenticationScheme, o =>
            {
                o.SignInScheme = IdentityConstants.ApplicationScheme;

                o.AppId = "3990509061237234";
                o.AppSecret = "200ace445ef49c83232f1d554993c638";
                o.CallbackPath = "/signin-facebook";
                o.AuthorizationEndpoint = "https://www.facebook.com/v18.0/dialog/oauth";
                o.AuthorizationEndpoint += "?" + Prompt.ForceLogin;
                o.TokenEndpoint = "https://graph.facebook.com/v18.0/oauth/access_token";
                o.UserInformationEndpoint = "https://graph.facebook.com/v18.0/me?fields=id,email";

                o.SaveTokens = true;

                o.Events = new OAuthEvents
                {
                    OnRemoteFailure = AuthenticationEvents.OnRemoteFailure,
                    OnCreatingTicket = async ctx =>
                    {
                        using var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);
                        using var result = await ctx.Backchannel.SendAsync(request);
                        var user = await result.Content.ReadFromJsonAsync<JsonElement>();
                        ctx.RunClaimActions(user);
                    }
                };
            });

        services.AddAuthorizationBuilder()
        .AddPolicy(OAuthProviders.OAuth, pb =>
        {
            pb.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
            .RequireClaim(ClaimTypes.AuthenticationMethod, "OAuth")
            .RequireAuthenticatedUser();
        })
        .AddPolicy(FacebookDefaults.AuthenticationScheme, pb =>
        {
            pb.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
            .RequireClaim(ClaimTypes.AuthenticationMethod, "OAuth")
            .RequireAuthenticatedUser();
        })
        .AddPolicy(GoogleDefaults.AuthenticationScheme, pb =>
        {
            pb.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
            .RequireClaim(ClaimTypes.AuthenticationMethod, "OAuth")
            .RequireAuthenticatedUser();
        })
        .AddPolicy(IdentityConstants.ApplicationScheme, pb =>
        {
            pb.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
           .RequireClaim(ClaimTypes.AuthenticationMethod, "Internal")
           .RequireAuthenticatedUser();
        });

        return services;
    }
}
