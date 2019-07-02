using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// source: https://stackoverflow.com/a/271411/6454517
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string F(this string str, params object[] args) {
            return string.Format(str, args);
        }

        #region https://www.danylkoweb.com/Blog/10-extremely-useful-net-extension-methods-8J

        public static string ToFileSize(this long size) {
            if (size < 1024) { return (size).ToString("F0") + " bytes"; }
            if (size < Math.Pow(1024, 2)) { return (size / 1024).ToString("F0") + "KB"; }
            if (size < Math.Pow(1024, 3)) { return (size / Math.Pow(1024, 2)).ToString("F0") + "MB"; }
            if (size < Math.Pow(1024, 4)) { return (size / Math.Pow(1024, 3)).ToString("F0") + "GB"; }
            if (size < Math.Pow(1024, 5)) { return (size / Math.Pow(1024, 4)).ToString("F0") + "TB"; }
            if (size < Math.Pow(1024, 6)) { return (size / Math.Pow(1024, 5)).ToString("F0") + "PB"; }
            return (size / Math.Pow(1024, 6)).ToString("F0") + "EB";
        }

        public static string RemoveLastCharacter(this string instr) {
            return instr.Substring(0, instr.Length - 1);
        }

        public static string RemoveLast(this string instr, int number) {
            return instr.Substring(0, instr.Length - number);
        }

        public static string RemoveFirstCharacter(this string instr) {
            return instr.Substring(1);
        }

        public static string RemoveFirst(this string instr, int number) {
            return instr.Substring(number);
        }

        public static Stream ToUTF8Stream(this string str) {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(str);
            //byte[] byteArray = Encoding.ASCII.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        public static Stream ToStream(this string str, System.Text.Encoding encoding) {
            byte[] byteArray = encoding.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        #endregion

        #region https://dzone.com/articles/5-more-c-extension-methods-for-the-stocking-plus-a

        public static string OrdinalSuffix(this DateTime datetime) {
            int day = datetime.Day;
            if (day % 100 >= 11 && day % 100 <= 13)
                return string.Concat(day, "th");
            switch (day % 10) {
                case 1:
                    return string.Concat(day, "st");
                case 2:
                    return string.Concat(day, "nd");
                case 3:
                    return string.Concat(day, "rd");
                default:
                    return string.Concat(day, "th");
            }
        }

        public static string Join<T>(this IEnumerable<T> self, string separator) {
            return string.Join(separator, self.Where(e => e != null).Select(e => e!.ToString()).ToArray());
        }

        public static string Join(this Array array, string separator) {
            return string.Join(separator, array);
        }

        #endregion

        /// <summary>
        /// http://extensionmethod.net/5626/csharp/string/camelcasetohumancase
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitCamelCase(this string source) {
            const string pattern = @"[A-Z][a-z]*|[a-z]+|\d+";
            var matches = Regex.Matches(source, pattern);
            foreach (Match match in matches) {
                yield return match.Value;
            }
        }

        /// <summary>
        /// http://extensionmethod.net/5626/csharp/string/camelcasetohumancase
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CamelCaseToHumanCase(this string source) {
            var words = source.SplitCamelCase();
            string humanCased = string.Join(" ", words);
            return humanCased;
        }

        /// <summary>
        /// Returns characters from right of specified length
        /// http://extensionmethod.net/2022/csharp/string/string-extensions
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from right</returns>
        public static string Right(this string value, int length) {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }

        /// <summary>
        /// Returns characters from left of specified length
        /// http://extensionmethod.net/2022/csharp/string/string-extensions
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from left</returns>
        public static string Left(this string value, int length) {
            return value != null && value.Length > length ? value.Substring(0, length) : value;
        }

        /// <summary>
        /// http://extensionmethod.net/1602/csharp/string/topropercase
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToProperCase(this string text) {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        }

        /// <summary>
        /// https://gist.github.com/machupicchubeta/10016121
        /// </summary>
        public static string SnakeToPascalCase(this string str) {
            var snakeParts = str.Split("_".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return snakeParts.Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                .Aggregate(string.Empty, (s1, s2) => s1 + s2);
        }

        public static bool IsNumeric(this string str, out int i) {
            return int.TryParse(str, out i);
        }

        public static bool IsNumeric(this string str) {
            return int.TryParse(str, out int i);
        }
    }
}
