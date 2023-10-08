using Gybs.Logic.Operations.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NasaHacka1on.BusinessLogic.Commands.Account;
using NasaHacka1on.Models;
using NasaHacka1on.Models.Models;
using NasaHacka1on.Web.Account.WebModels;

namespace NasaHacka1on.Web.Account
{
    public class LoginPasswordAccountController : CommunityCodeHubController
    {
        private readonly IOperationFactory _operationFactory;
        private readonly ISignInManager _signInManager;

        private const string Route = "/api/account/";

        public LoginPasswordAccountController(
            IOperationFactory operationFactory,
            ISignInManager signInManager)
        {
            _operationFactory = operationFactory;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost, Route(Route + "sign-in")]
        public async Task<ActionResult> SignIn([FromBody] SignInUserWebModel webModel)
        {
            var result = await _operationFactory
                .Create<SignInUserCommand>(c =>
                {
                    c.Email = webModel.Email.Trim().ToLower();
                    c.Password = webModel.Password;
                    c.RememberMe = webModel.RememberMe;
                })
                .HandleAsync();

            if (!result.HasSucceeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        [HttpPost, Route(Route + "sign-up")]
        public async Task<IActionResult> SignUp([FromBody] RegisterUserWebModel webModel)
        {
            var result = await _operationFactory
                .Create<SignUpUserCommand>(c =>
                {
                    c.DisplayName = webModel.DisplayName.Trim();
                    c.Email = webModel.Email.Trim().ToLower();
                    c.Password = webModel.Password;
                })
                .HandleAsync();

            if (!result.HasSucceeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        [HttpPost, Route(Route + "forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordWebModel webModel)
        {
            var result = await _operationFactory
                .Create<ForgotPasswordCommand>(c =>
                {
                    c.Email = webModel.Email.Trim().ToLower();
                })
                .HandleAsync();

            if (!result.HasSucceeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost, Route(Route + "sign-out")]
        public async Task<IActionResult> SignOutUser()
        {
            await _signInManager.SignOutAsync(IdentityConstants.ApplicationScheme);

            return Ok();
        }

        [HttpPost, Route(Route + "reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordWebModel webModel)
        {
            var result = await _operationFactory
                .Create<ResetPasswordCommand>(c =>
                {
                    c.Email = webModel.Email.Trim().ToLower();
                    c.Token = webModel.Token;
                    c.Password = webModel.Password;
                    c.ConfirmPassword = webModel.ConfirmPassword;
                    c.UserId = webModel.User;
                })
                .HandleAsync();

            if (!result.HasSucceeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        //    [HttpPost, Route(Route + "request-change-password")]
        //    public async Task<IActionResult> RequestChangePassword(ChangePasswordWebModel webModel)
        //    {
        //        var result = await _operationFactory
        //            .Create<ChangePasswordCommand>(c =>
        //            {
        //                c.CurrentPassword = webModel.CurrentPassword;
        //                c.Password = webModel.Password;
        //                c.ConfirmPassword = webModel.ConfirmPassword;
        //            })
        //            .HandleAsync();

        //        if (!result.HasSucceeded)
        //        {
        //            return BadRequest(result.Errors);
        //        }

        //        return Ok();
        //    }

        //    [HttpPost, Route(Route + "change-password")]
        //    public async Task<IActionResult> ChangePassword(ChangePasswordWebModel webModel)
        //    {
        //        var result = await _operationFactory
        //            .Create<ChangePasswordCommand>(c =>
        //            {
        //                c.CurrentPassword = webModel.CurrentPassword;
        //                c.Password = webModel.Password;
        //                c.ConfirmPassword = webModel.ConfirmPassword;
        //            })
        //            .HandleAsync();

        //        if (!result.HasSucceeded)
        //        {
        //            return BadRequest(result.Errors);
        //        }

        //        return Ok();
        //    }

        //    [HttpPost, Route(Route + "request-change-email")]
        //    public async Task<IActionResult> RequestForChangeEmail(RequestForChangeEmailWebModel webModel)
        //    {
        //        var result = await _operationFactory
        //            .Create<RequestForChangeEmailCommand>(c =>
        //            {
        //                c.Email = webModel.Email.Trim().ToLower();
        //                c.CurrentPassword = webModel.CurrentPassword;
        //            })
        //            .HandleAsync();

        //        if (!result.HasSucceeded)
        //        {
        //            return BadRequest(result.Errors);
        //        }

        //        return Ok();
        //    }

        //    [HttpPost, Route(Route + "change-email")]
        //    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailWebModel webModel)
        //    {
        //        var result = await _operationFactory
        //            .Create<ChangeEmailCommand>(c =>
        //            {
        //                c.Email = webModel.Email.Trim().ToLower();
        //                c.Token = webModel.Token;
        //                c.UserId = webModel.User;
        //            })
        //            .HandleAsync();

        //        if (!result.HasSucceeded)
        //        {
        //            return BadRequest(result.Errors);
        //        }

        //        await _signInManager.SignOutAsync(IdentityConstants.ApplicationScheme);

        //        return Ok();
        //    }

        //    [HttpPost, Route(Route + "confirm-email")]
        //    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailWebModel webModel)
        //    {
        //        var result = await _operationFactory
        //            .Create<ConfirmEmailCommand>(c =>
        //            {
        //                c.Token = webModel.Token;
        //                c.UserId = webModel.User;
        //            })
        //            .HandleAsync();

        //        if (!result.HasSucceeded)
        //        {
        //            return BadRequest(result.Errors);
        //        }

        //        return Ok();
        //    }
        //}
    }
}