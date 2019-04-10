using System;
using System.Collections.Generic;

namespace penCsharpener.DotnetUtils {
    public static class StringExtensions {

        /// <summary>
        /// same as string.IsNullOrEmpty(str);
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string? str) {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Case insensitive fuzzy filter.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static bool Like(this string? str, string? searchText) {
            if (str == null && str == searchText) return true;
            if (str == null) return false;
            return str.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// To avoid nested lists or arrays 'partsString' is split by 'delimiter' and each 
        /// part is check one by one whether 'str' contains that part.
        /// To return true all parts must be contained in 'str'. 
        /// This function is not case sensitive.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="partsString"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static bool ContainsAllParts(this string str, string partsString, char delimiter) {
            if (str.IsNullOrEmpty() && !partsString.IsNullOrEmpty()) return false;
            if (str.IsNullOrEmpty() && partsString.IsNullOrEmpty()) return true;

            var parts = partsString.Split(delimiter);
            foreach (var part in parts) {
                if (!str.Like(part)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks a string 'str' whether it is contained in a string 'collection'.
        /// Optionally allows for wildcard in form of a delimiter in path.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="collection"></param>
        /// <param name="containsAllPartsDelimiter">Represents wildcard with which strings are split.</param>
        /// <returns></returns>
        public static bool InIgnoreCase(this string str,
                                        IEnumerable<string> collection,
                                        char? containsAllPartsDelimiter = null) {
            if (containsAllPartsDelimiter.HasValue) {
                foreach (var part in collection) {
                    if (str.ContainsAllParts(part, containsAllPartsDelimiter.Value)) return true;
                }
            } else {
                foreach (var part in collection) {
                    if (str.Like(part)) return true;
                }
            }
            return false;
        }

        public static string ToMySqlDate(this DateTime datetime, bool withSingleQuotes = true) {
            var singleQuote = withSingleQuotes ? "'" : null;
            return singleQuote + datetime.ToString("yyyy-MM-dd HH:mm:ss") + singleQuote;
        }
    }
}
