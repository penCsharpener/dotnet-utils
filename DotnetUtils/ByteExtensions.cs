using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace penCsharpener.DotnetUtils {
    public static class ByteExtensions {

        public static byte[] ToUTF8Bytes(this string str) {
            return Encoding.UTF8.GetBytes(str);
        }

        public static byte[] ToBytes(this string str, Encoding encoding) {
            return encoding.GetBytes(str);
        }

        public static string ToString(this byte[] bytes, Encoding encoding) {
            return encoding.GetString(bytes, 0, bytes.Length);
        }

        public static byte[] ToBytes(this FileInfo fileInfo) {
            return File.ReadAllBytes(fileInfo.FullName);
        }

        public static string ToBase64(this FileInfo fileInfo) {
            return Convert.ToBase64String(File.ReadAllBytes(fileInfo.FullName));
        }

        public static string ToHex(this byte[] bytes) {
            return string.Concat(bytes.Select(x => x.ToString("X2"))).ToLower();
        }

        public static string ToBase64(this byte[] bytes) {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] Serialize<T>(this T obj) where T : class {
            var binFormatter = new BinaryFormatter();
            using (var ms = new MemoryStream()) {
                binFormatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T? Deserialize<T>(this byte[] bytes) where T : class {
            using (var ms = new MemoryStream(bytes)) {
                var binFormatter = new BinaryFormatter();
                var obj = binFormatter.Deserialize(ms);
                return obj as T;
            }
        }

        #region https://www.danylkoweb.com/Blog/10-extremely-useful-net-extension-methods-8J

        public static string ToString(this Stream stream) {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Copy from one stream to another.
        /// Example:
        /// using(var stream = response.GetResponseStream())
        /// using(var ms = new MemoryStream())
        /// {
        ///     stream.CopyTo(ms);
        ///      // Do something with copied data
        /// }
        /// </summary>
        /// <param name="fromStream">From stream.</param>
        /// <param name="toStream">To stream.</param>
        public static void CopyTo(this Stream fromStream, Stream toStream) {
            if (fromStream == null)
                throw new ArgumentNullException(nameof(fromStream));
            if (toStream == null)
                throw new ArgumentNullException(nameof(toStream));
            var bytes = new byte[8092];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
                toStream.Write(bytes, 0, dataRead);
        }

        #endregion
    }
}
