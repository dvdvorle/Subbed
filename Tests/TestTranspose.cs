using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DvdV.Subbed.Core.Formats;

namespace Tests
{
    [TestFixture]
    public class TestTranspose
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
        public void constructor()
        {
            Assert.That(oneSecondSubtitle.Begin, Is.EqualTo(new TimeSpan(0, 4, 0)));
            Assert.That(oneSecondSubtitle.End, Is.EqualTo(new TimeSpan(0, 4, 1)));
            Assert.That(oneSecondSubtitle.Duration, Is.EqualTo(new TimeSpan(0, 0, 1)));
        }

        [Test]
        public void transposeBy_one_second()
        {
            oneSecondSubtitle.TransposeBy(new TimeSpan(0, 0, 4));

            Assert.That(oneSecondSubtitle.Duration, Is.EqualTo(new TimeSpan(0, 0, 1)));
            Assert.That(oneSecondSubtitle.Begin, Is.EqualTo(new TimeSpan(0, 4, 4)));
            Assert.That(oneSecondSubtitle.End, Is.EqualTo(new TimeSpan(0, 4, 5)));
        }

        [Test]
        public void transposeTo_one_hour()
        {
            oneSecondSubtitle.TransposeTo(new TimeSpan(0, 1, 0));

            Assert.That(oneSecondSubtitle.Duration, Is.EqualTo(new TimeSpan(0, 0, 1)));
            Assert.That(oneSecondSubtitle.Begin, Is.EqualTo(new TimeSpan(0, 1, 0)));
            Assert.That(oneSecondSubtitle.End, Is.EqualTo(new TimeSpan(0, 1, 1)));
        }

        [Test]
        public void transposeTill_one_hour()
        {
            oneSecondSubtitle.TransposeTill(new TimeSpan(0, 1, 0));

            Assert.That(oneSecondSubtitle.Duration, Is.EqualTo(new TimeSpan(0, 0, 1)));
            Assert.That(oneSecondSubtitle.Begin, Is.EqualTo(new TimeSpan(0, 0, 59)));
            Assert.That(oneSecondSubtitle.End, Is.EqualTo(new TimeSpan(0, 1, 0)));
        }
    }
}
