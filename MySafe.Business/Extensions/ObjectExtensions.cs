using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Business.Extensions
{
    public static class ObjectExtensions
    {
        public static bool TryCastToCollection<TSource, TResult>(this object enumerable, out IEnumerable<TResult> result)
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
