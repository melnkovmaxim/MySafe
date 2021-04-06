using System.Collections.Generic;
using System.Linq;

namespace MySafe.Services.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool TryCast<TSource, TResult>(this IEnumerable<TSource> enumerable,
            out IEnumerable<TResult> result)
        {
            result = null;

            try
            {
                result = enumerable.Cast<TResult>().ToList();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}