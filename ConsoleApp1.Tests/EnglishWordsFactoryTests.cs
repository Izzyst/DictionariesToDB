
using ConsoleApp1.Factories;
using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    public class EnglishWordsFactoryTests
    {
        [Test]
        public void SprawdzaniePoprawnosciGettingNodesfromUrl()
        {
            EnglishWordsFactory ob = new EnglishWordsFactory();
            var result = ob.GetWordFromNode("https://www.collinsdictionary.com/dictionary/english/12-oclock-flasher", "//span[@class='orth']", "//div[@class='def']", "eng");
            Assert.NotNull(result);
        }

        [Test]
        public void SprawdzeniePoprawnosciZwracaniaDefinicjiGetWords()
        {
            EnglishWordsFactory ob = new EnglishWordsFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(Assert.NotNull));
        }

        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanejDefinicjiGetWords()
        {
            EnglishWordsFactory ob = new EnglishWordsFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(j=>Assert.GreaterOrEqual( 255, j.Length)));
        }
        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanegoSlowaGetWords()
        {
            EnglishWordsFactory ob = new EnglishWordsFactory();
            var result = ob.GetWords();
            result.ForEach(i => Assert.GreaterOrEqual(255, i.Word.Length));
        }

        [Test]
        public void SprawdzaniePoprawnosciGetWords()
        {
            EnglishWordsFactory ob = new EnglishWordsFactory();
            var result = ob.GetWords();
            result.ForEach(Assert.NotNull);
        }
    }
}
