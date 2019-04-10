using System;
using System.Collections.Generic;
using System.Text;

namespace penCsharpener.DotnetUtils {
    public static class EnumExtensions {

        #region https://www.danylkoweb.com/Blog/10-extremely-useful-net-extension-methods-8J

        public static bool Has<T>(this System.Enum type, T value) {
            try {
                return (((int)(object)type & (int)(object?)value) == (int)(object?)value);
            } catch {
                return false;
            }
        }

        public static bool Is<T>(this System.Enum type, T value) {
            try {
                return (int)(object)type == (int)(object?)value;
            } catch {
                return false;
            }
        }

        public static T Add<T>(this System.Enum type, T value) {
            try {
                return (T)(object)(((int)(object)type | (int)(object?)value));
            } catch (Exception ex) {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

        public static T Remove<T>(this System.Enum type, T value) {
            try {
                return (T)(object)(((int)(object)type & ~(int)(object?)value));
            } catch (Exception ex) {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

        #endregion

    }
}
