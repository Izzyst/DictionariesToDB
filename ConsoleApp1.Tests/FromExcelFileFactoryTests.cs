﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FromCsvFileFactory ob = new FromCsvFileFactory();
            var result = ob.GetWords();
            result.ForEach(i => i.Defs.ForEach(Assert.NotNull));
        }

    }
}
