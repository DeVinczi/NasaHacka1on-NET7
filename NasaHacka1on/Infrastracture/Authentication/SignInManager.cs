using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using NasaHacka1on.Database.Models;

namespace NasaHacka1on.Models.Models;

public interface ISignInManager
{
    Task<SignInResultType> PasswordSignInAsync(SignedInUser user);
    Task SignOutAsync(params string[] schemes);
}

public class SignInManager : ISignInManager
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public SignInManager(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _HttpContextAccessor = httpContextAccessor;
    }

    public async Task<SignInResultType> PasswordSignInAsync(SignedInUser user)
    {
        var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.RememberMe, user.ShouldLockOutOnFailure);

        if (result.Succeeded)
        {
            return SignInResultType.Succeeded;
        }

        if (result.IsLockedOut)
        {
            return SignInResultType.IsLockedOut;
        }

        if (result.IsNotAllowed)
        {
            return SignInResultType.IsNotAllowed;
        }

        if (result.RequiresTwoFactor)
        {
            return SignInResultType.RequiresTwoFactor;
        }

        return SignInResultType.Unknown;
    }

    public async Task SignOutAsync(params string[] schemes)
    {
        if (_HttpContextAccessor.HttpContext is null)
        {
            throw new Exception("HttpContext can not be null");
        }

        foreach (var scheme in schemes)
        {
            await _HttpContextAccessor.HttpContext.SignOutAsync(scheme);
        }
    }
}

