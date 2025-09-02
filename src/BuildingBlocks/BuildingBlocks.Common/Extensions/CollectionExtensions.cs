using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Common.Extensions;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T>? collection)
    {
        return collection is null || collection.Count == 0;
    }
}
