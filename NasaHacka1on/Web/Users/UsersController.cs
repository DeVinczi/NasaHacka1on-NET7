using Gybs.Logic.Operations.Factory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NasaHacka1on.Models;
using NasaHacka1on.Models.Constants;

namespace NasaHacka1on.Web.Users;

public class UsersLoginController : CommunityCodeHubController
{
    private const string Route = "/api/login";
    private const string Github = Route + "/github";
    private const string Google = Route + "/google";
    private const string Facebook = Route + "/facebook";

    private readonly IOperationFactory _operationFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsersLoginController(
        IOperationFactory operationFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _operationFactory = operationFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    [AllowAnonymous]
    [HttpGet, Route(Route)]
    public async Task<IActionResult> GithubClaim()
    {
        var x = _httpContextAccessor.HttpContext.User.Claims.Select(x => new { x.Type, x.Value }).ToList();
        await Console.Out.WriteLineAsync(x.ToString());
        var z = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        var zz = await _httpContextAccessor.HttpContext.GetTokenAsync("prompt");
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet, Route(Github)]
    public async Task<IResult> GithubLogin()
    {
        return Results.Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "https://localhost:7120"
            },
            authenticationSchemes: new List<string>() { OAuthProviders.Github });
    }

    [AllowAnonymous]
    [HttpGet, Route(Google)]
    public async Task<IResult> GoogleLogin()
    {
        return Results.Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "https://localhost:7120"
            },
            authenticationSchemes: new List<string>() { GoogleDefaults.AuthenticationScheme });
    }

    [AllowAnonymous]
    [HttpGet, Route(Facebook)]
    public async Task<IResult> FacebookLogin()
    {
        return Results.Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "https://localhost:7120"
            },
            authenticationSchemes: new List<string>() { FacebookDefaults.AuthenticationScheme });
    }
}
