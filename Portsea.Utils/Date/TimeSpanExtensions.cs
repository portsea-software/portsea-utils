using System;

namespace Portsea.Utils.Date
{
    public static class TimeSpanExtensions
    {
        private const double AverageDaysInYear = 365.2425;

        public static int Years(this TimeSpan timespan)
        {
            return (int)(timespan.TotalDays / AverageDaysInYear);
        }
    }
}
