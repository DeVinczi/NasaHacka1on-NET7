using Microsoft.AspNetCore.Identity;
using NasaHacka1on.Database;
using System.Security.Claims;

namespace NasaHacka1on.Infrastracture.Services;

public interface IAccountService
{
    IdentityUser<Guid> GetCurrentUser();
    IdentityUser<Guid> GetUserByEmail(string email);
}

public class AccountService : IAccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CommunityCodeHubDataContext _dataContext;

    public AccountService(
        IHttpContextAccessor httpContextAccessor,
        CommunityCodeHubDataContext codeHubDataContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dataContext = codeHubDataContext;
    }

    public IdentityUser<Guid> GetCurrentUser()
    {
        var claim = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

        return GetUserByEmail(claim);
    }

    public IdentityUser<Guid> GetUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        return _dataContext.Users.FirstOrDefault(x => x.Email == email);
    }
}