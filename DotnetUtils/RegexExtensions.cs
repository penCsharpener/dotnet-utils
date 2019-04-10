using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace penCsharpener.DotnetUtils {
    public static class RegexExtensions {

        #region http://extensionmethod.net/5633/csharp/string/ismatchregex

        public static bool IsMatchRegex(this string value, string pattern) {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        public static T[] REExtract<T>(this string s, string regex) {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
            if (!tc.CanConvertFrom(typeof(string))) {
                throw new ArgumentException("Type does not have a TypeConverter from string", "T");
            }
            if (!string.IsNullOrEmpty(s)) {
                return Regex.Matches(s, regex)
                        .Cast<Match>()
                        .Select(f => f.ToString())
                        .Select(f => (T)tc.ConvertFrom(f))
                        .ToArray();
            } else {
                return new T[0];
            }
        }

        #endregion

    }
}
