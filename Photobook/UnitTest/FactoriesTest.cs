using System;
using NUnit;
using NUnit.Framework;
using Photobook.Models;

namespace UnitTest
{
    [TestFixture]
    class FactoriesTest
    {
        [TestCase(DataType.Guest, typeof(GuestParser))]
        public void TestGenerateParserFactory(DataType d, IJSONParser pars)
        {
            IJSONParser testPars = ParserFactory.Generate(d);

            Assert.That(testPars.GetType(), Is.EqualTo(pars.GetType()));
        }
    }
}
