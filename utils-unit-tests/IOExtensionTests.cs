using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace penCsharpener.DotnetUtils.NUnit {
    [TestFixture]
    public class IOExtensionTests {

        private string AppData = Environment.GetEnvironmentVariable("appdata");
        private string LocalAppData = Environment.GetEnvironmentVariable("localappdata");
        private string UserProfile = Environment.GetEnvironmentVariable("userprofile");

        private string[] IgnoreList;

        [SetUp]
        public void Setup() {
            IgnoreList = new string[] {
                TestConstants.partsMozillaPath,
                TestConstants.partsChromePath,
                TestConstants.partsChromePath2,
                TestConstants.expectFalseParts,
            };
        }

        [Test]
        public void FindFiles() {
            var dirInfo = new DirectoryInfo($@"{AppData}\Visual Studio Setup");
            var files = dirInfo.GetFilesRecursively().ToList();
            Assert.Greater(files.Count, 0);
        }

        /// <summary>
        /// For this to work properly add a folder in the directory to test against 
        /// and revoke all rights from your own user.
        /// </summary>
        [Test]
        public void FindFilesException() {
            var dirInfo = new DirectoryInfo($@"{UserProfile}\Documents");
            var files = dirInfo.GetFilesRecursively().ToList();
            Assert.Greater(files.Count, 0);
        }

        [Test]
        public void Find() {
            var dirInfo = new DirectoryInfo($@"{UserProfile}\Documents");
            var files = dirInfo.GetFilesRecursively().ToList();
            Assert.Greater(files.Count, 0);
        }

        [Test]
        public void FindWithIgnoreList() {
            var dirInfo = new DirectoryInfo($@"{LocalAppData}\Mozilla");
            var filesIgnore = dirInfo.GetFilesRecursively(ignorePaths: IgnoreList,
                                                    containsPartsDelimiter: '|')
                                                    .ToList();
            var files = dirInfo.GetFilesRecursively().ToList();
            Assert.Greater(files.Count, filesIgnore.Count);
        }
    }
}
