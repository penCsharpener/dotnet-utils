using System.IO;
using System.Security.Cryptography;

namespace penCsharpener.DotnetUtils {
    public static class HashExtensions {

        public static string ToMD5(this FileInfo fileInfo) {
            using (var sha1 = new MD5CryptoServiceProvider()) {
                var byteHash = sha1.ComputeHash(fileInfo.ToBytes());
                return byteHash.ToHex();
            }
        }

        public static string ToSha1(this FileInfo fileInfo) {
            using (var sha1 = new SHA1Managed()) {
                var byteHash = sha1.ComputeHash(fileInfo.ToBytes());
                return byteHash.ToHex();
            }
        }

        public static string ToSha256(this FileInfo fileInfo) {
            using (var sha1 = new SHA256Managed()) {
                var byteHash = sha1.ComputeHash(fileInfo.ToBytes());
                return byteHash.ToHex();
            }
        }

        public static string ToSha512(this FileInfo fileInfo) {
            using (var sha1 = new SHA512Managed()) {
                var byteHash = sha1.ComputeHash(fileInfo.ToBytes());
                return byteHash.ToHex();
            }
        }

        public static string ToMD5(this string str) {
            using (var sha1 = new MD5CryptoServiceProvider()) {
                var byteHash = sha1.ComputeHash(str.ToUTF8Bytes());
                return byteHash.ToHex();
            }
        }

        public static string ToSha1(this string str) {
            using (var sha1 = new SHA1Managed()) {
                var byteHash = sha1.ComputeHash(str.ToUTF8Bytes());
                return byteHash.ToHex();
            }
        }

        public static string ToSha256(this string str) {
            using (var sha1 = new SHA256Managed()) {
                var byteHash = sha1.ComputeHash(str.ToUTF8Bytes());
                return byteHash.ToHex();
            }
        }

        public static string ToSha512(this string str) {
            using (var sha1 = new SHA512Managed()) {
                var byteHash = sha1.ComputeHash(str.ToUTF8Bytes());
                return byteHash.ToHex();
            }
        }
    }
}
