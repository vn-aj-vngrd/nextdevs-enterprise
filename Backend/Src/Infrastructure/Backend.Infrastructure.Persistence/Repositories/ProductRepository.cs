using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces.Repositories;
using Backend.Application.Parameters;
using Backend.Domain.Products.DTOs;
using Backend.Domain.Products.Entities;
using Backend.Infrastructure.Persistence.Contexts;

namespace Backend.Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext dbContext)
    : GenericRepository<Product>(dbContext), IProductRepository
{
    public async Task<PaginationResponseDto<ProductDto>> GetPagedListAsync(
        string name,
        int pageNumber,
        int pageSize,
        List<SortCriterion<ProductDto>> sortCriteria,
        List<FilterCriterion<ProductDto>> filters)
    {
        // Start with the base query
        var query = dbContext.Products.AsQueryable();

        // Apply name filter
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(p => p.Name.Contains(name));

        // Apply dynamic filters
        if (filters != null && filters.Any())
            foreach (var filter in filters)
                query = query.Where(filter.ToExpression<Product>());

        // Apply dynamic sorting
        if (sortCriteria != null && sortCriteria.Any())
            query = ApplySorting(query, sortCriteria);
        else
            // Default sorting if no criteria provided
            query = query.OrderBy(p => p.Created);

        // Project to DTO and apply pagination
        var projectedQuery = query.Select(p => new ProductDto(p));
        return await Paged(projectedQuery, pageNumber, pageSize);
    }

    private IQueryable<T> ApplySorting<T, TDto>(
        IQueryable<T> query,
        List<SortCriterion<TDto>> sortCriteria)
    {
        if (sortCriteria == null || !sortCriteria.Any())
            return query;

        IOrderedQueryable<T> orderedQuery = null;

        for (var i = 0; i < sortCriteria.Count; i++)
        {
            var criterion = sortCriteria[i];

            // Validate the property exists on T
            var property = typeof(T).GetProperty(criterion.PropertyName);
            if (property == null)
                throw new ArgumentException(
                    $"Property '{criterion.PropertyName}' does not exist on type '{typeof(T).Name}'.");

            // Create parameter for lambda expression (e.g., "x => x.PropertyName")
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, criterion.PropertyName);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            // Apply sorting
            if (i == 0)
                orderedQuery = criterion.Desc
                    ? Queryable.OrderByDescending(query, (dynamic)lambda)
                    : Queryable.OrderBy(query, (dynamic)lambda);
            else
                orderedQuery = criterion.Desc
                    ? Queryable.ThenByDescending(orderedQuery, (dynamic)lambda)
                    : Queryable.ThenBy(orderedQuery, (dynamic)lambda);
        }

        return orderedQuery ?? query;
    }
}