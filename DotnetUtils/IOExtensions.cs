using System;
using System.Collections.Generic;
using System.IO;

namespace penCsharpener.DotnetUtils {
    public static class IOExtensions {

        public static IEnumerable<FileInfo> GetFilesRecursively(this DirectoryInfo dirInfo,
                                                                string? searchPattern = null,
                                                                string[]? ignorePaths = null,
                                                                char? containsPartsDelimiter = null,
                                                                Action<UnauthorizedAccessException>? unauthExceptionCallback = null,
                                                                Action<Exception>? exceptionCallback = null) {
            var files = new List<FileInfo>();
            var dirInfos = new List<DirectoryInfo>();

            if (ignorePaths != null && dirInfo.FullName.InIgnoreCase(ignorePaths, containsPartsDelimiter)) {
                return files;
            }

            try {
                if (searchPattern.IsNullOrEmpty()) {
                    files.AddRange(dirInfo.GetFiles());
                    dirInfos.AddRange(dirInfo.GetDirectories());
                } else {
                    files.AddRange(dirInfo.GetFiles(searchPattern));
                    dirInfos.AddRange(dirInfo.GetDirectories(searchPattern));
                }
            } catch (UnauthorizedAccessException unAuthEx) {
                unauthExceptionCallback?.Invoke(unAuthEx);
            } catch (Exception ex) {
                exceptionCallback?.Invoke(ex);
            }
            foreach (var dir in dirInfos) {
                files.AddRange(dir.GetFilesRecursively(searchPattern,
                                                       ignorePaths: ignorePaths,
                                                       containsPartsDelimiter: containsPartsDelimiter));
            }

            return files;
        }

    }
}
