using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Backend.Application.Parameters;

public class PaginationSortFilterRequestParameter<TDto>
{
    public PaginationSortFilterRequestParameter()
    {
        PageNumber = 1;
        PageSize = 20;
        SortCriteria = new List<SortCriterion<TDto>>();
        Filters = new List<FilterCriterion<TDto>>();
    }

    public PaginationSortFilterRequestParameter(
        int pageNumber,
        int pageSize,
        List<SortCriterion<TDto>> sortCriteria,
        List<FilterCriterion<TDto>> filters)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize;
        SortCriteria = sortCriteria ?? new List<SortCriterion<TDto>>();
        Filters = filters ?? new List<FilterCriterion<TDto>>();
    }

    // Pagination properties
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    // Sorting properties
    public List<SortCriterion<TDto>> SortCriteria { get; set; }

    // Filtering properties
    public List<FilterCriterion<TDto>> Filters { get; set; }
}

// Sort criterion with a property selector
public class SortCriterion<TDto>
{
    public Expression<Func<TDto, object>> PropertySelector { get; set; }
    public bool Desc { get; set; } // True for descending, false for ascending
}

// Filter criterion with a property selector and value
public class FilterCriterion<TDto>
{
    public Expression<Func<TDto, object>> PropertySelector { get; set; }
    public FilterOperation Operation { get; set; }
    public object Value { get; set; }

    public Expression<Func<TEntity, bool>> ToExpression<TEntity>()
    {
        // Get the property name from the PropertySelector
        var propertyName = ((MemberExpression)PropertySelector.Body).Member.Name;

        // Create the parameter for the lambda (e.g., "x" in x => x.Property)
        var parameter = Expression.Parameter(typeof(TEntity), "x");

        // Get the property to filter on (e.g., x.Property)
        var property = Expression.Property(parameter, propertyName);

        // Create the value to compare (e.g., "Value" in x.Property == Value)
        var constant = Expression.Constant(Value);

        // Build the operation (e.g., ==, >, <, Contains)
        Expression comparison = Operation switch
        {
            FilterOperation.Equals => Expression.Equal(property, constant),
            FilterOperation.NotEquals => Expression.NotEqual(property, constant),
            FilterOperation.GreaterThan => Expression.GreaterThan(property, constant),
            FilterOperation.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
            FilterOperation.LessThan => Expression.LessThan(property, constant),
            FilterOperation.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
            FilterOperation.Contains => Expression.Call(
                property,
                typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                constant),
            _ => throw new NotSupportedException($"Filter operation {Operation} is not supported.")
        };

        // Build and return the complete lambda expression
        return Expression.Lambda<Func<TEntity, bool>>(comparison, parameter);
    }
}

public enum FilterOperation
{
    Equals,
    NotEquals,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Contains
}