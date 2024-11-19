using System.Threading.Tasks;
using Backend.Application.DTOs.Account.Requests;
using Backend.Application.DTOs.Account.Responses;
using Backend.Application.Wrappers;

namespace Backend.Application.Interfaces.UserInterfaces;

public interface IAccountServices
{
    Task<BaseResult<string>> RegisterGhostAccount();
    Task<BaseResult> ChangePassword(ChangePasswordRequest model);
    Task<BaseResult> ChangeUserName(ChangeUserNameRequest model);
    Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
    Task<BaseResult<AuthenticationResponse>> AuthenticateByUserName(string username);
}