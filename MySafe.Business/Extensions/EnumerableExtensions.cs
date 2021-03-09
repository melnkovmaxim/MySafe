using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Business.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool TryCast<TSource, TResult>(this IEnumerable<TSource> enumerable, out IEnumerable<TResult> result)
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
