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
    }
}
