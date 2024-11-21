using System.Threading.Tasks;
using Backend.Application.DTOs.Account.Requests;
using Backend.Application.DTOs.Account.Responses;
using Backend.Application.Interfaces.UserInterfaces;
using Backend.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers.v1;

[ApiVersion("1")]
public class AccountController(IAccountServices accountServices) : BaseApiController
{
    [HttpPost]
    public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
    {
        return await accountServices.Authenticate(request);
    }

    [HttpPut]
    [Authorize]
    public async Task<BaseResult> ChangeUserName(ChangeUserNameRequest model)
    {
        return await accountServices.ChangeUserName(model);
    }

    [HttpPut]
    [Authorize]
    public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
    {
        return await accountServices.ChangePassword(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<BaseResult<UserDto>> GetProfile()
    {
        return await accountServices.GetProfile();
    }

    [HttpPost]
    public async Task<BaseResult<AuthenticationResponse>> Start()
    {
        var ghostUsername = await accountServices.RegisterGhostAccount();
        return await accountServices.AuthenticateByUserName(ghostUsername.Data);
    }

    [HttpGet]
    [Authorize]
    public async Task<BaseResult> CheckTokenValidity()
    {
        return await accountServices.CheckTokenValidity();
    }
}