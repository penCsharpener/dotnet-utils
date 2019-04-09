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

        [SetUp]
        public void Setup() {

        }

        [Test]
        public void FindUserProfileFiles() {
            var dirInfo = new DirectoryInfo($@"{AppData}\Visual Studio Setup");
            var files = dirInfo.GetFilesRecursively().ToList();
            Assert.Greater(files.Count, 0);
        }

        [Test]
        public void FindUserProfileFilesException() {
            var dirInfo = new DirectoryInfo($@"{UserProfile}\Documents");
            var files = dirInfo.GetFilesRecursively().ToList();
            Assert.Greater(files.Count, 0);
        }
    }
}
