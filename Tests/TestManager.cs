using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DvdV.Subbed.Core.Formats;

namespace DvdV.Subbed.Tests
{
    [TestFixture]
    public class TestManager
    {
        private SubtitleManager sut;

        [SetUp]
        public void Setup()
        {
            sut = new SubtitleManager(new List<BaseBeginEndSubtitle>()
            {
                new BaseBeginEndSubtitle(new TimeSpan(0, 0, 30), new TimeSpan(0, 0, 32), "Text 1"),
                new BaseBeginEndSubtitle(new TimeSpan(0, 1, 42), new TimeSpan(0, 1, 43), "Text 2"),
                new BaseBeginEndSubtitle(new TimeSpan(0, 1, 53), new TimeSpan(0, 1, 56), "Text 3"),
                new BaseBeginEndSubtitle(new TimeSpan(0, 2, 19), new TimeSpan(0, 2, 20), "Text 4"),
                new BaseBeginEndSubtitle(new TimeSpan(0, 3, 55), new TimeSpan(0, 4, 1), "Text 5"),
                new BaseBeginEndSubtitle(new TimeSpan(0, 5, 3), new TimeSpan(0, 5, 14), "Text 6")
            });
        }

        [Test]
        public void transposeBy_one_minute()
        {
            sut.TransposeBy(TimeSpan.FromMinutes(1));

            Assert.That(sut.Subtitles[0].Begin, Is.EqualTo(new TimeSpan(0, 1, 30)));
            Assert.That(sut.Subtitles[1].Begin, Is.EqualTo(new TimeSpan(0, 2, 42)));
            Assert.That(sut.Subtitles[2].Begin, Is.EqualTo(new TimeSpan(0, 2, 53)));
            Assert.That(sut.Subtitles[3].Begin, Is.EqualTo(new TimeSpan(0, 3, 19)));
            Assert.That(sut.Subtitles[4].Begin, Is.EqualTo(new TimeSpan(0, 4, 55)));
            Assert.That(sut.Subtitles[5].Begin, Is.EqualTo(new TimeSpan(0, 6, 3)));
        }

        [Test]
        public void stretchBy_factor_of_one_point_5()
        {
            sut.StretchBy(1.5);

            Assert.That(sut.Subtitles[0].Begin, Is.EqualTo(new TimeSpan(0, 0, 45)));
            Assert.That(sut.Subtitles[0].End, Is.EqualTo(new TimeSpan(0, 0, 48)));
            Assert.That(sut.Subtitles[0].Duration, Is.EqualTo(new TimeSpan(0, 0, 3)));
            // Assuming the middle part is right too...
            Assert.That(sut.Subtitles[5].Begin, Is.EqualTo(new TimeSpan(0, 0, 7, 34, 500)));
            Assert.That(sut.Subtitles[5].End, Is.EqualTo(new TimeSpan(0, 7, 51)));
            Assert.That(sut.Subtitles[5].Duration, Is.EqualTo(new TimeSpan(0, 0, 0, 16, 500)));
        }

        [Test]
        public void extrapolate()
        {

        }


    }
}
