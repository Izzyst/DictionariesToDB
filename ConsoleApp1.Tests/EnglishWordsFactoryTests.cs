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
            var result = ob.gettingNodesfromURL("");
            Assert.AreEqual(result, "");
        }
    }
}
