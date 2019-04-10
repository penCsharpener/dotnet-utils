using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace penCsharpener.DotnetUtils {
    public static class OtherExtensions {

        public static bool IsNull(this object obj) => obj == null;
        public static bool IsNotNull(this object obj) => obj != null;

        #region https://dzone.com/articles/5-more-c-extension-methods-for-the-stocking-plus-a

        public static int ToInt(this string input) {
            int result;
            int.TryParse(input, out result);
            return result;
        }

        /// <summary>
        /// Makes a copy from the object.
        /// Doesn't copy the reference memory, only data.
        /// http://extensionmethod.net/1706/csharp/object/clone-t
        /// </summary>
        /// <typeparam name="T">Type of the return object.</typeparam>
        /// <param name="item">Object to be copied.</param>
        /// <returns>Returns the copied object.</returns>
        public static T Clone<T>(this object item) {
            if (item != null) {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();

                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);

                T result = (T)formatter.Deserialize(stream);

                stream.Close();

                return result;
            } else return default(T)!;
        }

        #endregion
    }
}
