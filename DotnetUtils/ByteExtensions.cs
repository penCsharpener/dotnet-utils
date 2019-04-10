using System;
using System.IO;
using System.Linq;
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
    }
}
