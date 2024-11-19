using Backend.Application.Parameters;

namespace Backend.Application.DTOs.Account.Requests;

public class GetAllUsersRequest : PaginationRequestParameter
{
    public string Name { get; set; }
}