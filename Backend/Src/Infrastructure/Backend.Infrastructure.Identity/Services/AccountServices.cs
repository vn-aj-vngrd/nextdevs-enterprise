using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Application.DTOs.Account.Requests;
using Backend.Application.DTOs.Account.Responses;
using Backend.Application.Helpers;
using Backend.Application.Interfaces;
using Backend.Application.Interfaces.UserInterfaces;
using Backend.Application.Wrappers;
using Backend.Infrastructure.Identity.Models;
using Backend.Infrastructure.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Infrastructure.Identity.Services;

public class AccountServices(
    UserManager<ApplicationUser> userManager,
    IAuthenticatedUserService authenticatedUser,
    SignInManager<ApplicationUser> signInManager,
    JwtSettings jwtSettings,
    ITranslator translator) : IAccountServices
{
    public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
    {
        var user = await userManager.FindByIdAsync(authenticatedUser.UserId);

        var token = await userManager.GeneratePasswordResetTokenAsync(user!);
        var identityResult = await userManager.ResetPasswordAsync(user, token, model.Password);

        if (identityResult.Succeeded)
            return BaseResult.Ok();

        return identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)).ToList();
    }

    public async Task<BaseResult> ChangeUserName(ChangeUserNameRequest model)
    {
        var user = await userManager.FindByIdAsync(authenticatedUser.UserId);

        user.UserName = model.UserName;

        var identityResult = await userManager.UpdateAsync(user);

        if (identityResult.Succeeded)
            return BaseResult.Ok();

        return identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)).ToList();
    }

    public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return new Error(ErrorCode.NotFound,
                translator.GetString(
                    TranslatorMessages.AccountMessages.Account_NotFound_with_UserName(request.UserName)),
                nameof(request.UserName));

        var signInResult = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
        if (!signInResult.Succeeded)
            return new Error(ErrorCode.FieldDataInvalid,
                translator.GetString(TranslatorMessages.AccountMessages.Invalid_password()), nameof(request.Password));

        return await GetAuthenticationResponse(user);
    }

    public async Task<BaseResult<AuthenticationResponse>> AuthenticateByUserName(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null)
            return new Error(ErrorCode.NotFound,
                translator.GetString(TranslatorMessages.AccountMessages.Account_NotFound_with_UserName(username)),
                nameof(username));

        return await GetAuthenticationResponse(user);
    }

    public async Task<BaseResult<string>> RegisterGhostAccount()
    {
        var user = new ApplicationUser
        {
            UserName = GenerateRandomString(7)
        };

        var identityResult = await userManager.CreateAsync(user);

        if (identityResult.Succeeded)
            return user.UserName;

        return identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)).ToList();

        string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public async Task<BaseResult<UserDto>> GetProfile()
    {
        var user = await userManager.FindByIdAsync(authenticatedUser.UserId);

        if (user == null)
            return new Error(ErrorCode.NotFound,
                translator.GetString(
                    TranslatorMessages.AccountMessages.Account_NotFound_with_UserId(authenticatedUser.UserId)),
                nameof(authenticatedUser.UserId));

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Name = user.Name,
            Created = user.Created,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }

    private async Task<AuthenticationResponse> GetAuthenticationResponse(ApplicationUser user)
    {
        await userManager.UpdateSecurityStampAsync(user);

        var jwToken = await GenerateJwtToken();

        var rolesList = await userManager.GetRolesAsync(user);

        return new AuthenticationResponse
        {
            Id = user.Id.ToString(),
            JwToken = new JwtSecurityTokenHandler().WriteToken(jwToken),
            Email = user.Email,
            UserName = user.UserName,
            Roles = rolesList,
            IsVerified = user.EmailConfirmed
        };

        async Task<JwtSecurityToken> GenerateJwtToken()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                (await signInManager.ClaimsFactory.CreateAsync(user)).Claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
        }
    }
}