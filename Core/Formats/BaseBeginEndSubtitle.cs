using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DvdV.Subbed.Core.Formats
{
    public class BaseBeginEndSubtitle : ISubtitle
    {
        public BaseBeginEndSubtitle(TimeSpan begin, TimeSpan end, string text)
        {
            Begin = begin;
            End = end;
            Text = text;
        }


        #region ISubtitle Members

        public TimeSpan Duration
        {
            get
            {
                return End - Begin;
            }
        }

        public virtual TimeSpan Begin { get; protected set; }

        public virtual TimeSpan End { get; protected set; }

        public virtual string Text { get; set; }

        public virtual void TransposeBy(TimeSpan diff)
        {

            Begin += diff;
            End += diff;
        }

        public void TransposeTo(TimeSpan begin)
        {
            var duration = Duration;
            Begin = begin;
            End = Begin + duration;
        }

        public void TransposeTill(TimeSpan end)
        {
            var duration = Duration;
            End = end;
            Begin = End - duration;
        }

        public void StretchBy(double factor)
        {
            var currentDuration = Duration;
            var newDuration = new TimeSpan((long)(currentDuration.Ticks * factor));
            End = newDuration + Begin;
        }

        public void StretchTo(TimeSpan duration)
        {
            End = Begin + duration;
        }

        #endregion
    }
}
