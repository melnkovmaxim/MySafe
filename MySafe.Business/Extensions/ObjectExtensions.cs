using System.Collections.Generic;
using System.Linq;

namespace MySafe.Services.Extensions
{
    public static class ObjectExtensions
    {
        public static bool TryCastToCollection<TSource, TResult>(this object enumerable,
            out IEnumerable<TResult> result)
        {
            result = null;

            try
            {
                var sourceCollection = (IEnumerable<TSource>) enumerable;
                result = sourceCollection.Cast<TResult>().ToList();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}