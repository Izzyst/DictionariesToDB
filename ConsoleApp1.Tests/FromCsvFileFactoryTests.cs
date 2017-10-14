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
    public class FromCsvFileFactoryTests
    {
        [Test]
        public void SprawdzeniePoprawnosciGetWords()
        {
            FromCsvFileFactory ob = new FromCsvFileFactory();
            var result = ob.GetWords();
            result.ForEach(Assert.NotNull);
        }
    }
}
