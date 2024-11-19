using System.Security.Claims;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Backend.WebApi.Infrastructure.Services;

public class AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUserService
{
    public string UserId { get; } = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string UserName { get; } = httpContextAccessor.HttpContext?.User.Identity?.Name;
}