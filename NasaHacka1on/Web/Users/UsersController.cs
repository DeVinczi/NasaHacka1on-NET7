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

    public UsersLoginController()
    {
    }

    [AllowAnonymous]
    [HttpGet, Route(Github)]
    public ActionResult GithubLogin()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "https://localhost:44418"
            }, OAuthProviders.Github);
    }

    [AllowAnonymous]
    [HttpGet, Route(Google)]
    public ActionResult GoogleLogin()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "https://localhost:44418"
            }, GoogleDefaults.AuthenticationScheme);
    }

    [AllowAnonymous]
    [HttpGet, Route(Facebook)]
    public ActionResult FacebookLogin()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "https://localhost:44418"
            }, FacebookDefaults.AuthenticationScheme);
    }
}
