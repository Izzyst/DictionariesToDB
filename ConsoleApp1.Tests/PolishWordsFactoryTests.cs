using ConsoleApp1.Factories;
using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    class PolishWordsFactoryTests
    {
        [Test]
        public void SprawdzaniePoprawnosciGetLinksPl()
        {
            PolishWordsFactory ob = new PolishWordsFactory();
            var result = ob.GetLinksPL();
            result.ForEach(Assert.NotNull);
        }

        [Test]
        public void SprawdzaniePoprawnosciGettingNodesFromUrlpl()
        {
            PolishWordsFactory ob = new PolishWordsFactory();
            var result = ob.gettingNodesFromURLPL("http://www.slownik-online.pl/kopalinski/402FB89DD53C6221C12565DB0067FFF5.php");
            result.ForEach(Assert.NotNull);
        }

        [Test]
        public void SprawdzaniePoprawnosciGetWords()
        {
            PolishWordsFactory ob = new PolishWordsFactory();
            var result = ob.GetWords();
            result.ForEach(Assert.NotNull);
        }
    }
}
