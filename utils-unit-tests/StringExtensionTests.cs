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
            Assert.True(TestConstants.testMozillaPath.ContainsAllParts(TestConstants.partsMozillaPath, '|'));
            Assert.False(TestConstants.testMozillaPath.ContainsAllParts(TestConstants.expectFalseParts, '|'));
        }

        [Test]
        public void SnakeToPascalCaseTest() {
            var snakeCase = "some_column_in_table_of_database";
            var PascalCase = "SomeColumnInTableOfDatabase";
            Assert.IsTrue(PascalCase == snakeCase.SnakeToPascalCase());
        }
    }
}