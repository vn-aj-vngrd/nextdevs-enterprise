using System.Threading.Tasks;
using Backend.Application.DTOs.Account.Requests;
using Backend.Application.DTOs.Account.Responses;
using Backend.Application.Wrappers;

namespace Backend.Application.Interfaces.UserInterfaces;

public interface IGetUserServices
{
    Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model);
}