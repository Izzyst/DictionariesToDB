using NUnit.Framework;
using ConsoleApp1.Factories;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    public class FromCsvFileFactoryTests
    {
        [Test]
        public void SprawdzeniePoprawnosciGetWords()
        {
            FromCsvFileFactory ob = new FromCsvFileFactory();
            var result = ob.GetWords();
            result.ForEach(Assert.NotNull); 
        }
        [Test]
        public void SprawdzeniePoprawnosciZwracaniaDefinicjiGetWords()
        {
            FromCsvFileFactory ob = new FromCsvFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(Assert.NotNull));
        }

        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanejDefinicjiGetWords()
        {
            FromCsvFileFactory ob = new FromCsvFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(j => Assert.GreaterOrEqual(255, j.Length)));
        }

        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanegoSlowaGetWords()
        {
            FromCsvFileFactory ob = new FromCsvFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => Assert.GreaterOrEqual(255, i.Word.Length));
        }
    }
}
