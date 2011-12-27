using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DvdV.Subbed.Core.Parsers;
using DvdV.Subbed.Core.Formats;

namespace DvdV.Subbed.Tests
{
    [TestFixture]
    public class TestSubRip
    {
        [Test]
        public void test_GetBegin()
        {
            var begin = SubRip.GetBegin("00:40:43,884 --> 00:40:46,786");

            Assert.That(begin, Is.EqualTo(new TimeSpan(0, 0, 40, 43, 884)));
        }

        [Test]
        public void test_GetEnd()
        {
            var end = SubRip.GetEnd("00:40:43,884 --> 00:40:46,786");

            Assert.That(end, Is.EqualTo(new TimeSpan(0, 0, 40, 46, 786)));
        }
    }
}
