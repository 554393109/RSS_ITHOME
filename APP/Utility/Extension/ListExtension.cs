namespace APP.Utility.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtension
    {
        public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var query = source.Select(selector);
            return query.ToList();
        }

        /// <summary>
        /// IList -> List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list_old"></param>
        /// <returns></returns>
        public static List<T> ConvertToCollectionList<T>(this IList<T> list_old)
            where T : new()
        {
            List<T> list_new = new List<T>();
            if (list_old == null)
                return list_new;

            list_new.AddRange(list_old);
            return list_new;
        }

        /// <summary>
        /// string.Join(";", list);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IList<T> list, string separator = ";")
        {
            var result = string.Empty;

            try
            {
                if (list != null || list.Count > 0)
                    result = string.Join(separator, list);
            }
            catch /*(Exception ex)*/
            {
            }

            return result;
        }
    }
}
