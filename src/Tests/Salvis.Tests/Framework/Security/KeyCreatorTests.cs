using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Salvis.Framework.Security;

namespace Salvis.Tests.Framework.UnitTests.Security
{

    [TestFixture]
    public class KeyCreatorTests
    {

        [Test]
        public void RequestKey_Success()
        {
            var codes = new Dictionary<int, string>();

            for (int i = 0; i < 2500; i++)
            {
                var value = KeyCreator.RequestKey(i + "", 4);
                //  assert
                Assert.IsFalse(codes.ContainsValue(value), "El índice {0} es un duplicado de '{1}'.", i, value);
                //  continue...
                codes.Add(i, value);
            }
        }

        [Test]
        public void RequestKey_SendInfiniteValuesWithTime_Success()
        {
          //KeyCreator.RequestKey(String.Format("{0}", i), x, DateTimeOffset.Now.DateTime, true);
            var codes = new Dictionary<int, string>();

            for (int i = 0; i < 4500; i++)
            {
                var value = KeyCreator.RequestKey(i + "", 5, DateTimeOffset.Now.DateTime, true);
                //  assert
                Assert.IsFalse(codes.ContainsValue(value), "El índice {0} es un duplicado de '{1}'.", i, value);
                //  continue...
                codes.Add(i, value);
            }
        }
        
        [Test]
        public void RequestKey_SendFiniteValuesWithTime_Success()
        {
            var test1 = KeyCreator.RequestKey(1 + "", 4, DateTimeOffset.Now.DateTime, true);
            var test2 = KeyCreator.RequestKey(2 + "", 8, DateTimeOffset.Now.DateTime, true);
            var test3 = KeyCreator.RequestKey(3 + "", 15, DateTimeOffset.Now.DateTime, true);
            var test4 = KeyCreator.RequestKey(4 + "", 30, DateTimeOffset.Now.DateTime, true);

            Assert.IsFalse(test1.Equals(test2), "The contain must not be the same.");
            Assert.IsFalse(test3.Equals(test4), "The contain must not be the same.");
            Assert.IsFalse(test1.Equals(test4), "The contain must not be the same.");
        }

        [TestCase(true, 50)]
        [TestCase(true, 25)]
        public void RequestKey_SendValueAndLimitWithNowDate_Success(bool excludeChars, int limit)
        {

            var fixture = CompositionRoot.FixtureInstance;

            var code = KeyCreator.RequestKey(fixture.Create<int>() + "", limit, DateTimeOffset.Now.DateTime, excludeChars);

            Assert.IsNotNull(code);
            var regex = new Regex(KeyCreator.INVALID_CHARS_REGEX);
            Assert.IsFalse(regex.IsMatch(code), "There could not be matches.");
            Assert.AreEqual(limit, code.Length, "Longitudes no pueden ser diferentes de las ordenadas.");
        }

        [TestCase(false, null)]
        [TestCase(true, null)]
        public void RequestKey_SendValueWithNowDate_Success(bool exluceChars, int limit = -1)
        {
            var fixture = CompositionRoot.FixtureInstance;

            var code = KeyCreator.RequestKey(fixture.Create<int>() + "", limit, DateTimeOffset.Now.DateTime, exluceChars);

            Assert.IsNotNull(code);
            Assert.AreEqual(limit, code.Length, "Longitudes no pueden ser diferentes de las ordenadas.");
        }

        [Test]
        public void RequestKey_SendInvalidValue_Fail()
        {
            var fixture = CompositionRoot.FixtureInstance;

            var code = KeyCreator.RequestKey(Guid.NewGuid().ToString(), -1);

            Assert.IsNotNull(code);
            var regex = new Regex(KeyCreator.INVALID_CHARS_REGEX);
            Assert.IsTrue(regex.IsMatch(code), "There could be matches.");
        }

    }

}
