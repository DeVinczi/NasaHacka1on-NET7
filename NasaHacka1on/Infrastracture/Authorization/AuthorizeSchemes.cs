using Microsoft.AspNetCore.Authorization;
using NasaHacka1on.Infrastracture.Services;
using System.Security.Claims;

namespace NasaHacka1on.Infrastracture.Authorization;

public class AuthorizeSchemeRequirement : IAuthorizationRequirement
{
}

public class AuthorizeSchemes : AuthorizationHandler<AuthorizeSchemeRequirement>
{
    private readonly IAccountService _accountService;

    public AuthorizeSchemes(IAccountService accountService)
    {
        _accountService = accountService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeSchemeRequirement requirement)
    {
        var currentUser = _accountService.GetUserByEmail(context.User.Claims.First(x => x.Type == ClaimTypes.Email).Value);

        if (currentUser == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
