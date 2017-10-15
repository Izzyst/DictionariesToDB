using NUnit.Framework;
using ConsoleApp1.Factories;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    public class FromExcelFileFactoryTests
    {
        [Test]
        public void SprawdzaniePoprawnoscLadowaniaPlikuExcel()
        {
            FromExcelFileFactory ob = new FromExcelFileFactory();
            var result = ob.GetWords();
            result.ForEach(Assert.NotNull);
        }


        [Test]
        public void SprawdzeniePoprawnosciZwracaniaDefinicjiGetWords()
        {
            FromExcelFileFactory ob = new FromExcelFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(Assert.NotNull));
        }

        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanejDefinicjiGetWords()
        {
            FromExcelFileFactory ob = new FromExcelFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(j => Assert.GreaterOrEqual(255, j.Length)));
        }
        [Test]
        public void SprawdzeniePoprawnosciDlugosciZwracanegoSlowaGetWords()
        {
            FromExcelFileFactory ob = new FromExcelFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => Assert.GreaterOrEqual(255, i.Word.Length));
        }

    }
}
