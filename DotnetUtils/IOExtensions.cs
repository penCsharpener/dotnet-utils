using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace penCsharpener.DotnetUtils {
    public static class IOExtensions {

        private static List<FileInfo> fileList;
        public static IEnumerable<FileInfo> GetFiles(this DirectoryInfo dirInfo,
                                                                string? searchPattern = null,
                                                                string[]? ignorePaths = null,
                                                                char? containsPartsDelimiter = null,
                                                                int depth = -1,
                                                                Action<UnauthorizedAccessException>? unauthExceptionCallback = null,
                                                                Action<Exception>? exceptionCallback = null) {
            fileList = new List<FileInfo>();
            dirInfo.GetFilesRecursively(searchPattern: searchPattern,
                                        ignorePaths: ignorePaths,
                                        containsPartsDelimiter: containsPartsDelimiter,
                                        depth: depth,
                                        unauthExceptionCallback: unauthExceptionCallback,
                                        exceptionCallback: exceptionCallback);
            return fileList;
        }

        private static void GetFilesRecursively(this DirectoryInfo dirInfo,
                                                            string? searchPattern = null,
                                                            string[]? ignorePaths = null,
                                                            char? containsPartsDelimiter = null,
                                                            int depth = -1,
                                                            Action<UnauthorizedAccessException>? unauthExceptionCallback = null,
                                                            Action<Exception>? exceptionCallback = null) {
            var files = new List<FileInfo>();
            if (depth == 0) {
                return;
            }
            var dirInfos = new List<DirectoryInfo>();

            if (ignorePaths != null && dirInfo.FullName.InIgnoreCase(ignorePaths, containsPartsDelimiter)) {
                return;
            }

            try {
                if (searchPattern.IsNullOrEmpty()) {
                    files.AddRange(dirInfo.GetFiles());
                    dirInfos.AddRange(dirInfo.GetDirectories());
                } else {
                    foreach (var part in searchPattern.Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries)) {
                        files.AddRange(dirInfo.GetFiles(part, SearchOption.TopDirectoryOnly));
                    }
                    dirInfos.AddRange(dirInfo.GetDirectories());
                }
            } catch (UnauthorizedAccessException unAuthEx) {
                unauthExceptionCallback?.Invoke(unAuthEx);
            } catch (Exception ex) {
                exceptionCallback?.Invoke(ex);
            }
            foreach (var dir in dirInfos) {
                dir.GetFilesRecursively(searchPattern,
                                        ignorePaths: ignorePaths,
                                        containsPartsDelimiter: containsPartsDelimiter,
                                        depth: --depth);
            }

            fileList.AddRange(files);
            return;
        }

    }
}
