using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var result = ob.GetWordFromNode("https://www.collinsdictionary.com/dictionary/english/12-oclock-flasher", "//span[@class='orth']", "//div[@class='def']");
            Assert.NotNull(result);
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
