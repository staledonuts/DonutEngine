namespace Engine.Utils.Extensions;
using System.Collections.Generic;

internal static class EnumerableExtensions
{
    internal static string Join<T>(this IEnumerable<T> collection, string separator)
    {
        return string.Join(separator, collection);
    }

    internal static string JoinWithSpace<T>(this IEnumerable<T> collection)
    {
        return collection.Join(" ");
    }
}