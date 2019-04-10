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
    }
}
