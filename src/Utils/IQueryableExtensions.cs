using System.Linq.Expressions;

namespace IWantApp.Utils;

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderByDynamicInternal(propertyName, ascending: true);
    }

    public static IQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderByDynamicInternal(propertyName, ascending: false);
    }

    private static IQueryable<T> OrderByDynamicInternal<T>(this IQueryable<T> source, string propertyName, bool ascending)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var selector = Expression.Lambda(property, parameter);

        var method = ascending ? "OrderBy" : "OrderByDescending";
        var expression = Expression.Call(typeof(Queryable), method,
            new Type[] { source.ElementType, property.Type },
            source.Expression, Expression.Quote(selector));

        return source.Provider.CreateQuery<T>(expression);
    }
}
