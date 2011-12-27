using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DvdV.Subbed.Core.Formats;

namespace DvdV.Subbed.Tests
{
    [TestFixture]
    public class TestStretch
    {
        private BaseBeginEndSubtitle oneSecondSubtitle;

        [SetUp]
        public void Setup()
        {
            oneSecondSubtitle = new BaseBeginEndSubtitle(
                new TimeSpan(0, 4, 0),
                new TimeSpan(0, 4, 1),
                "TestText");
        }

        [Test]
        public void stretchBy_three_times()
        {
            oneSecondSubtitle.StretchBy(3);

            Assert.That(oneSecondSubtitle.Duration, Is.EqualTo(new TimeSpan(0, 0, 3)));
            Assert.That(oneSecondSubtitle.Begin, Is.EqualTo(new TimeSpan(0, 4, 0)));
            Assert.That(oneSecondSubtitle.End, Is.EqualTo(new TimeSpan(0, 4, 3)));
        }

        [Test]
        public void stretchTo_one_hour()
        {
            oneSecondSubtitle.StretchTo(new TimeSpan(0, 1, 0));

            Assert.That(oneSecondSubtitle.Duration, Is.EqualTo(new TimeSpan(0, 1, 0)));
            Assert.That(oneSecondSubtitle.Begin, Is.EqualTo(new TimeSpan(0, 4, 0)));
            Assert.That(oneSecondSubtitle.End, Is.EqualTo(new TimeSpan(0, 5, 0)));
        }
    }
}
