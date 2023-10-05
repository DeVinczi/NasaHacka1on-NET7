using Microsoft.AspNetCore.Identity;
using NasaHacka1on.Database.Models;

namespace NasaHacka1on.Models.Models;

public interface IUserManager
{
    Task<IdentityResult> CreateAsync(ApplicationUser user);
    Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
    Task<IdentityResult> ChangeEmailAsync(ApplicationUser user, string newEmail, string token);
    Task<ApplicationUser> FindByIdAsync(string userId);
    Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
    Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
    Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
    Task<string> GenerateChangeEmailTokenAsync(ApplicationUser user, string newEmail);
    Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    Task<ApplicationUser> FindByEmailAsync(string email);
    Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
}

public class UserManager : IUserManager
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManager(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> ChangeEmailAsync(ApplicationUser user, string newEmail, string token)
    {
        var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
        return result;
    }

    public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, newPassword, currentPassword);
        return result;
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result;
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user)
    {
        var result = await _userManager.CreateAsync(user);
        return result;
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result;
    }

    public async Task<ApplicationUser> FindByEmailAsync(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);
        return result;
    }

    public async Task<ApplicationUser> FindByIdAsync(string userId)
    {
        var result = await _userManager.FindByIdAsync(userId);
        return result;
    }

    public async Task<string> GenerateChangeEmailTokenAsync(ApplicationUser user, string newEmail)
    {
        var result = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        return result;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user, string newEmail)
    {
        var result = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        return result;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return result;
    }

    public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
    {
        var result = _userManager.GeneratePasswordResetTokenAsync(user);
        return result;
    }

    public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
    {
        var result = await _userManager.IsEmailConfirmedAsync(user);
        return result;
    }

    public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
    {
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result;
    }
}
