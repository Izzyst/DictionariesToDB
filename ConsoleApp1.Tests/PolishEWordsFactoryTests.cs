using NUnit.Framework;
using ConsoleApp1.Factories;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    class PolishEWordsFactoryTests
    {
        [Test]
        public void SprawdzaniePoprawnosciGetWords()
        {
            PolishEWordsFactory ob = new PolishEWordsFactory();
            var result = ob.GetWords();
            result.ForEach(Assert.NotNull);
        }

        [Test]
        public void SprawdzeniePoprawnosciGettingLinksFromSubCategory()
        {
            PolishEWordsFactory ob = new PolishEWordsFactory();
            var result = ob.GettingLinksFromSubCategory("https://www.bryk.pl/slowniki/slownik-wyrazow-obcych/e");
            result.ForEach(Assert.NotNull);
        }
        [Test]
        public void SprawdzeniePoprawnosciZwracaniaDefinicjiGetWords()
        {
            PolishEWordsFactory ob = new PolishEWordsFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(Assert.NotNull));
        }

        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanejDefinicjiGetWords()
        {
            PolishEWordsFactory ob = new PolishEWordsFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(j => Assert.GreaterOrEqual(255, j.Length)));
        }

        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanegoSlowaGetWords()
        {
            PolishWordsFactory ob = new PolishWordsFactory();
            var result = ob.GetWords();
            result.ForEach(i => Assert.GreaterOrEqual(255, i.Word.Length));
        }
    }
}
