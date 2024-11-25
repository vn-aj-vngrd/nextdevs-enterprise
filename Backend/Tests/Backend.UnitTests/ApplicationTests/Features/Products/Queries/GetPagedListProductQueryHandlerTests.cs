using Backend.Application.DTOs;
using Backend.Application.Features.Products.Queries.GetPagedListProduct;
using Backend.Application.Interfaces.Repositories;
using Backend.Application.Parameters;
using Backend.Domain.Products.DTOs;
using Moq;
using Shouldly;

namespace Backend.UnitTests.ApplicationTests.Features.Products.Queries;

public class GetPagedListProductQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsPagedListResponse()
    {
        // Arrange
        var name = "Test";
        var pageNumber = 1;
        var pageSize = 10;
        var sortCriteria = new List<SortCriterion<ProductDto>>
        {
            new()
            {
                PropertyName = "Name",
                Desc = false
            }
        };
        var filterCriteria = new List<FilterCriterion<ProductDto>>
        {
            new()
            {
                PropertyName = "Price",
                Operation = FilterOperation.GreaterThan,
                Value = "1000"
            }
        };


        var products = new List<ProductDto>
        {
            new() { Id = 1, Name = "Product 1", Price = 1000 },
            new() { Id = 2, Name = "Product 2", Price = 1500 }
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(repo =>
                repo.GetPagedListAsync(name, pageNumber, pageSize, sortCriteria, filterCriteria))
            .ReturnsAsync(new PaginationResponseDto<ProductDto>(products, 100, pageNumber, pageSize));

        var handler = new GetPagedListProductQueryHandler(productRepositoryMock.Object);

        var query = new GetPagedListProductQuery
        {
            Name = name,
            PageNumber = pageNumber,
            PageSize = pageSize,
            SortCriteria = sortCriteria,
            FilterCriteria = filterCriteria
        };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Data.ShouldNotBeNull();
        result.Data.ShouldBeEquivalentTo(products);
        result.PageNumber.ShouldBe(pageNumber);
        result.PageSize.ShouldBe(pageSize);
    }
}