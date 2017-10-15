using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FromCsvFileFactory ob = new FromCsvFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(Assert.NotNull));
        }
    }
}
