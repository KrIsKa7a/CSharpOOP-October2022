namespace Book.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        private Book defBook;

        [SetUp]
        public void SetUp()
        {
            this.defBook = new Book("Harry Potter", "J. K. Rowling");
        }

        //I assume that the property is working correcly
        [Test]
        public void ConstructorShouldInitializeBookNameCorrectly()
        {
            string expectedBookName = "Harry Potter";
            Book book = new Book(expectedBookName, "J. K. Rowling");

            string actualBookName = book.BookName;
            Assert.AreEqual(expectedBookName, actualBookName);
        }

        [Test]
        public void ConstructorShouldInitializeAuthorNameCorrectly()
        {
            string expectedName = "J. K. Rowling";
            Book book = new Book("Harry Potter", expectedName);

            string actualName = book.Author;
            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void ConstructorShouldInitializeFootNoteDictionary()
        {
            Type bookType = this.defBook.GetType();
            FieldInfo dictFieldInfo = bookType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(fi => fi.Name == "footnote");

            object fieldValue = dictFieldInfo.GetValue(this.defBook);
            Assert.IsNotNull(fieldValue);
        }

        [Test]
        public void CountShouldReturnZeroWhenNoFootNotesAdded()
        {
            int expectedCount = 0;
            int actualCount = this.defBook.FootnoteCount;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void CountShouldReturnCorrectCountWhenFootNotesAdded()
        {
            int expectedCount = 2;
            for (int i = 0; i < expectedCount; i++)
            {
                this.defBook.AddFootnote(i, i.ToString());
            }

            int actualCount = this.defBook.FootnoteCount;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase("Real name")]
        [TestCase("1")]
        [TestCase("   ")]
        public void BookNameShouldSetCorrectValues(string bookName)
        {
            Book book = new Book(bookName, "Author");

            string expectedBookName = bookName;
            string actualBookName = book.BookName;

            Assert.AreEqual(expectedBookName, actualBookName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void BookNameShouldThrowExceptionWhenBookNameNullOrEmpty(string bookName)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book(bookName, "Author");
            }, "Invalid BookName!");
        }

        [TestCase("Real name")]
        [TestCase("1")]
        [TestCase("   ")]
        public void AuthorNameShouldSetCorrectValues(string authorName)
        {
            Book book = new Book("Book name", authorName);

            string expectedAuthorName = authorName;
            string actualAuthorName = book.Author;

            Assert.AreEqual(expectedAuthorName, actualAuthorName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void AuthorNameShouldThrowExceptionWhenAuthorNameNullOrEmpty(string authorName)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("Book name", authorName);
            }, "Invalid Author!");
        }

        [Test]
        public void AddingFootNoteShouldIncreaseCount()
        {
            int expectedCount = 1;
            for (int i = 0; i < expectedCount; i++)
            {
                this.defBook.AddFootnote(i, i.ToString());
            }

            int actualCount = this.defBook.FootnoteCount;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddingFootNoteShouldAddKeyInTheDictionary()
        {
            int addedKey = 1;
            this.defBook.AddFootnote(addedKey, "Random text");

            Type bookType = this.defBook.GetType();
            FieldInfo dictFieldInfo = bookType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(fi => fi.Name == "footnote");

            Dictionary<int, string> fieldValue = (Dictionary<int, string>)
                dictFieldInfo.GetValue(this.defBook);
            bool containsKey = fieldValue.ContainsKey(addedKey);

            Assert.IsTrue(containsKey);
        }

        [Test]
        public void AddingExistingFootNoteShouldThrowException()
        {
            int sameKey = 1;
            this.defBook.AddFootnote(sameKey, "Random note");
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.AddFootnote(sameKey, "Another text");
            }, "Footnote already exists!");
        }

        [Test]
        public void FindFootNoteShouldReturnCorrectTextWhenExisting()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            string expectedResult = $"Footnote #{footKey}: {footText}";
            string actualResult = this.defBook.FindFootnote(footKey);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void FindFootNoteShouldThrowExceptionIfThereAreFootNotesButPassedKeyDoesNotExist()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.FindFootnote(999);
            }, "Footnote doesn't exists!");
        }

        [Test]
        public void FindFootNoteShouldThrowExceptionIfThereAreNoFootNotesAtAll()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.FindFootnote(1);
            }, "Footnote doesn't exists!");
        }

        [Test]
        public void AlterFootNoteShouldChangeTextWhenFootNoteExists()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            string expectedText = "New text";
            this.defBook.AlterFootnote(footKey, expectedText);

            Type bookType = this.defBook.GetType();
            FieldInfo dictFieldInfo = bookType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(fi => fi.Name == "footnote");

            Dictionary<int, string> fieldValue = (Dictionary<int, string>)
                dictFieldInfo.GetValue(this.defBook);

            string actualText = fieldValue[footKey];
            Assert.AreEqual(expectedText, actualText);
        }

        [Test]
        public void AlterFootNoteShouldThrowExceptionIfThereAreFootNotesButPassedKeyDoesNotExist()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.AlterFootnote(999, "New invalid text");
            }, "Footnote doesn't exists!");
        }

        [Test]
        public void AlterFootNoteShouldThrowExceptionIfThereAreNoFootNotesAtAll()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.AlterFootnote(1, "Invalid text");
            }, "Footnote doesn't exists!");
        }
    }
}