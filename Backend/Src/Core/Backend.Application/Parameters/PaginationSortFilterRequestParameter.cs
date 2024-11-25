using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Backend.Application.Parameters;

public class PaginationSortFilterRequestParameter<TDto>
{
    public PaginationSortFilterRequestParameter()
    {
        PageNumber = 1;
        PageSize = 10;
        SortCriteria = new List<SortCriterion<TDto>>();
        FilterCriteria = new List<FilterCriterion<TDto>>();
    }

    public PaginationSortFilterRequestParameter(
        int pageNumber,
        int pageSize,
        List<SortCriterion<TDto>> sortCriteria,
        List<FilterCriterion<TDto>> filterCriteria)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize;
        SortCriteria = sortCriteria ?? new List<SortCriterion<TDto>>();
        FilterCriteria = filterCriteria ?? new List<FilterCriterion<TDto>>();
    }

    // Pagination properties
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    // Sorting properties
    public List<SortCriterion<TDto>> SortCriteria { get; set; }

    // Filtering properties
    public List<FilterCriterion<TDto>> FilterCriteria { get; set; }
}

// Sort criterion with a string property name
public class SortCriterion<TDto>
{
    private string _propertyName;

    public string PropertyName
    {
        get => _propertyName;
        set
        {
            ValidatePropertyName(value);
            _propertyName = value;
        }
    }

    public bool Desc { get; set; } // True for descending, false for ascending

    private static void ValidatePropertyName(string propertyName)
    {
        if (typeof(TDto).GetProperty(propertyName) == null)
            throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(TDto).Name}'.");
    }
}

// Filter criterion with a string property name
public class FilterCriterion<TDto>
{
    private string _propertyName;

    public string PropertyName
    {
        get => _propertyName;
        set
        {
            ValidatePropertyName(value);
            _propertyName = value;
        }
    }

    public FilterOperation Operation { get; set; }
    public object Value { get; set; }

    public Expression<Func<TEntity, bool>> ToExpression<TEntity>()
    {
        // Get the property to filter on (e.g., x.Property)
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, PropertyName);

        // Create the value to compare
        var constant = Expression.Constant(Value);

        // Build the operation
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

    private static void ValidatePropertyName(string propertyName)
    {
        if (typeof(TDto).GetProperty(propertyName) == null)
            throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(TDto).Name}'.");
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