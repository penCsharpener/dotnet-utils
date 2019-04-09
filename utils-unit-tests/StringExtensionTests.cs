using NUnit.Framework;

namespace penCsharpener.DotnetUtils.NUnit {
    [TestFixture]
    public class StringExtensionTests {

        private string NullString = null;
        private string EmptyString = string.Empty;
        private string StringWithContent = "some content";

        [SetUp]
        public void Setup() {

        }

        [Test]
        public void StringIsNullOrEmpty() {
            Assert.True(NullString.IsNullOrEmpty());
            Assert.True(EmptyString.IsNullOrEmpty());
            Assert.IsFalse(StringWithContent.IsNullOrEmpty());
        }

        [Test]
        public void CaseIgnoreContains() {
            var text = "We experience more than we know " +
                        "and know more than we are able to articulate.";
            Assert.True(text.Like("than"));
            Assert.True(text.Like("THAN "));
            Assert.True(NullString.Like(null));
            Assert.False(NullString.Like(""));
            Assert.True(EmptyString.Like(""));
            Assert.False(text.Like("xyz"));
        }

        [Test]
        public void ContainsAllPartsTest() {
            var parts = @"C:\Users\|\AppData\Roaming\Mozilla\Firefox\Profiles\|.default\cache2\entries";
            var expectFalseParts = @"D:\Software\|\Firefox\Profiles\|\cache2\entries";
            var testPath = @"C:\Users\MyUsername\AppData\Roaming\Mozilla\Firefox\Profiles\clsow3acld9s.default\cache2\entries";
            Assert.True(testPath.ContainsAllParts(parts, '|'));
            Assert.False(testPath.ContainsAllParts(expectFalseParts, '|'));
        }
    }
}