namespace Backend.Application.Parameters;

public class PaginationRequestParameter
{
    public PaginationRequestParameter()
    {
        PageNumber = 1;
        PageSize = 20;
    }

    public PaginationRequestParameter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}