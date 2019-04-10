using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace penCsharpener.DotnetUtils {
    public static class LinqExtensions {

        /// <summary>
        /// source: https://stackoverflow.com/a/833477/6454517
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In<T>(this T source, params T[] list) {
            if (source == null) throw new ArgumentNullException("source");
            return list.Contains(source);
            // alternatively do this: (new T[] { 1, 2, 3 }).Contains(a) 
        }

        /// <summary>
        /// http://extensionmethod.net/5635/csharp/ilist-t/chainable-list-add-typesafe
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TList Push<TList, TItem>(this TList list, TItem item) where TList : IList<TItem> {
            list.Add(item);
            return list;
        }

        /// <summary>
        /// Appends a value to the end of the sequence. 
        /// http://extensionmethod.net/5619/csharp/ienumerableoft/append-and-prepend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="element">The value to append to source.</param>
        /// <returns>A new sequence that ends with element.</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element) {
            foreach (var item in source) {
                yield return item;
            }
            yield return element;
        }

        /// <summary>
        /// Adds a value to the beginning of the sequence.
        /// http://extensionmethod.net/5619/csharp/ienumerableoft/append-and-prepend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="element">The value to prepend to source.</param>
        /// <returns>A new sequence that begins with element</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T element) {
            yield return element;
            foreach (var item in source) {
                yield return item;
            }
        }

        /// <summary>
        /// http://extensionmethod.net/1735/csharp/ienumerable-t/whereif
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate) {
            if (condition) return source.Where(predicate);
            else return source;
        }

        /// <summary>
        /// http://extensionmethod.net/1735/csharp/ienumerable-t/whereif
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate) {
            if (condition) return source.Where(predicate);
            else return source;
        }

        /// <summary>
        /// http://extensionmethod.net/2067/csharp/ienumerable-t/foreach-3
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> act) {
            foreach (var i in array) act(i);
            return array;
        }

        /// <summary>
        /// http://extensionmethod.net/2067/csharp/ienumerable-t/foreach-3
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="RT"></typeparam>
        /// <param name="array"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<RT> ForEach<T, RT>(this IEnumerable<T> array, Func<T, RT> func) {
            var list = new List<RT>();
            foreach (var i in array) {
                var obj = func(i);
                if (obj != null)
                    list.Add(obj);
            }
            return list;
        }
    }
}
